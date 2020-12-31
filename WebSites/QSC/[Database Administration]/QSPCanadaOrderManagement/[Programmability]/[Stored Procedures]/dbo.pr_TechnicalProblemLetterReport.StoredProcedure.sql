USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_TechnicalProblemLetterReport]    Script Date: 06/07/2017 09:20:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_TechnicalProblemLetterReport] AS

declare	@sLastName			varchar(50),
	@sFirstName			varchar(50),
	@sStreet1			varchar(50),
	@sStreet2			varchar(50),
	@sCity				varchar(50),
	@sProvince			varchar(10),
	@sPostalCode			varchar(20),
	@sCountry			varchar(6),
	@sLanguage			varchar(2),
	@sMagazineTitle		varchar(55),
	@iNbIssues			int,
	@fAmount			numeric(10,2),
	@sMagazineTitleResult		varchar(3000),
	@sNbIssuesResult		varchar(2000),
	@sAmountResult		varchar(2000)

create table #temp     (LastName	varchar(50),
			FirstName	varchar(50),
			Street1		varchar(50),
			Street2		varchar(50),
			City		varchar(50),
			Province	varchar(10),
			PostalCode	varchar(20),
			Country		varchar(6),
			Language	varchar(2),
			MagazineTitle	varchar(3000),
			NbIssues	varchar(2000),
			Amount		varchar(2000))

declare c1 cursor for
SELECT	distinct
	ltrim(rtrim(crh.LastName)) as LastName,
    	ltrim(rtrim(crh.FirstName)) as FirstName,
     	coalesce(crh.Address1, '') as Street1,
	coalesce(crh.Address2, '') as Street2,
	crh.City,
	crh.state as province,
	crh.zip as postalcode,
	'Canada' as Country,
	--codrh.MagazineTitle,
	--codrh.NumberOfissues	as NbIssues,	
	--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Amount,
	--codrh.CustomerOrderHeaderInstance,
	--codrh.TransID,
	--codrh.remitbatchid,
	case crh.state when 'QC' then 'fr' else 'en' end as Language

FROM	CustomerOrderHeader coh,
	CustomerOrderDetail cod,
	CustomerRemitHistory crh,
	CustomerOrderDetailRemitHistory codrh,
	QSPCanadaCommon..Campaign ca

WHERE
	cod.CustomerOrderHeaderInstance = coh.Instance
AND	codrh.CustomerOrderHeaderInstance = coh.Instance
AND	codrh.TransID = cod.TransID
AND	crh.Instance = codrh.CustomerRemitHistoryInstance
AND	ca.ID = coh.CampaignID
AND	coh.OrderBatchDate < '2005-01-01'
and 	codrh.datechanged > '2005-02-02 11:00' and codrh.datechanged < '2005-02-02 13:20'
AND	codrh.Status = 42000

--ORDER BY
--	ltrim(crh.LastName), ltrim(crh.FirstName)

open c1
fetch next from c1 into @sLastName, @sFirstName, @sStreet1, @sStreet2, @sCity, @sProvince, @sPostalCode, @sCountry, @sLanguage
while @@fetch_status = 0
begin
	set @sMagazineTitleResult = ''
	set @sNbIssuesResult = ''
	set @sAmountResult = ''

	declare c2 cursor for
	SELECT	codrh.MagazineTitle,
		codrh.NumberOfissues	as NbIssues,	
		--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Amount
		convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) as Amount
	
	FROM	CustomerOrderHeader coh,
		CustomerOrderDetail cod,
		CustomerRemitHistory crh,
		CustomerOrderDetailRemitHistory codrh,
		QSPCanadaCommon..Campaign ca
	
	WHERE
		cod.CustomerOrderHeaderInstance = coh.Instance
	AND	codrh.CustomerOrderHeaderInstance = coh.Instance
	AND	codrh.TransID = cod.TransID
	AND	crh.Instance = codrh.CustomerRemitHistoryInstance
	AND	ca.ID = coh.CampaignID
	AND	coh.OrderBatchDate < '2005-01-01'
	and 	codrh.datechanged > '2005-02-02 11:00' and codrh.datechanged < '2005-02-02 13:20'
	AND	codrh.Status = 42000
	AND	ltrim(rtrim(crh.LastName)) = @sLastName
	AND	ltrim(rtrim(crh.FirstName)) = @sFirstName
	AND	coalesce(crh.Address1, '') = @sStreet1
	AND	coalesce(crh.Address2, '') = @sStreet2
	AND	crh.City = @sCity
	AND	crh.state = @sProvince
	AND	crh.zip = @sPostalCode
	AND	case crh.state when 'QC' then 'fr' else 'en' end = @sLanguage

	open c2
	fetch next from c2 into @sMagazineTitle, @iNbIssues, @fAmount
	while @@fetch_status = 0
	begin
		set @sMagazineTitleResult = @sMagazineTitleResult + @sMagazineTitle + CHAR(13) + CHAR(10)
		set @sNbIssuesResult = @sNbIssuesResult + convert(varchar, @iNbIssues) + CHAR(13) + CHAR(10)
		set @sAmountResult = @sAmountResult + convert(varchar, @fAmount) + CHAR(13) + CHAR(10)

		fetch next from c2 into @sMagazineTitle, @iNbIssues, @fAmount
	end
	close c2
	deallocate c2

	insert into #temp values (@sLastName, @sFirstName, @sStreet1, @sStreet2, @sCity, @sProvince, @sPostalCode, @sCountry, @sLanguage, @sMagazineTitleResult, @sNbIssuesResult, @sAmountResult)

	fetch next from c1 into @sLastName, @sFirstName, @sStreet1, @sStreet2, @sCity, @sProvince, @sPostalCode, @sCountry, @sLanguage
end
close c1
deallocate c1

SELECT
	LastName,
	FirstName,
	Street1,
	Street2,
	City,
	Province,
	PostalCode,
	Country,
	Language,
	MagazineTitle,
	NbIssues,
	Amount
FROM	#temp

drop table #temp

/*
SELECT
	upper(substring(ltrim(crh.LastName), 1, 1)) + case when len(crh.LastName) > 0 then lower(substring(ltrim(crh.LastName), 2, len(ltrim(crh.LastName)) - 1)) else '' end as LastName,
    	upper(substring(ltrim(crh.FirstName), 1, 1)) + case when len(crh.FirstName) > 0 then lower(substring(ltrim(crh.FirstName), 2, len(ltrim(crh.FirstName)) - 1)) else '' end as FirstName,
     	coalesce(crh.Address1, '') as Street1,
	coalesce(crh.Address2, '') as Street2,
	crh.City,
	crh.state as province,
	crh.zip as postalcode,
	'Canada' as Country,
	codrh.MagazineTitle,
	codrh.NumberOfissues	as NbIssues,	
	convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Amount,
	codrh.CustomerOrderHeaderInstance,
	codrh.TransID,
	codrh.remitbatchid,
	case crh.state when 'QC' then 'fr' else 'en' end as Language

FROM	CustomerOrderHeader coh,
	CustomerOrderDetail cod,
	CustomerRemitHistory crh,
	CustomerOrderDetailRemitHistory codrh,
	QSPCanadaCommon..Campaign ca

WHERE
	cod.CustomerOrderHeaderInstance = coh.Instance
AND	codrh.CustomerOrderHeaderInstance = coh.Instance
AND	codrh.TransID = cod.TransID
AND	crh.Instance = codrh.CustomerRemitHistoryInstance
AND	ca.ID = coh.CampaignID
AND	coh.OrderBatchDate < '2005-01-01'
and 	codrh.datechanged > '2005-02-02 11:00' and codrh.datechanged < '2005-02-02 13:20'
AND	codrh.Status = 42000

ORDER BY
	ltrim(crh.LastName), ltrim(crh.FirstName)
*/
GO

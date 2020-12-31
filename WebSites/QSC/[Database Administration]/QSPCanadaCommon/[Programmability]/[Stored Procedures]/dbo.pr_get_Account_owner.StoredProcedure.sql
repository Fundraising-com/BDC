USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Account_owner]    Script Date: 06/07/2017 09:33:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_Account_owner]
 @AccountID int
AS

select 
	'Campaign' as TBL
	, NULL as ProductLine
	, Id as CampaignID
	, BillToAccountID
	, ShipToAccountId
	, CP.FMID
	, Firstname
	, LastName
	, Convert(varchar(10),StartDate,101) AS StartDate
	, Convert(varchar(10),EndDate  ,101) AS EndDate
from 
	dbo.Campaign CP
	left join dbo.FieldManager FM
	on CP.fmid = FM.fmid
where
	BillToAccountID = @AccountID
	or shipToAccountID = @AccountID
union
select 
	'FMAccountProduct' as TBL
	, fmap.MajorProductLineID as ProductLine
	, NULL as CampaignID
	, fmap.AccountID
	, fmap.AccountID
	, fmap.FMID
	, FM.Firstname
	, FM.LastName
	, NULL
	, NULL
 FROM
	dbo.FieldManagerAccountProduct fmap
	left join dbo.FieldManager FM
	on fmap.fmid = FM.fmid
WHERE
	fmap.AccountID = @AccountID
GO

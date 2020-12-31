USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[EZTRAK_contract_export]    Script Date: 06/07/2017 09:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EZTRAK_contract_export]
--	@sFMID varchar(4),
--	@isearch_type int = null,
--	@scriteria varchar(100) = null,
--	@iErrorCode int OUTPUT
AS
SET NOCOUNT ON
--based on [QCAP].[dbo].[pr_CAccount_SelectAll_Search]
--exports list of eligable accounts\campaigns over to EzTrak

/*
--DECLARE @table_AllAccount TABLE
create table [dbo].[EZTRAK_contract_export_TABLE]
--#table_AllAccount
(
	Id int,
	Name varchar(50),
	Address1 varchar(50),
	Address2 varchar(50),
	City varchar(50),
	State varchar(2),
	Zip varchar(6),
	Zip4 varchar(4),
	Sponsor varchar(50),
	FMID varchar(4),
	ProgramStartDate datetime,
	ProgramEndDate datetime,
	--x SchoolType varchar(2),
	--x ProgramType int,
	--x Status int,
	FiscalYR int,
	--x ProgramDescription varchar(50),
	--x ProductId int,
	--x ProductDescription varchar(50)
	CampaignID int,
	Country varchar(4)
)
*/
DELETE FROM [dbo].[EZTRAK_contract_export_TABLE] ;
	
/*
DECLARE @table_AccountToLink TABLE
(
	Id int,
	Name varchar(50),
	Address1 varchar(50),
	Address2 varchar(50),
	City varchar(50),
	State varchar(2),
	Zip varchar(6),
	Zip4 varchar(4),
	Sponsor varchar(50),
	FMID varchar(4),
	ProgramStartDate datetime,
	ProgramEndDate datetime,
	--x SchoolType varchar(2),
	--x ProgramType int,
	--x Status int,
	FiscalYR int,
	--x ProgramDescription varchar(50),
	--x ProductId int,
	--x ProductDescription varchar(50),
	CampaignID int,
	Country varchar(4),
	CampaignAlreadyExists bit
)
*/

DECLARE @CurrentFY int
SET @CurrentFY = dbo.fnc_GetDateFiscalYR(GETDATE())

--Select statement to retreive all Account for the current FiscalYear
INSERT [dbo].[EZTRAK_contract_export_TABLE]
SELECT     
		  ACCOUNT.Id
		, ACCOUNT.Name
		, ad.Street1
		, ad.Street2
		, ad.City
		, ad.StateProvince
		, ad.Postal_Code
		, ad.zip4
		, [sponsor] = cont.[Firstname] + ' ' + cont.[Lastname]
			--ACCOUNT.Sponsor
		, [fmid] = CAMP.FMID --ACCOUNT.FMID
		, CAMP.StartDate AS ProgramStartDate
		, CAMP.EndDate AS ProgramEndDate
		--x , ACCOUNT.SchoolType
		--x , ACCOUNT.ProgramType
		--x , ACCOUNT.Status
		, [FiscalYR] = [dbo].[fnc_GetDateFiscalYR] ( CAMP.StartDate ) --x  CAMP.CAFiscal
		--x , [ProgramDescription] = QSPCanadaCommon.dbo.CAccount_ProgramType.Description
		--x , [ProductId] = QSPCanadaCommon.dbo.CAccount_ProgramType.ProductId
		--x , [ProductDescription] = QSPCanadaCommon.dbo.CAccount_ProductType.Description
		, CAMP.ID AS CampaignID
		, ad.Country
		, [Corp_Division] = 2 --QSP Canada
FROM         
		QSPCanadaCommon.dbo.CAccount as ACCOUNT
	--xINNER JOIN QSPCanadaCommon.dbo.CCA ON ACCOUNT.Id = CAMP.AccountInstance 
		inner join QSPCanadaCommon.dbo.campaign as CAMP on ACCOUNT.Id = CAMP.BillToAccountID
		left join QSPCanadaCommon.dbo.Address ad on ad.AddressListID = ACCOUNT.AddressListID and ad.Address_Type = 54002 --54002: Bill to address

	--INNER JOIN QSPCanadaCommon.dbo.CAccount_ProgramType ON ACCOUNT.ProgramType = QSPCanadaCommon.dbo.CAccount_ProgramType.Id 
	--INNER JOIN QSPCanadaCommon.dbo.CAccount_ProductType ON QSPCanadaCommon.dbo.CAccount_ProgramType.ProductId = QSPCanadaCommon.dbo.CAccount_ProductType.Id
	
		INNER JOIN [QSPCanadaCommon].[dbo].[Contact] as cont ON CAMP.[ShipToCampaignContactID] = cont.ID
WHERE	--x (CAMP.FMID = @sFMID) 
		--AND (ACCOUNT.Status IN (1, 2, 3, 6)) 
		--AND (ACCOUNT.ProgramType <> 5) 
        
        --x (CAMP.CAFiscal >= @CurrentFY)
        CAMP.[StartDate] >= '2015-06-01'
        AND CAMP.[Status] = 37002
/*--x
	--Select statement to retreive all Dummy Account
	INSERT INTO @table_AllAccount
		SELECT  ACCOUNT.Id, 
				ACCOUNT.Name, 
				ACCOUNT.Address1, 
				ACCOUNT.Address2, 
				ACCOUNT.City, 
				ACCOUNT.State, 
				ACCOUNT.Zip, 
				ACCOUNT.Zip4, 
				ACCOUNT.Sponsor, 
				ACCOUNT.FMID, 
				ACCOUNT.ProgramStartDate, 
				ACCOUNT.ProgramEndDate 
				--x ACCOUNT.SchoolType,
				--x ACCOUNT.ProgramType, 
				--x ACCOUNT.Status,
				, [FiscalYR] = @CurrentFY --x @CurrentFY as FiscalYR  -- We virtually create an FY cause we need the info when insert a new campaign
		        --x QSPCanadaCommon.dbo.CAccount_ProgramType.Description AS ProgramDescription, 
		        --x QSPCanadaCommon.dbo.CAccount_ProgramType.ProductId, 
		        --x QSPCanadaCommon.dbo.CAccount_ProductType.Description AS ProductDescription
		FROM    QSPCanadaCommon.dbo.CAccount as ACCOUNT
				
				--x INNER JOIN QSPCanadaCommon.dbo.CAccount_ProgramType ON ACCOUNT.ProgramType = QSPCanadaCommon.dbo.CAccount_ProgramType.Id 
				--x INNER JOIN QSPCanadaCommon.dbo.CAccount_ProductType ON QSPCanadaCommon.dbo.CAccount_ProgramType.ProductId = QSPCanadaCommon.dbo.CAccount_ProductType.Id
		--x WHERE   --x (ACCOUNT.FMID = @sFMID) 
				--x (ACCOUNT.Status IS NULL) 
				--x AND (ACCOUNT.ATrackDateCreated IS NOT NULL)
--x
*/
				
	--Select statement for All Account in Account_Ownership Table
/*	SELECT  caccount_id, 
			dbo.fnc_GetDateFiscalYR(start_date) AS FiscalYR
	INTO    #ExistingAccount
	FROM    dbo.account_ownership
	WHERE   (fm_id = @sFMID) 
			AND (deleted = 0)*/
/*			
	--Last operation to filter
	INSERT  @table_AccountToLink
		SELECT		DISTINCT
					AllAccount.Id int,
					AllAccount.Name,
					AllAccount.Address1,
					AllAccount.Address2,
					AllAccount.City,
					AllAccount.State,
					AllAccount.Zip,
					AllAccount.Zip4,
					AllAccount.Sponsor,
					AllAccount.FMID,
					AllAccount.ProgramStartDate,
					AllAccount.ProgramEndDate,
					--x AllAccount.SchoolType,
					--x AllAccount.ProgramType,
					--x AllAccount.Status,
					AllAccount.FiscalYR
					--x AllAccount.ProgramDescription,
					--x AllAccount.ProductId,
					--x AllAccount.ProductDescription
					-- Ben 09/05/2006:
					-- Add a boolean existing whether a campaign already exists
					--CASE WHEN EXISTS
					--(SELECT		caccount_id
					--	FROM    dbo.account_ownership
					--	WHERE   fm_id = @sFMID
					--	AND		deleted = 0
					, AllAccount.CampaignID
					, AllAccount.Country
					, [CampaignAlreadyExists] = 
					 CASE WHEN EXISTS
					(SELECT		caccount_id
						FROM    [dbo].[account_ownership] as ao
						WHERE   ao.[fm_id] = AllAccount.[FMID]
						AND		ao.[deleted] = 0
						AND		ao.[caccount_id] = [AllAccount].[id]
						AND		[dbo].[fnc_GetDateFiscalYR]([start_date]) = AllAccount.[FiscalYR])
						THEN 1 ELSE 0
						--THEN 'YES' else 'no'
					END					
		FROM (Select * FROM @table_AllAccount) AS AllAccount
		-- Ben - 09/05/2006:
		-- Remove filter so that many campaigns can be created for an account
		/*WHERE ExistingAccount.caccount_id IS NULL AND
			ExistingAccount.FiscalYR IS NULL*/
		ORDER BY id
*/
		
		
--		select max(CampaignID) , min(CampaignID) from @table_AllAccount 

SELECT 
	  [ContractID] = CampaignID
	, [CONTRACTTYPE] = 'CAN'
	, [PLTYPES] = '' --There is code to do this but character limit of 10 makes it not worth it
	, [PROGRAMSTARTDATE] = cast([ProgramStartDate] as datetime)
	, [PROGRAMENDDATE]   = cast([ProgramEndDate] as datetime)
	, [AccountID_SAPsoldToPartner] = [ID]
	, [AccountName_SAPsoldToPartner] = Name
	, [STREETADDRESS1] = Address1
	, [STREETADDRESS2] = Address2
	, [CITY] = City
	, [STATEPROVINCE] = [State]
	, [POSTALCODE] = Zip
	, [Zip4] = Zip4
	, [COUNTRY] = Country
	, [AccountID_Sponsor] = null
	, [AccountName_Sponsor] = [Sponsor]
	, [fmid_qspca] = fmid
	, [AccountID_SalesRep] = ''
	, [AccountName_SalesRep] = ''
	, [PhoneHome_SalesRep] = ''
	, [PhoneWork_SalesRep] = ''
	, [PhoneFax_SalesRep] = ''
	, [Email_SalesRep] = ''
	, [iDocSapNumber] = ''
	, [iDocName] = ''
	, [iDocSegmentName] = ''
	, [processed] = cast(0 as bit)--false
	, [Corp_Division]=cast(2 as tinyint)--Canada
		
FROM
	[dbo].[EZTRAK_contract_export_TABLE]
--where isnull([Sponsor],'') != '' 
order by [Id] desc 
/*	
	IF (LEN(@scriteria)=0 OR (@scriteria IS NULL))
		SET @isearch_type = NULL 
	
	IF (@isearch_type) IS NULL
		SELECT  *	
		FROM @table_AccountToLink			
	ELSE
		BEGIN
			IF (@isearch_type = 0)
				BEGIN
					IF (@scriteria = '#%')
						BEGIN
							SELECT  *	
							FROM @table_AccountToLink	
							WHERE [Name]  < 'a'									
						END
					ELSE
						SELECT  *	
						FROM @table_AccountToLink	
						WHERE [Name] LIKE @scriteria
				END
			IF (@isearch_type = 1)				
					SELECT  *	
					FROM @table_AccountToLink	
					WHERE [Name] LIKE @scriteria		
				
			IF (@isearch_type = 2)
				SELECT  *	
				FROM @table_AccountToLink	
				WHERE  [Id] LIKE @scriteria
					
			IF (@isearch_type = 3)
				SELECT  *	
				FROM @table_AccountToLink	
				WHERE  [State] LIKE @scriteria
				
			IF (@isearch_type = 4)
				SELECT  *	
				FROM @table_AccountToLink	
				WHERE  [Zip] LIKE @scriteria
			
		END
		
	-- Get the Error Code for the statement just executed.
	SELECT @iErrorCode=@@ERROR
*/
--drop table #table_AllAccount ;
SET NOCOUNT OFF
GO

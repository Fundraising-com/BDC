USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_send_EstimatedSalesView_email]    Script Date: 06/07/2017 09:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_send_EstimatedSalesView_email]
   @p_program_id 	int = 0
  ,@p_status_instance   int = 0
  ,@p_fm_id 		varchar(4)
  ,@p_from_date 	datetime = '07/01/2005'
  ,@p_to_date		datetime = '06/30/2006'
  ,@b_show_daily 	bit = 1
As

SET NOCOUNT ON

-------------------------------------------------
--- get the date format to use ---
-------------------------------------------------
DECLARE	@DateFormat int
--exec 	@DateFormat = QspCanadaCommon.dbo.pr_DateFormat @p_fm_id
--SELECT @DateFormat = 101
SELECT @DateFormat = 1


IF @p_program_id = 0--all programs
BEGIN
 SET @p_program_id = NULL
END

IF @p_status_instance = 0--all statuses
BEGIN
 SET @p_status_instance = NULL
END

IF @p_fm_id = 0 or @p_fm_id = ''--all FMs
BEGIN
 SET @p_fm_id = NULL
END

-------------------------------------------------
--- get the sales info               ---
-------------------------------------------------
 Select 
	  BT.DateReceived 						AS DateReceived
	, BT.AccountId 		 					AS AccountID                                           
	, AC.[Name] 							AS AccountName
	, BT.CampaignId 						AS CampaignID
	, cast('' as varchar(2000)) 					AS Programs
	, FM.LastName + ',' + Firstname 				AS FMname
	, BT.OrderID 							AS OrderID
	, CD.[Description] 						AS OrderStatus
	, isnull(BT.EnterredAmount,isnull(CP.[EstimatedGross],0))	AS AmtEstimated
	, isnull(BT.[CalculatedAmount],0)				AS AmtActual
	, (isnull(BT.[CalculatedAmount],0)  - isnull(BT.EnterredAmount,isnull(CP.[EstimatedGross],0)) ) 	AS Variance
 Into
	#TempEstSales
 From 
	  QspCanadaOrderManagement.dbo.Batch 	BT
	, QspCanadaCommon.dbo.Campaign		CP
	, QspCanadaCommon.dbo.FieldManager		FM
	, QspCanadaCommon.dbo.CAccount		AC
	, QspCanadaCommon.dbo.CodeDetail		CD
 Where  
	BT.CampaignId			= CP.[ID]
	--Order Qualifier EXCLUSION LIST
	and BT.[OrderQualifierID] NOT IN (
		 39004 --Test
		,39005 --Problem Solver
		,39007 --Field Supplies
		,39006 --Kanata
		,39008 --Customer Service
		,39009 --Internet
		,39010 --GiftFix
		,39011 --Internet Fix
		,39012 --Order Correction
		,39013 --Credit Card Reprocess
		,39014 --CC Reprocess Courtesy	
		,39015 --CC Reprocessed to invoice	
		,39016 --FM Gift Sample	
		,39017 -- 
		,39018
		,39019
	)
	--Order Qualifier INCLUSION LIST
	AND BT.[OrderQualifierID] IN (
		 39001 --Main
		,39002 --Supplement
		,39003 --Staff
	)
	--Order Type EXCLUSION LIST
	and BT.[OrderTypeCode] 	NOT IN (
		41009 --MAGNET
	) 
	and FM.fmid			= CP.FMID
 	and BT.AccountId		= AC.ID
 	and BT.StatusInstance		= CD.Instance
 	and BT.StatusInstance		= isnull(@p_status_instance,BT.StatusInstance)
	-- according to ping, for the SDS-QSP-SQL db only
	--and BT.StatusInstance		= 40002 
 	and FM.fmid 			= isnull(@p_fm_id,FM.fmid)
	and BT.Date  BETWEEN @p_from_date and @p_to_date
 	and (	(@p_program_id IS NULL)
		OR 
		(BT.CampaignId IN ( 
			select distinct CampaignId
			  from QspCanadaCommon.dbo.CampaignProgram
			 where ProgramId = @p_program_id and DeletedTF <> 1 )))

	and BT.OrderID NOT IN (91658, 91659) -- Steve Bailey's rekeyed orders - BN 2005/06/28


-----------------------------------------------
--- Update the programs run ---
-----------------------------------------------
DECLARE @CampaignIDtoUpdate int
DECLARE @CurrentProgram varchar(255)

DECLARE CampaignsCursor CURSOR FOR 
   SELECT  DISTINCT CampaignID FROM #TempEstSales
      OPEN CampaignsCursor
FETCH NEXT FROM CampaignsCursor INTO @CampaignIDtoUpdate

--loop through #TempEstSales
WHILE(@@fetch_status <> -1)
BEGIN
	DECLARE ProgramsCursor CURSOR FOR 
	select 	c.[Abr] + ',' AS ProgramName
	from 	CampaignProgram a left join Program c on a.ProgramID = c.ID
	WHERE	a.CampaignID = @CampaignIDtoUpdate AND a.DeletedTF <> 1
	ORDER BY a.ProgramID ASC

	OPEN ProgramsCursor
	FETCH NEXT FROM ProgramsCursor INTO @CurrentProgram
	
	--loop through programs for this @CampaignIDtoUpdate
	WHILE(@@fetch_status <> -1)
	BEGIN
		UPDATE #TempEstSales
		SET [Programs] = [Programs] + @CurrentProgram
		WHERE CampaignID = @CampaignIDtoUpdate
		
		--get the next program to update
		--this campaign with
		FETCH NEXT FROM ProgramsCursor INTO @CurrentProgram
	END
	--all done with this campaign
	CLOSE ProgramsCursor
	DEALLOCATE ProgramsCursor

	--GET THE NEXT campaign to update
	FETCH NEXT FROM CampaignsCursor INTO @CampaignIDtoUpdate
END
--all done updating the campaigns
CLOSE CampaignsCursor
DEALLOCATE CampaignsCursor

--remove the ',' at the end of programs
UPDATE #TempEstSales 
   SET Programs = substring(Programs,  1, len(Programs) - 1 )
 WHERE len(Programs) > 1
-- WHERE substring(Programs, len(Programs) - 2, 2 ) = ', ';

SET NOCOUNT OFF

IF @b_show_daily = 1
begin
	--regular emails
	SELECT 
		Convert(varchar(10),DateReceived,@DateFormat) AS DateRecieved
		,  AccountID                                           
		,  AccountName
		,  CampaignID
		,  Programs
		,  FMname
		,  OrderID
		,  OrderStatus
		,  AmtEstimated
		,  AmtActual
		,  Variance
	   FROM 
		#TempEstSales 
	ORDER BY  
	 	  DATEPART(yyyy, DateReceived) DESC
	 	, DATEPART(mm,  DateReceived) DESC
	 	, DATEPART(dd,   DateReceived) DESC
		, AccountID   DESC
		, CampaignID DESC
end
ELSE
begin
	--company summary emails - no daily details
	SELECT 
		Convert(varchar(10),DateReceived,111) AS DateRecieved
		, count(OrderID)    as OrderCount
		, sum(AmtEstimated) as AmtEstimatedSum
	   FROM 
		#TempEstSales 
	group by
		Convert(varchar(10),DateReceived,111)
	ORDER BY  
		Convert(varchar(10),DateReceived,111) DESC	
end

DROP TABLE #TempEstSales ;
GO

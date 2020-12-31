USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CampaignInfo_ForLinks]    Script Date: 06/07/2017 09:33:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_get_CampaignInfo_ForLinks]
  @AccountID int
AS



DECLARE @fyS datetime
DECLARE @fyE datetime

EXEC QSPCanadaCommon.dbo.pr_get_FYdates
  @FiscalStart = @fyS OUTPUT 
, @FiscalEnd   = @fyE OUTPUT 


CREATE TABLE #Campaigns
(
	CampaignID int NOT NULL,
	ValidYear varchar(5) NULL,
	StartDate datetime NULL,
	EndDate datetime NULL,
	ShipAID int NULL,
	BillAID int NULL,
	FMID varchar(4) NULL,
	Status int NULL,
	Lang varchar(10) NULL,
	IsStaffOrder bit NULL,
	IsTestCampaign bit NULL,
	Programs varchar(255) NULL
)

------------------------------------------------------------------
--- Get the Campaigns with this Account  ---
------------------------------------------------------------------
INSERT INTO 
	#Campaigns
SELECT
	[ID]	   AS [CampaignID],
	'FALSE' AS ValidYear,
	StartDate,
	EndDate,
	shiptoaccountid as ShipAID,
	billtoaccountid as BillAID,
	FMID,
	Status,
	Lang,
	ISNULL(IsStaffOrder, 0) AS [IsStaffOrder],
	ISNULL(IsTestCampaign, 0) AS [IsTestCampaign],
	'' AS [Programs]	
FROM 
	Campaign
WHERE
	ShipToAccountId = @AccountID
	OR BillToAccountId = @AccountID
	AND Campaign.Status NOT IN (37005) --(campaign status - cancel)

UPDATE #Campaigns
   SET ValidYear = 'TRUE'
WHERE  StartDate BETWEEN @fyS AND @fyE
   AND EndDate   BETWEEN @fyS AND @fyE
   AND IsStaffOrder = 0 ;

-------------------------------
--- Update the programs run ---
-------------------------------
DECLARE @CampaignIDtoUpdate int
DECLARE @c_IsStaffOrder bit
DECLARE @c_IsTestCampaign bit
DECLARE @CurrentProgram varchar(255)

DECLARE CampaignsCursor CURSOR FOR 
SELECT CampaignID, IsStaffOrder, IsTestCampaign FROM #Campaigns
OPEN CampaignsCursor
FETCH NEXT FROM CampaignsCursor INTO @CampaignIDtoUpdate, @c_IsStaffOrder, @c_IsTestCampaign

--loop through #Campaigns
WHILE(@@fetch_status <> -1)
BEGIN
	DECLARE ProgramsCursor CURSOR FOR 
	select 	c.[Name] + ';' AS ProgramName
	from 	CampaignProgram a left join Program c on a.ProgramID = c.ID
	WHERE	a.CampaignID = @CampaignIDtoUpdate AND a.DeletedTF <> 1
	ORDER BY a.ProgramID ASC

	OPEN ProgramsCursor
	FETCH NEXT FROM ProgramsCursor INTO @CurrentProgram
	
	--loop through programs for this @CampaignIDtoUpdate
	WHILE(@@fetch_status <> -1)
	BEGIN
		UPDATE #Campaigns
		SET [Programs] = [Programs] + @CurrentProgram
		WHERE CampaignID = @CampaignIDtoUpdate
		
		--get the next program to update
		--this campaign with
		FETCH NEXT FROM ProgramsCursor INTO @CurrentProgram
	END
	--all done with this campaign
	CLOSE ProgramsCursor
	DEALLOCATE ProgramsCursor

	--pretend that "IsStaffOrder" is a program 
	UPDATE #Campaigns
	SET [Programs] = 'Staff Order;' + [Programs]
	WHERE CampaignID = @CampaignIDtoUpdate AND @c_IsStaffOrder = 1

	--pretend that "IsTestOrder" is a program 
	UPDATE #Campaigns
	SET [Programs] = 'Test Campaign;' + [Programs]
	WHERE CampaignID = @CampaignIDtoUpdate AND @c_IsTestCampaign = 1

	--GET THE NEXT campaign to update
	FETCH NEXT FROM CampaignsCursor INTO @CampaignIDtoUpdate, @c_IsStaffOrder, @c_IsTestCampaign

END
--all done updating the campaigns
CLOSE CampaignsCursor
DEALLOCATE CampaignsCursor

--final select, then toss the temp table
SET NOCOUNT OFF

  SELECT 	
	CampaignID,
	ValidYear,
	StartDate,
	EndDate,
	ShipAID,
	BillAID,
	FMID,
	Status,
	Lang,
	Programs
    FROM 
	#Campaigns 
ORDER BY 
	DATEPART(yyyy, StartDate) DESC, 
        DATEPART(mm, StartDate) DESC, 
        DATEPART(dd, StartDate) DESC, 
        CampaignID DESC

DROP TABLE #Campaigns
GO

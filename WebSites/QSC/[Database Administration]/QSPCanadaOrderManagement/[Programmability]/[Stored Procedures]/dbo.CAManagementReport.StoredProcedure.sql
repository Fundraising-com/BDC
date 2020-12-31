USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CAManagementReport]    Script Date: 06/07/2017 09:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CAManagementReport] @FmId Int, 
						@CAStartDate DateTime, 
						@CAEndDate DateTime, 
						@DateApprovedFrom DateTime, 
						@DateApprovedTo DateTime, 
						@CAStatus Int, 
						@CAProgram Int, 
						@SortBy Varchar(20)
AS
If @DateApprovedFrom = '01/01/1995'
Begin
	Set @DateApprovedFrom = null;
End

If @DateApprovedTo = '01/01/1995'
Begin
	Set @DateApprovedTo = null;
End


CREATE TABLE #AllAccounts  (
			FMID			INT, 
			LastName		VARCHAR(50), 
			FirstName		VARCHAR(50), 
			CampaignId		INT, 
			New_Renewed		Varchar(10),
			OldCA			int,
			CampaignStatus		VARCHAR(20), 
            StartDate		VARCHAR(10), 
			EndDate		VARCHAR(10), 
			SuppliesDeliveryDate	VARCHAR(10), 
			SuppliesShipDate	VARCHAR(10),
			EstimatedGross		NUMERIC(10,2), 
			Student			INT,
			Staff			INT,
			SuppliesShipto		VARCHAR(20),
			Programs		VARCHAR(max),
            DateModified		VARCHAR(10), 
			UserIDModified		INT, 
			UserName		VARCHAR(50),
			AccountId		INT, 
			AccountName		VARCHAR(50), 
			City			VARCHAR(50), 
            IsStaffOrder		VARCHAR(1), 
			OrderID			INT, 
			DateReceived		VARCHAR(10), 
			OrderType		VARCHAR(20),
			CACount		INT,
			TotalEstimate		NUMERIC(10,2), 
			TotalCount		INT,
			TotalGross		NUMERIC(10,2),
			CookieDoughDeliveryDate VARCHAR(19),
			ApprovedStatusDate VARCHAR(20),
			OnlineOnlyPrograms BIT,
			CampaignContactFirstName VARCHAR(20),
			CampaignContactLastName VARCHAR(30),
			CampaignContactEmail VARCHAR(60),
			CampaignContactFunction VARCHAR(50),
			CampaignContactPhoneNumber VARCHAR(50),
			CampaignContactPhoneType INT,
			CampaignContactBestTimeToCall VARCHAR(2000),
			ShippingAddress1 VARCHAR(50),
			ShippingAddress2 VARCHAR(50),
			ShippingAddressCity VARCHAR(50),
			ShippingAddressProvince VARCHAR(10),
			ShippingAddressPostalCode VARCHAR(7),
			DMID			INT, 
			DMLastName		VARCHAR(50), 
			DMFirstName		VARCHAR(50), 
			)

DECLARE @DistinctCAWithGrossByFM TABLE(
			FMId			INT,
			CA			INT,
			EstimatedGross		Numeric(10,2)
			)

DECLARE @CACountBYFM TABLE(
			Id			INT IDENTITY,
			FMId			INT,
			CNT			INT,
			TotalGross		Numeric(10,2)
			)


DECLARE @F	INT
DECLARE @C	INT
DECLARE @rCount INT
DECLARE @EstimatedGrossByFM NUMERIC(10,2)

DECLARE @CurrentSeasonStartDate DateTime
DECLARE @CurrentSeasonEndDate  DateTime

SELECT @CurrentSeasonStartDate=StartDate,@CurrentSeasonEndDate=EndDate     
FROM QSPCanadacommon.dbo.Season 
WHERE @CAStartDate Between StartDate And EndDate
AND Season='Y' --Fiscal Year

SELECT	DISTINCT 
		billtoaccountid,
		id,
		Case IsNull(IsStaffOrder,0)
			When 1 Then 'Y' 
			Else 'N' 
		End  IsStaffOrder 
INTO	#OldAccounts
FROM	QSPCanadaCommon.dbo.campaign camp
JOIN	QSPCanadaCommon..FieldManager fm
			ON	fm.FMID = camp.FMID 
JOIN	QSPCanadaCommon..FieldManager dm
			ON	dm.FMID = fm.DMID
JOIN	QSPCanadaCommon..FieldManager fmAccountOwner
			ON	fmAccountOwner.FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(mm, 6, GETDATE()))
JOIN	QSPCanadaCommon..FieldManager dmAccountOwner
			ON	dmAccountOwner.FMID = fmAccountOwner.DMID
WHERE	camp.startdate  <  @CAStartDate
AND		camp.status <> 37005
AND		(fm.FMID = ISNULL(@FMID, fm.FMID) OR @FMID = fmAccountOwner.FMID OR dm.FMID = ISNULL(@FMID, dm.FMID) OR @FMID = dmAccountOwner.FMID)
AND EXISTS ( Select 1
			 FROM qspcanadacommon..campaign c
			 Where c.id=camp.id
			 AND  camp.startdate >= (Select startdate 
								 FROM qspcanadacommon.dbo.Season 
								 Where FiscalYear in (Select FiscalYear-1 FROM qspcanadacommon.dbo.Season S1 
													  Where startdate = @CurrentSeasonStartDate and enddate = @CurrentSeasonEndDate )	 
								 AND season='Y')
		     AND  camp.Enddate <=    (Select Enddate 
									From qspcanadacommon.dbo.Season 
									Where FiscalYear in (Select FiscalYear-1 FROM qspcanadacommon.dbo.Season S1 
														Where startdate = @CurrentSeasonStartDate And enddate = @CurrentSeasonEndDate )	 
									AND season='Y')
		      ) 

INSERT INTO #AllAccounts
SELECT		fm.FMID AS FMID, 
			fm.LastName AS LastName, 
			fm.FirstName AS FirstName, 
			camp.ID AS CampaignId, 'N',0,
			CD1.Description,
			--UPPER(SUBSTRING(CD1.Description,19,1))+SUBSTRING(CD1.Description,20,14)  AS CampaignStatus, 
			CONVERT(VARCHAR(10),camp.StartDate,1) AS StartDate, 
			CONVERT(VARCHAR(10),camp.EndDate,1)  AS EndDate, 
			CASE ISNULL(camp.SuppliesDeliveryDate, '01/01/1995')
			WHEN  '01/01/1995' THEN Null
			ELSE 	CONVERT(VARCHAR(10),camp.SuppliesDeliveryDate,1) 
			END AS SuppliesDeliveryDate, 
			--CONVERT(VARCHAR(10),QspCanadaOrderManagement.dbo.UDF_FS_MAX_SHIPMENT_DATE (C.ID ),1) SuppliesShipDate,
			(	SELECT		CONVERT(VARCHAR(10), MAX(ship.ShipmentDate), 1)
				FROM		Batch batch
				JOIN		CustomerOrderHeader coh
								ON	coh.OrderBatchID = batch.ID
								AND	coh.OrderBatchDate = batch.Date
				JOIN		CustomerOrderDetail cod
								ON	coh.Instance = cod.CustomerOrderHeaderInstance
				JOIN		Shipment ship
								ON	ship.ID = cod.ShipmentID
				WHERE		batch.OrderQualifierID = 39007 --39007: Field Supplies
				AND			ship.ShipmentDate IS NOT NULL
				AND			batch.CampaignID = camp.ID
			) SuppliesShipDate,
			camp.EstimatedGross AS EstimatedGross, 
			camp.NumberOfParticipants,
			camp.NumberOfStaff, 
			CASE camp.SuppliesShipToCampaignContactId
				WHEN 63001 THEN 'FM'
				WHEN 63002 THEN 'School'
				WHEN 63003 THEN 'Other'
				ELSE ''
			END,	
			QspCanadaOrderManagement.dbo.Udf_ProgramsByCampaign(camp.ID) Programs,
			CONVERT(VARCHAR(10),camp.DateModified,1) AS DateModified, 
			camp.UserIDModified AS UserIDModified, 
			U.FirstName+' '+U.LastName UserName,
			A.ID AS AccountId, 
			A.Name AS AccountName, 
			A.City AS City, 
			(CASE camp.IsStaffOrder 
				WHEN 1 THEN 'Y'
				WHEN 0  THEN 'N'
			END ) AS IsStaffOrder, 		
			Null,
			Null,
			Null,
			0,0,0,0,
			CASE ISNULL(camp.CookieDoughDeliveryDate, '01/01/1995')
				WHEN  '01/01/1995' THEN Null
				ELSE 	CONVERT(VARCHAR(19),camp.CookieDoughDeliveryDate,120) 
			END AS CookieDoughDeliveryDate,
			CASE ISNULL(camp.ApprovedStatusDate, '01/01/1995')
				WHEN  '01/01/1995' THEN Null
				ELSE 	CONVERT(VARCHAR(10),camp.ApprovedStatusDate,101) 
			END AS ApprovedStatusDate,
			camp.OnlineOnlyPrograms,
			cont.FirstName CampaignContactFirstName,
			cont.LastName CampaignContactLastName,
			cont.Email CampaignContactEmail,
			cont.[Function] CampaignContactFunction,
			ph.PhoneNumber CampaignContactPhoneNumber,
			ph.[Type] CampaignContactPhoneType,
			ph.BestTimeToCall CampaignContactBestTimeToCall,
			adShip.street1 ShippingAddress1,
			adShip.street2 ShippingAddress2,
			adShip.city ShippingAddressCity,
			adShip.stateProvince ShippingAddressProvince,
			adShip.postal_code ShippingAddressPostalCode,
			dm.FMID,
			dm.LastName, 
			dm.FirstName
FROM		QSPCanadaCommon.dbo.Campaign camp
JOIN		QSPCanadaCommon.dbo.CodeDetail CD1 ON CD1.Instance = camp.Status
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID 
JOIN		QSPCanadaCommon..FieldManager dm
				ON	dm.FMID = fm.DMID
JOIN		QSPCanadaCommon..FieldManager fmAccountOwner
				ON	fmAccountOwner.FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(mm, 6, GETDATE()))
JOIN		QSPCanadaCommon..FieldManager dmAccountOwner
				ON	dmAccountOwner.FMID = fmAccountOwner.DMID
JOIN		QSPCanadaCommon.dbo.CAccount A ON camp.ShipToAccountID = A.ID
LEFT JOIN	QSPCanadaCommon.dbo.CUserProfile U ON camp.UserIDModified = U.Instance 
LEFT JOIN	QSPCanadaCommon..Contact cont ON cont.Id = camp.ShipToCampaignContactID
LEFT JOIN	QSPCanadaCommon..Phone ph ON ph.PhoneListID = cont.PhoneListID
LEFT JOIN	QSPCanadaCommon..Address adShip ON adShip.AddressListID = A.AddressListID AND adShip.address_type = 54001
WHERE		(fm.FMID = ISNULL(@FMID, fm.FMID) OR @FMID = fmAccountOwner.FMID OR dm.FMID = ISNULL(@FMID, dm.FMID) OR @FMID = dmAccountOwner.FMID)
AND			CAST(CONVERT(VARCHAR(10),camp.StartDate,101)  AS DATETIME)  >=  ISNULL(CAST(@CAStartDate AS DATETIME),  CAST(CONVERT(VARCHAR(10),camp.StartDate,101)  AS DATETIME) )
AND			CAST(CONVERT(VARCHAR(10),camp.StartDate,101)  AS DATETIME)  <=   ISNULL(CAST(@CAEndDate  AS DATETIME),  CAST(CONVERT(VARCHAR(10), camp.StartDate,101)  AS DATETIME) )
AND			camp.STATUS = ISNULL(@CAStatus,camp.STATUS)
AND			IsNull(CAST(CONVERT(VARCHAR(10),camp.ApprovedStatusDate,101)  AS DATETIME) ,'01/01/1995') >=  ISNULL(@DateApprovedFrom,  '01/01/1995') 
AND			IsNull(CAST(CONVERT(VARCHAR(10),camp.ApprovedStatusDate,101)  AS DATETIME) ,'01/01/2050') <=  ISNULL(@DateApprovedTo  ,  '01/01/2050') 
AND			camp.Id In (SELECT CampaignId FROM QSPCanadaCommon.dbo.CampaignProgram 
	   	           WHERE ProgramID = ISNULL(@CAProgram, ProgramID) and DeletedTF=0 AND OnlineOnly = 0)	

--If there was a CA for that account (Staff/Regular) update flag
UPDATE #AllAccounts 
SET New_Renewed ='R', OldCA=#OldAccounts.id
FROM #OldAccounts
WHERE #OldAccounts.BilltoAccountId=#AllAccounts.AccountId
AND #AllAccounts.IsStaffOrder = #OldAccounts.IsStaffOrder

--To get Estimated gross for Distinct CAs
INSERT INTO @DistinctCAWithGrossByFM
SELECT DISTINCT  FMId, CampaignId ,EstimatedGross  FROM #AllAccounts 

INSERT INTO @CACountBYFM
SELECT FMID, COUNT(*),SUM(Estimatedgross) FROM @DistinctCAWithGrossByFM GROUP  BY FMId

DELETE  FROM @CACountBYFM WHERE ISNULL(Cnt ,0)=0

SELECT @rCount = COUNT (*) FROM @CACountBYFM	

WHILE @rCount > 0
BEGIN
	SELECT @F= FMID, @C=CNT, @EstimatedGrossByFM = TotalGross FROM @CACountBYFM WHERE Id = @rCount

	UPDATE #AllAccounts SET CACount = @C, TotalEstimate =@EstimatedGrossByFM
	WHERE FMId = @F
	
SET @rCount=@rCount-1
END

UPDATE #AllAccounts SET TotalCount = (SELECT SUM(CNT)  FROM @CACountBYFM), TotalGross = (SELECT SUM(TotalGross)  FROM @CACountBYFM)
			

IF ( @SortBy ='FM'  OR (ISNULL(@SortBy,'@')='@' OR @SortBy=''))
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY LastName,FirstName 

END
IF @SortBy = 'ORDER ID'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY OrderId  Desc

END	
IF @SortBy = 'DELIVERY DATE'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY CAST(SuppliesDeliveryDate AS DATETIME) Desc

END	
IF @SortBy = 'ACCOUNT'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY AccountName

END	
IF @SortBy = 'START DATE'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY CAST(StartDate AS DATETIME)

END	
IF @SortBy = 'END DATE'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY CAST(EndDate AS DATETIME)

END	
IF @SortBy = 'STAFF ORDER'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY IsStaffOrder

END	
IF @SortBy = 'RENEWED CA'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY 5

END	
IF @SortBy = 'ONLINE ONLY'
BEGIN
	SELECT  * FROM #AllAccounts ORDER BY OnlineOnlyPrograms

END	
Drop Table #OldAccounts
Drop Table #AllAccounts
GO
USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchTitleForRemit]    Script Date: 06/07/2017 09:20:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SwitchTitleForRemit] 

@TitleCode varchar(4),
@NewFulfillmentHouseNbr int,
@RunID int

AS
DECLARE @NewRemitBatchID int
DECLARE @CustomerRemitHistoryInstance int
DECLARE @sqlStatement varchar(1024)


IF @RunID <> 0
BEGIN
	SELECT @NewRemitBatchID=ID FROM RemitBatch WHERE RunID=@RunID AND FulfillmentHouseNbr=@NewFulfillmentHouseNbr
END
ELSE
BEGIN
	SELECT @NewRemitBatchID= MAX(ID) FROM RemitBatch WHERE Status='42000' AND FulfillmentHouseNbr=@NewFulfillmentHouseNbr

	IF(COALESCE(@NewRemitBatchID, 0) = 0)
	BEGIN
		CREATE TABLE #tempinst
			(
				 NextInstance int
			)
		INSERT INTO #tempinst EXEC qspcanadaordermanagement..InsertNextInstance 17
		
		SELECT @NewRemitBatchID = NextInstance FROM #tempinst
		DROP TABLE #tempinst

		INSERT RemitBatch(ID, Date, Status, FulfillmentHouseNbr, UserIDChanged,DateChanged) 
				values(@NewRemitBatchID, '1/1/95', 42000, @NewFulfillmentHouseNbr, 'ADMI', GetDate())
	END
END



IF @RunID <> 0
BEGIN
	SET @sqlStatement = 'DECLARE c1 CURSOR FOR 	SELECT  	 CustomerRemitHistoryInstance 
								FROM	   	CustomerOrderDetailRemitHistory codrh,
										RemitBatch rb
								WHERE	codrh.RemitBatchID = rb.ID
										AND rb.RunID = ' + cast(@RunID as varchar) 
										+' AND codrh.RemitCode = ''' + @TitleCode + ''''
END
ELSE
BEGIN

	SET @sqlStatement = 'DECLARE c1 CURSOR FOR 	SELECT  	CustomerRemitHistoryInstance 
								FROM	   	CustomerOrderDetailRemitHistory codrh,
										RemitBatch rb
								WHERE	codrh.RemitBatchID = rb.ID
										AND rb.status = 42000
										AND codrh.RemitCode = ''' + @TitleCode + '''
										AND	codrh.RemitBatchID <> ' + CONVERT(varchar, @NewRemitBatchID)
										-- 10/24/2006 - Ben : This eliminates the update for several subscriptions
										-- where it is unnecessary
END

exec (@sqlStatement)

OPEN c1
FETCH NEXT FROM c1 INTO @CustomerRemitHistoryInstance
WHILE @@fetch_status = 0
BEGIN

	UPDATE CustomerRemitHistory SET RemitBatchID=@NewRemitBatchID WHERE Instance = @CustomerRemitHistoryInstance

	-- 09/18/2006 - Ben : Added this because CHADDs use audit record in remit
	UPDATE		crha
	SET			crha.RemitBatchID = @NewRemitBatchID
	FROM		CustomerRemitHistoryAudit crha
	WHERE		crha.Instance = @CustomerRemitHistoryInstance
	AND			crha.AuditDate = 
	-- 10/24/2006 - Ben : Changed MAX for TOP 1 to avoid locking
				(SELECT		TOP 1
							crha2.AuditDate
				FROM		CustomerRemitHistoryAudit crha2
				WHERE		crha2.Instance = @CustomerRemitHistoryInstance
				ORDER BY	crha2.AuditDate DESC)

	UPDATE CustomerOrderDetailRemitHistory SET RemitBatchID=@NewRemitBatchID WHERE CustomerRemitHistoryInstance = @CustomerRemitHistoryInstance

	-- 09/18/2006 - Ben : Added this because CHADDs use audit record in remit
	UPDATE		codrha
	SET			codrha.RemitBatchID = @NewRemitBatchID
	FROM		CustomerOrderDetailRemitHistoryAudit codrha
	WHERE		codrha.CustomerRemitHistoryInstance = @CustomerRemitHistoryInstance
	AND			codrha.AuditDate = 
	-- 10/24/2006 - Ben : Changed MAX for TOP 1 to avoid locking
				(SELECT		TOP 1
							codrha2.AuditDate
				FROM		CustomerOrderDetailRemitHistoryAudit codrha2
				WHERE		codrha2.CustomerRemitHistoryInstance = @CustomerRemitHistoryInstance
				ORDER BY	codrha2.AuditDate DESC)

	FETCH NEXT FROM c1 INTO @CustomerRemitHistoryInstance
END
CLOSE c1
DEALLOCATE c1
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Batch_Update_Status]    Script Date: 06/07/2017 09:19:44 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- ============================================================
-- Author:		Jeff Miles
-- Create date: Jan/23/2006
-- Description:	Updates the status of a row from the Batch Table
-- ============================================================

CREATE PROCEDURE [dbo].[pr_Batch_Update_Status]
	@iOrderID			int,
	@iStatusInstance	int
AS

SET NOCOUNT ON

UPDATE	QSPCanadaOrderManagement..Batch
SET		StatusInstance = @iStatusInstance
WHERE	OrderId = @iOrderID
GO

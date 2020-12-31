USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_TVWeekOutsideBC_Fix]    Script Date: 06/07/2017 09:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jeff Miles
-- Create date: 05/06/2008
-- Description:	TV Week Magazine - Customer Outside BC Remit Test
-- =============================================
CREATE PROCEDURE [dbo].[pr_RemitTest_TVWeekOutsideBC_Fix] 
	
	@iRunID		int = 0

AS
CREATE TABLE	[#SubsToUpdate](
					[CustomerOrderHeaderInstance] [int] NOT NULL,
					[TransID] [int] NOT NULL) 

	INSERT INTO		[#SubsToUpdate]
				   ([CustomerOrderHeaderInstance]
				   ,[TransID])
	SELECT			cod.CustomerOrderHeaderInstance,
					cod.TransID
	FROM			CustomerRemitHistory crh
	JOIN			CustomerOrderDetailRemitHistory codrh
						ON	codrh.CustomerRemitHistoryInstance = crh.Instance
						AND	codrh.RemitCode = '11BD'
						AND	codrh.Status IN (42000, 42001)
	JOIN			CustomerOrderHeader coh
						ON	coh.Instance = codrh.CustomerOrderHeaderInstance
	JOIN			CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
						AND	cod.TransID = codrh.TransID
	JOIN			RemitBatch rb
						ON	rb.ID = codrh.RemitBatchID
						AND	rb.RunID = @iRunID
	WHERE			crh.State <> 'BC'

	UPDATE		cod
	SET			cod.StatusInstance = 517
	FROM		CustomerOrderDetail cod
	JOIN		#SubsToUpdate stu
					ON	stu.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
					AND	stu.TransID = cod.TransID

	DELETE		crh
	FROM		CustomerRemitHistory crh
	JOIN		CustomerOrderDetailRemitHistory codrh
					ON	codrh.CustomerRemitHistoryInstance = crh.Instance
					AND	codrh.RemitCode = '11BD'
					AND	codrh.Status IN (42000, 42001)
	JOIN		#SubsToUpdate stu
					ON	stu.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
					AND	stu.TransID = codrh.TransID
					
	DELETE		codrh
	FROM		CustomerOrderDetailRemitHistory codrh
	JOIN		#SubsToUpdate stu
					ON	stu.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
					AND	stu.TransID = codrh.TransID
	WHERE		codrh.RemitCode = '11BD'
	AND			codrh.Status IN (42000, 42001)
GO

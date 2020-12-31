USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_TVWeekOutsideBC]    Script Date: 06/07/2017 09:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jeff Miles
-- Create date: 05/06/2008
-- Description:	TV Week Magazine - Customer Outside BC Remit Test
-- =============================================
CREATE PROCEDURE [dbo].[pr_RemitTest_TVWeekOutsideBC] 
	
	@iRunID		int = 0

AS
	SELECT		codrh.CustomerOrderHeaderInstance,
				codrh.TransID,
				crh.FirstName,
				crh.LastName,
				codrh.TitleCode,
				codrh.MagazineTitle,
				codrh.Quantity,
				codrh.CatalogPrice,
				crh.Address1,
				COALESCE(crh.Address2, '') AS Address2,
				crh.City,
				crh.State AS Province,
				crh.Zip AS PostalCode,
				cod.CreationDate
	FROM		CustomerRemitHistory crh
	JOIN		CustomerOrderDetailRemitHistory codrh
					ON	codrh.CustomerRemitHistoryInstance = crh.Instance
					AND	codrh.RemitCode = '11BD'
					AND	codrh.Status IN (42000, 42001)
	JOIN		CustomerOrderHeader coh
					ON	coh.Instance = codrh.CustomerOrderHeaderInstance
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
					AND	cod.TransID = codrh.TransID
	JOIN		RemitBatch rb
					ON	rb.ID = codrh.RemitBatchID
					AND	rb.RunID = @iRunID
	WHERE		crh.State <> 'BC'
GO

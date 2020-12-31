USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatchCustomerOrderDetail_Delete]    Script Date: 06/07/2017 09:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will delete row(s) in the table 'LetterBatchCustomerOrderDetail'
-- Gets: @iLetterBatchID int
-- Gets: @iCustomerOrderHeaderInstance int
-- Gets: @iTransID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_LetterBatchCustomerOrderDetail_Delete]

	@iCustomerOrderHeaderInstance int,
	@iTransID int

AS

DELETE	lbcod
FROM	LetterBatchCustomerOrderDetail lbcod
WHERE	lbcod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		lbcod.TransID = @iTransID
GO

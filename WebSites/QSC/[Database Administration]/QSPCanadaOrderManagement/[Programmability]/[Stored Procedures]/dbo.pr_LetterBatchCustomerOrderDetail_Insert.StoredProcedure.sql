USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatchCustomerOrderDetail_Insert]    Script Date: 06/07/2017 09:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'LetterBatchCustomerOrderDetail'
-- Gets: @iLetterBatchID int
-- Gets: @iCustomerOrderHeaderInstance int
-- Gets: @iTransID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_LetterBatchCustomerOrderDetail_Insert]

	@iLetterBatchID int,
	@iCustomerOrderHeaderInstance int,
	@iTransID int

AS

INSERT [dbo].[LetterBatchCustomerOrderDetail]
(
	[LetterBatchID],
	[CustomerOrderHeaderInstance],
	[TransID]
)
VALUES
(
	@iLetterBatchID,
	@iCustomerOrderHeaderInstance,
	@iTransID
)
GO

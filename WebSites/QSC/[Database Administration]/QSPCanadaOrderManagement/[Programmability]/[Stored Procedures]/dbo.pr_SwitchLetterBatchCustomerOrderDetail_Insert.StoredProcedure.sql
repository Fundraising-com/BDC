USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterBatchCustomerOrderDetail_Insert]    Script Date: 06/07/2017 09:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'SwitchBatchLetterCustomerOrderDetail'
-- Gets: @iSwitchLetterBatchInstance int
-- Gets: @iCustomerOrderHeaderInstance int
-- Gets: @iTransID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_SwitchLetterBatchCustomerOrderDetail_Insert]
	@iSwitchLetterBatchInstance int,
	@iCustomerOrderHeaderInstance int,
	@iTransID int
AS
-- INSERT a new row in the table.
INSERT [dbo].[SwitchLetterBatchCustomerOrderDetail]
(
	[SwitchLetterBatchInstance],
	[CustomerOrderHeaderInstance],
	[TransID]
)
VALUES
(
	@iSwitchLetterBatchInstance,
	@iCustomerOrderHeaderInstance,
	@iTransID
)
GO

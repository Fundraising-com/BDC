USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterBatchCustomerOrderDetail_SelectOne]    Script Date: 06/07/2017 09:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'SwitchBatchLetterCustomerOrderDetail'
-- based on the Primary Key.
-- Gets: @iSwitchLetterBatchInstance int
-- Gets: @iCustomerOrderHeaderInstance int
-- Gets: @iTransID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_SwitchLetterBatchCustomerOrderDetail_SelectOne]
	@iSwitchLetterBatchInstance int,
	@iCustomerOrderHeaderInstance int,
	@iTransID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[SwitchLetterBatchInstance],
	[CustomerOrderHeaderInstance],
	[TransID]
FROM [dbo].[SwitchLetterBatchCustomerOrderDetail]
WHERE
	[SwitchLetterBatchInstance] = @iSwitchLetterBatchInstance
	AND [CustomerOrderHeaderInstance] = @iCustomerOrderHeaderInstance
	AND [TransID] = @iTransID
GO

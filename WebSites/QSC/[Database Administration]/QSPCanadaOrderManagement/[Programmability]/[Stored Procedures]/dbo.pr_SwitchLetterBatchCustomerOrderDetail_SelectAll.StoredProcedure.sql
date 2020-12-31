USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterBatchCustomerOrderDetail_SelectAll]    Script Date: 06/07/2017 09:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'SwitchBatchLetterCustomerOrderDetail'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_SwitchLetterBatchCustomerOrderDetail_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[SwitchLetterBatchInstance],
	[CustomerOrderHeaderInstance],
	[TransID]
FROM [dbo].[SwitchLetterBatchCustomerOrderDetail]
ORDER BY 
	[SwitchLetterBatchInstance] ASC
	, [CustomerOrderHeaderInstance] ASC
	, [TransID] ASC
GO

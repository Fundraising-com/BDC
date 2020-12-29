USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_Return_Ageing_By_Sale]    Script Date: 02/14/2014 13:09:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Return_Ageing_By_Sale] (@SalesID Integer, @DateWanted DATETIME) AS

declare @ageing VARCHAR(20)
set @ageing = dbo.fn_Return_Ageing_By_Sale(@SalesID, @DateWanted)
SELECT @ageing
GO

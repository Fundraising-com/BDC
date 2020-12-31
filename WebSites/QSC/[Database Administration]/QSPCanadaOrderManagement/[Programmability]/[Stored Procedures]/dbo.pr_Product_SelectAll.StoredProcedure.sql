USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Product_SelectAll]    Script Date: 06/07/2017 09:20:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Product_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Product_Instance],
	[Product_Code],
	[Product_Year],
	[Product_Season],
	[Alpha_Code],
	[Product_Name],
	[Product_Sort_Name],
	[Pub_Nbr],
	[Ages],
	[Internet],
	[Issue_Rcvd_Dt],
	[CoverReceived],
	[HighlightCover],
	[Featuring],
	[Status],
	[Comment],
	[CommentDate],
	[Category_Code],
	[Fulfill_House_Nbr],
	[Mail_Dt],
	[Auth_Form_Rtrn_Dt],
	[IssueDateUsed],
	[Logged_By],
	[Log_Dt],
	[Lang],
	[ProductLine],
	[DaysLeadTime],
	[VendorNumber],
	[VendorSiteName],
	[PayGroupLookUpCode],
	[TermsName],
	[Currency],
	[CountryCode],
	[Type],
	[UnitOfMeasure],
	[UOMConvFactor],
	[UnitWeight],
	[UnitCost],
	[OracleCode],
	[Prize_Level],
	[Prize_Level_Qty_Required]
FROM QspCanadaProduct..[Product]
ORDER BY 
	[Product_Instance] ASC
GO

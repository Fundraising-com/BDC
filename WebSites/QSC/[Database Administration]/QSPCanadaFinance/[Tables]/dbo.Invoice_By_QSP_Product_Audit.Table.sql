USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[Invoice_By_QSP_Product_Audit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_By_QSP_Product_Audit](
	[INVOICE_ID] [int] NOT NULL,
	[QSP_PRODUCT_LINE_ID] [int] NOT NULL,
	[PRODUCT_AMOUNT] [decimal](10, 2) NULL,
	[US_Postage_Amount] [numeric](10, 2) NULL
) ON [PRIMARY]
GO

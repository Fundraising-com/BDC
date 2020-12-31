USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[Invoice_Section_Audit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice_Section_Audit](
	[INVOICE_SECTION_ID] [int] NOT NULL,
	[INVOICE_ID] [int] NULL,
	[SECTION_TYPE_ID] [int] NULL,
	[TOTAL_TAX_INCLUDED] [decimal](10, 2) NULL,
	[TOTAL_TAX_EXCLUDED] [decimal](10, 2) NULL,
	[GROUP_PROFIT_RATE] [numeric](10, 6) NULL,
	[GROUP_PROFIT_AMOUNT] [decimal](10, 2) NULL,
	[TOTAL_TAXABLE_AMOUNT] [decimal](10, 2) NULL,
	[NET_BEFORE_TAX] [decimal](10, 2) NULL,
	[TOTAL_TAX_AMOUNT] [decimal](10, 2) NULL,
	[DUE_AMOUNT] [decimal](10, 2) NULL,
	[DateCreated] [datetime] NULL,
	[CreatedBy] [varchar](4) NULL,
	[US_Postage_Amount] [numeric](10, 2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[Invoice_Section_Tax_Audit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice_Section_Tax_Audit](
	[INVOICE_SECTION_TAX_ID] [int] NOT NULL,
	[INVOICE_SECTION_ID] [int] NULL,
	[TAX_ID] [int] NULL,
	[TAXABLE_AMOUNT] [decimal](10, 2) NULL,
	[TAX_RATE] [decimal](10, 2) NULL,
	[TAX_AMOUNT] [decimal](10, 2) NULL,
	[DateCreated] [datetime] NULL,
	[CreatedBy] [varchar](30) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[Invoice_Audit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice_Audit](
	[INVOICE_ID] [int] NOT NULL,
	[ACCOUNT_ID] [int] NULL,
	[ACCOUNT_TYPE_ID] [int] NULL,
	[ORDER_ID] [int] NULL,
	[INVOICE_DATE] [datetime] NULL,
	[INVOICE_DUE_DATE] [datetime] NULL,
	[INVOICE_AMOUNT] [decimal](10, 2) NULL,
	[FIRST_PRINT_DATE] [datetime] NULL,
	[NOTE_TO_PRINT] [varchar](100) NULL,
	[DATETIME_CREATED] [datetime] NULL,
	[DATETIME_MODIFIED] [datetime] NULL,
	[LAST_UPDATED_BY] [varchar](30) NULL,
	[COUNTRY_CODE] [varchar](10) NULL,
	[IS_PRINTED] [char](10) NULL,
	[DATETIME_APPROVED] [datetime] NULL,
	[INVOICE_EFFECTIVE_DATE] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

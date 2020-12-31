USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[INVOICE]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[INVOICE](
	[INVOICE_ID] [int] IDENTITY(31000,1) NOT NULL,
	[ACCOUNT_ID] [int] NULL,
	[ACCOUNT_TYPE_ID] [int] NULL,
	[ORDER_ID] [int] NULL,
	[INVOICE_DATE] [datetime] NULL,
	[INVOICE_DUE_DATE] [datetime] NULL,
	[INVOICE_AMOUNT] [numeric](10, 2) NULL,
	[FIRST_PRINT_DATE] [datetime] NULL,
	[NOTE_TO_PRINT] [varchar](100) NULL,
	[DATETIME_CREATED] [datetime] NULL,
	[DATETIME_MODIFIED] [datetime] NULL,
	[LAST_UPDATED_BY] [varchar](30) NULL,
	[COUNTRY_CODE] [varchar](10) NULL,
	[IS_PRINTED] [char](10) NULL,
	[DATETIME_APPROVED] [datetime] NULL,
	[INVOICE_EFFECTIVE_DATE] [datetime] NULL,
	[PRINTED_INVOICE_ID] [int] NULL,
 CONSTRAINT [PK_INVOICE] PRIMARY KEY CLUSTERED 
(
	[INVOICE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[INVOICE]  WITH CHECK ADD  CONSTRAINT [FK_INVOICE_PRINTEDINVOICE] FOREIGN KEY([PRINTED_INVOICE_ID])
REFERENCES [dbo].[INVOICE] ([INVOICE_ID])
GO
ALTER TABLE [dbo].[INVOICE] CHECK CONSTRAINT [FK_INVOICE_PRINTEDINVOICE]
GO

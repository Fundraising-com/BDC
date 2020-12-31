USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementInvoice]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementInvoice](
	[StatementInvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[StatementID] [int] NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StatementInvoice] PRIMARY KEY CLUSTERED 
(
	[StatementInvoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementInvoice]  WITH CHECK ADD  CONSTRAINT [FK_StatementInvoice_INVOICE] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[INVOICE] ([INVOICE_ID])
GO
ALTER TABLE [dbo].[StatementInvoice] CHECK CONSTRAINT [FK_StatementInvoice_INVOICE]
GO
ALTER TABLE [dbo].[StatementInvoice]  WITH CHECK ADD  CONSTRAINT [FK_StatementInvoice_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementInvoice] CHECK CONSTRAINT [FK_StatementInvoice_Statement]
GO
ALTER TABLE [dbo].[StatementInvoice] ADD  CONSTRAINT [DF_StatementInvoice_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

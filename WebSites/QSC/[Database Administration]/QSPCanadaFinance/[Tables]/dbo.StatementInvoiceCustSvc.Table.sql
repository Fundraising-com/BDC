USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementInvoiceCustSvc]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementInvoiceCustSvc](
	[StatementInvoiceCustSvcID] [int] IDENTITY(1,1) NOT NULL,
	[StatementID] [int] NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StatementInvoiceCustSvc] PRIMARY KEY CLUSTERED 
(
	[StatementInvoiceCustSvcID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementInvoiceCustSvc]  WITH CHECK ADD  CONSTRAINT [FK_StatementInvoiceCustSvc_INVOICE] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[INVOICE] ([INVOICE_ID])
GO
ALTER TABLE [dbo].[StatementInvoiceCustSvc] CHECK CONSTRAINT [FK_StatementInvoiceCustSvc_INVOICE]
GO
ALTER TABLE [dbo].[StatementInvoiceCustSvc]  WITH CHECK ADD  CONSTRAINT [FK_StatementInvoiceCustSvc_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementInvoiceCustSvc] CHECK CONSTRAINT [FK_StatementInvoiceCustSvc_Statement]
GO
ALTER TABLE [dbo].[StatementInvoiceCustSvc] ADD  CONSTRAINT [DF_StatementInvoiceCustSvc_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

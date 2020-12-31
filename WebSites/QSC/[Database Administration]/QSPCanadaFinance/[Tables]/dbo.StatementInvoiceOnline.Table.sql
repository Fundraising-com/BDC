USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementInvoiceOnline]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementInvoiceOnline](
	[StatementInvoiceOnlineID] [int] IDENTITY(1,1) NOT NULL,
	[StatementID] [int] NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StatementInvoiceOnline] PRIMARY KEY CLUSTERED 
(
	[StatementInvoiceOnlineID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementInvoiceOnline]  WITH CHECK ADD  CONSTRAINT [FK_StatementInvoiceOnline_INVOICE] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[INVOICE] ([INVOICE_ID])
GO
ALTER TABLE [dbo].[StatementInvoiceOnline] CHECK CONSTRAINT [FK_StatementInvoiceOnline_INVOICE]
GO
ALTER TABLE [dbo].[StatementInvoiceOnline]  WITH CHECK ADD  CONSTRAINT [FK_StatementInvoiceOnline_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementInvoiceOnline] CHECK CONSTRAINT [FK_StatementInvoiceOnline_Statement]
GO
ALTER TABLE [dbo].[StatementInvoiceOnline] ADD  CONSTRAINT [DF_StatementInvoiceOnline_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

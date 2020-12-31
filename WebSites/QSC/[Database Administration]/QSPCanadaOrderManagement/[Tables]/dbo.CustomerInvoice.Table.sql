USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerInvoice]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerInvoice](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[InvoiceNumber] [int] NOT NULL,
	[TotalAmount] [float] NOT NULL,
	[ItemCount] [int] NOT NULL,
	[TaxAmount] [float] NOT NULL,
	[ProcessingFeeAmount] [float] NOT NULL,
 CONSTRAINT [aaaaaCustomerInvoice_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC,
	[Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerInvoice] ADD  CONSTRAINT [DF__CustomerI__Custo__371B3697]  DEFAULT (0) FOR [CustomerOrderHeaderInstance]
GO
ALTER TABLE [dbo].[CustomerInvoice] ADD  CONSTRAINT [DF__CustomerIn__Date__380F5AD0]  DEFAULT ('1/1/1995') FOR [Date]
GO
ALTER TABLE [dbo].[CustomerInvoice] ADD  CONSTRAINT [DF__CustomerI__Invoi__39037F09]  DEFAULT (0) FOR [InvoiceNumber]
GO
ALTER TABLE [dbo].[CustomerInvoice] ADD  CONSTRAINT [DF__CustomerI__Total__39F7A342]  DEFAULT (0.0) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[CustomerInvoice] ADD  CONSTRAINT [DF__CustomerI__ItemC__3AEBC77B]  DEFAULT (0) FOR [ItemCount]
GO
ALTER TABLE [dbo].[CustomerInvoice] ADD  CONSTRAINT [DF__CustomerI__TaxAm__3BDFEBB4]  DEFAULT (0.0) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[CustomerInvoice] ADD  CONSTRAINT [DF__CustomerI__Proce__3CD40FED]  DEFAULT (0.0) FOR [ProcessingFeeAmount]
GO

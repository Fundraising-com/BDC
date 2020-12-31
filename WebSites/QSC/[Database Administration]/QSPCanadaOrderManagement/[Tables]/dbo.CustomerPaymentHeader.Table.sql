USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerPaymentHeader]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPaymentHeader](
	[Instance] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[InvoiceNumber] [int] NOT NULL,
	[PaymentBatchDate] [datetime] NOT NULL,
	[PaymentBatchID] [int] NOT NULL,
	[PaymentBatchSequence] [int] NOT NULL,
	[NextDetailTransID] [int] NOT NULL,
	[TotalAmount] [numeric](10, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[StatusInstance] [int] NOT NULL,
	[IsCreditCard] [bit] NOT NULL,
	[Signed] [varchar](1) NULL,
 CONSTRAINT [aaaaaCustomerPaymentHeader_PK] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Insta__0272642E]  DEFAULT (0) FOR [Instance]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Custo__03668867]  DEFAULT (0) FOR [CustomerOrderHeaderInstance]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Invoi__045AACA0]  DEFAULT (0) FOR [InvoiceNumber]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Payme__054ED0D9]  DEFAULT ('1/1/1995') FOR [PaymentBatchDate]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Payme__0642F512]  DEFAULT (0) FOR [PaymentBatchID]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Payme__0737194B]  DEFAULT (0) FOR [PaymentBatchSequence]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__NextD__082B3D84]  DEFAULT (0) FOR [NextDetailTransID]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Total__091F61BD]  DEFAULT (0.0) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__DateC__0A1385F6]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__UserI__0B07AA2F]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__DateC__0BFBCE68]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__UserI__0CEFF2A1]  DEFAULT (' ') FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[CustomerPaymentHeader] ADD  CONSTRAINT [DF__CustomerP__Statu__0DE416DA]  DEFAULT (0) FOR [StatusInstance]
GO

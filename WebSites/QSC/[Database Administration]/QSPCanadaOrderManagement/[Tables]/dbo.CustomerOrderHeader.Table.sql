USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerOrderHeader]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerOrderHeader](
	[Instance] [int] NOT NULL,
	[NextDetailTransID] [int] NULL,
	[AccountID] [int] NULL,
	[CustomerBillToInstance] [int] NULL,
	[StudentInstance] [int] NULL,
	[StatusInstance] [int] NULL,
	[FirstStatusInstance] [int] NULL,
	[TotalProcessingFee] [float] NULL,
	[TotalProcessingFeeA] [float] NULL,
	[ProcessingFeeTax] [float] NULL,
	[ProcessingFeeTaxA] [float] NULL,
	[ProcessingFeeTransID] [int] NULL,
	[OrderBatchDate] [datetime] NULL,
	[OrderBatchID] [int] NULL,
	[OrderBatchSequence] [int] NULL,
	[CreationDate] [datetime] NULL,
	[LastSentInvoiceDate] [datetime] NULL,
	[NumberInvoicesSent] [int] NULL,
	[ForceInvoice] [bit] NULL,
	[DelFlag] [bit] NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NULL,
	[Type] [int] NULL,
	[PaymentMethodInstance] [int] NULL,
	[CampaignID] [int] NULL,
	[TRTGenerationCode] [varchar](4) NULL,
	[ToteID] [int] NULL,
	[FormCode] [varchar](4) NULL,
 CONSTRAINT [aaaaaCustomerOrderHeader_PK] PRIMARY KEY NONCLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Insta__5A6472D4]  DEFAULT (0) FOR [Instance]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__NextD__5B58970D]  DEFAULT (0) FOR [NextDetailTransID]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Accou__5C4CBB46]  DEFAULT (0) FOR [AccountID]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Custo__5D40DF7F]  DEFAULT (0) FOR [CustomerBillToInstance]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Stude__5E3503B8]  DEFAULT (0) FOR [StudentInstance]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Statu__5F2927F1]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__First__601D4C2A]  DEFAULT (0) FOR [FirstStatusInstance]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Total__61117063]  DEFAULT (0.0) FOR [TotalProcessingFee]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Total__6205949C]  DEFAULT (0.0) FOR [TotalProcessingFeeA]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Proce__62F9B8D5]  DEFAULT (0.0) FOR [ProcessingFeeTax]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Proce__63EDDD0E]  DEFAULT (0.0) FOR [ProcessingFeeTaxA]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Proce__64E20147]  DEFAULT (0) FOR [ProcessingFeeTransID]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Order__65D62580]  DEFAULT ('1/1/1995') FOR [OrderBatchDate]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Order__66CA49B9]  DEFAULT (0) FOR [OrderBatchID]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Order__67BE6DF2]  DEFAULT (0) FOR [OrderBatchSequence]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Creat__68B2922B]  DEFAULT ('1/1/1995') FOR [CreationDate]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__LastS__69A6B664]  DEFAULT ('1/1/1995') FOR [LastSentInvoiceDate]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Numbe__6A9ADA9D]  DEFAULT (0) FOR [NumberInvoicesSent]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Chang__6B8EFED6]  DEFAULT (' ') FOR [ChangeUserID]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerO__Chang__6C83230F]  DEFAULT ('1/1/1995') FOR [ChangeDate]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF__CustomerOr__Type__6A50C1DA]  DEFAULT (null) FOR [Type]
GO
ALTER TABLE [dbo].[CustomerOrderHeader] ADD  CONSTRAINT [DF_CustomerOrderHeader_CampaignID]  DEFAULT (0) FOR [CampaignID]
GO

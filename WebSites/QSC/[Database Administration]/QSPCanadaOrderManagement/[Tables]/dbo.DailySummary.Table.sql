USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[DailySummary]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailySummary](
	[Date] [datetime] NOT NULL,
	[OrdersEnterred] [int] NOT NULL,
	[OrdersInError] [int] NOT NULL,
	[Invoice1OrderCount] [int] NOT NULL,
	[Invoice1ItemCount] [int] NOT NULL,
	[Invoice1OrderAmount] [float] NOT NULL,
	[BridgeItemCount] [int] NOT NULL,
	[BridgeAmount] [float] NOT NULL,
	[BridgeProcessingFeeAmount] [float] NOT NULL,
	[Invoice1TaxAmount] [float] NOT NULL,
	[Invoice1ProcessingFeeAmount] [float] NOT NULL,
	[Invoice2OrderCount] [int] NOT NULL,
	[Invoice2ItemCount] [int] NOT NULL,
	[Invoice2OrderAmount] [float] NOT NULL,
	[Invoice2TaxAmount] [float] NOT NULL,
	[Invoice2ProcessingFeeAmount] [float] NOT NULL,
	[Invoice3OrderCount] [int] NOT NULL,
	[Invoice3ItemCount] [int] NOT NULL,
	[Invoice3OrderAmount] [float] NOT NULL,
	[Invoice3TaxAmount] [float] NOT NULL,
	[Invoice3ProcessingFeeAmount] [float] NOT NULL,
	[Invoice1UOrderCount] [int] NULL,
 CONSTRAINT [aaaaaDailySummary_PK] PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumma__Date__1F0EA2DC]  DEFAULT ('1/1/1995') FOR [Date]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Order__2002C715]  DEFAULT (0) FOR [OrdersEnterred]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Order__20F6EB4E]  DEFAULT (0) FOR [OrdersInError]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__21EB0F87]  DEFAULT (0) FOR [Invoice1OrderCount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__22DF33C0]  DEFAULT (0) FOR [Invoice1ItemCount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__23D357F9]  DEFAULT (0.0) FOR [Invoice1OrderAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Bridg__24C77C32]  DEFAULT (0) FOR [BridgeItemCount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Bridg__25BBA06B]  DEFAULT (0.0) FOR [BridgeAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Bridg__26AFC4A4]  DEFAULT (0.0) FOR [BridgeProcessingFeeAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__27A3E8DD]  DEFAULT (0.0) FOR [Invoice1TaxAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__28980D16]  DEFAULT (0.0) FOR [Invoice1ProcessingFeeAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__298C314F]  DEFAULT (0) FOR [Invoice2OrderCount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__2A805588]  DEFAULT (0) FOR [Invoice2ItemCount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__2B7479C1]  DEFAULT (0.0) FOR [Invoice2OrderAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__2C689DFA]  DEFAULT (0.0) FOR [Invoice2TaxAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__2D5CC233]  DEFAULT (0.0) FOR [Invoice2ProcessingFeeAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__2E50E66C]  DEFAULT (0) FOR [Invoice3OrderCount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__2F450AA5]  DEFAULT (0) FOR [Invoice3ItemCount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__30392EDE]  DEFAULT (0.0) FOR [Invoice3OrderAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__312D5317]  DEFAULT (0.0) FOR [Invoice3TaxAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF__DailySumm__Invoi__32217750]  DEFAULT (0.0) FOR [Invoice3ProcessingFeeAmount]
GO
ALTER TABLE [dbo].[DailySummary] ADD  CONSTRAINT [DF_DailySumma_Invoice1UOr1__40]  DEFAULT (0) FOR [Invoice1UOrderCount]
GO

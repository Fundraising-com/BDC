USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Code] [varchar](4) NOT NULL,
	[Type] [varchar](1) NULL,
	[Name] [varchar](55) NULL,
	[Status] [varchar](1) NULL,
	[FulfillmentHouseCode] [varchar](4) NULL,
	[AcceptUSAOrders] [dbo].[Boolean_UDDT] NULL,
	[AcceptCanadaOrders] [dbo].[Boolean_UDDT] NULL,
	[AcceptForeignOrders] [dbo].[Boolean_UDDT] NULL,
	[IssuesPerYear] [int] NULL,
	[OddPricesAccepted] [dbo].[Boolean_UDDT] NULL,
	[Unlisted] [dbo].[Boolean_UDDT] NULL,
	[ExclusiveCode] [dbo].[Boolean_UDDT] NULL,
	[UMC] [varchar](4) NULL,
 CONSTRAINT [aaaaaProduct_PK] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__Code__21B6055D]  DEFAULT (' ') FOR [Code]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__Type__22AA2996]  DEFAULT (null) FOR [Type]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__Name__239E4DCF]  DEFAULT (null) FOR [Name]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__Status__24927208]  DEFAULT (null) FOR [Status]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__Fulfill__25869641]  DEFAULT (null) FOR [FulfillmentHouseCode]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__IssuesP__267ABA7A]  DEFAULT (0) FOR [IssuesPerYear]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__product__Exclusi__1229A90A]  DEFAULT (0) FOR [ExclusiveCode]
GO

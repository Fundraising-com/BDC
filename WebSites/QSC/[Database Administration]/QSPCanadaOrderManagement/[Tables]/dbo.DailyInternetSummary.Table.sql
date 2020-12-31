USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[DailyInternetSummary]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DailyInternetSummary](
	[Date] [datetime] NOT NULL,
	[OrdersEnterred] [int] NOT NULL,
	[OrdersInError] [int] NOT NULL,
	[UnitsInOrders] [int] NOT NULL,
	[BridgeItemCount] [int] NOT NULL,
	[BridgeAmount] [float] NOT NULL,
	[Originator] [varchar](50) NOT NULL,
	[OrderAmount] [real] NULL,
 CONSTRAINT [PK_DailyInternetSummary_1__26] PRIMARY KEY CLUSTERED 
(
	[Date] ASC,
	[Originator] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DailyInternetSummary] ADD  CONSTRAINT [DF__DailyInte__Order__2942188C]  DEFAULT (0.0) FOR [OrderAmount]
GO

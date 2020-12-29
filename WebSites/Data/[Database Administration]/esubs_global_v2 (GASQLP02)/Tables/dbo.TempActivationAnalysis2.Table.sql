USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[TempActivationAnalysis2]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TempActivationAnalysis2](
	[year] [varchar](4) NULL,
	[partner_id] [int] NULL,
	[partner_name] [varchar](100) NULL,
	[NumberOfActivations] [int] NULL,
	[NumberOfOrders] [int] NULL,
	[OrderTotal] [decimal](12, 2) NULL,
	[TotalQuantity] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

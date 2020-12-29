USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[order_profit_backup]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_profit_backup](
	[order_profit_id] [int] NOT NULL,
	[event_id] [int] NULL,
	[event_participation_id] [int] NULL,
	[order_id] [int] NULL,
	[order_date] [datetime] NULL,
	[item_price] [float] NULL,
	[profit] [float] NULL,
	[total_profit] [float] NULL,
	[payment_id] [int] NULL,
	[order_item_id] [int] NULL,
	[order_status_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
	[update_date] [datetime] NULL
) ON [PRIMARY]
GO

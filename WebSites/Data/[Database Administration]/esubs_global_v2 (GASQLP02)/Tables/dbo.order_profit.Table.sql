USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[order_profit]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_profit](
	[order_profit_id] [int] IDENTITY(1,1) NOT NULL,
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
	[update_date] [datetime] NULL,
 CONSTRAINT [PK_order_profit] PRIMARY KEY CLUSTERED 
(
	[order_profit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[order_profit] ADD  CONSTRAINT [DF_order_profit_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

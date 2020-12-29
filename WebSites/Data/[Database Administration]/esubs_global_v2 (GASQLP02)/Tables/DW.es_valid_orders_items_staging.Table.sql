USE [esubs_global_v2]
GO
/****** Object:  Table [DW].[es_valid_orders_items_staging]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [DW].[es_valid_orders_items_staging](
	[act] [int] NULL,
	[order_id] [int] NOT NULL,
	[order_item_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[price] [money] NULL,
	[total_amount] [money] NULL,
	[sub_total] [money] NULL,
	[tax] [money] NULL,
	[freight] [money] NULL,
	[handling_fee] [money] NULL,
	[redeemed_amount] [money] NULL,
	[supp_id] [int] NULL,
	[supp_name] [varchar](255) NULL,
	[first_name] [varchar](255) NULL,
	[last_name] [varchar](255) NULL,
	[email_address] [varchar](255) NULL,
	[event_id] [int] NULL,
	[item_type_id] [int] NULL,
	[product_id] [int] NULL,
	[product_desc] [varchar](255) NULL,
	[product_type_id] [int] NULL,
	[product_type_desc] [varchar](255) NULL,
	[create_date] [datetime] NULL,
	[store_id] [int] NULL,
	[IsRenewal] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[order_item_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [DW].[es_valid_orders_items_staging] ADD  DEFAULT ((0)) FOR [IsRenewal]
GO

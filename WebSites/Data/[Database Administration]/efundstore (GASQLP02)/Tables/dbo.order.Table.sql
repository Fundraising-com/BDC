USE [eFundstore]
GO
/****** Object:  Table [dbo].[order]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[order](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[shopping_cart_id] [int] NOT NULL,
	[online_user_id] [int] NOT NULL,
	[credit_card_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NULL,
	[random_number] [int] NOT NULL,
	[order_number]  AS (convert(varchar(10),[random_number]) + '-' + convert(varchar(15),[order_id])),
	[order_total] [numeric](9, 2) NOT NULL,
	[shipping_total] [numeric](9, 2) NOT NULL,
	[tax_total] [numeric](9, 2) NOT NULL,
	[order_submitted] [bit] NOT NULL,
	[date_created] [datetime] NOT NULL,
	[scheduled_delivery_date] [datetime] NOT NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_orders_random_number]  DEFAULT (rand() * 1000000) FOR [random_number]
GO
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_orders_order_submitted]  DEFAULT (0) FOR [order_submitted]
GO
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_orders_date_created]  DEFAULT (getdate()) FOR [date_created]
GO
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_orders_scheduled_delivery_date]  DEFAULT (dateadd(day,21,getdate())) FOR [scheduled_delivery_date]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[sales_item_to_add]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_item_to_add](
	[sales_item_to_add_no] [smallint] NOT NULL,
	[sale_to_add_id] [int] NOT NULL,
	[scratch_book_id] [int] NOT NULL,
	[service_type_id] [tinyint] NULL,
	[group_name] [text] NULL,
	[quantity_sold] [smallint] NOT NULL,
	[unit_price_sold] [decimal](15, 4) NOT NULL,
	[quantity_free] [smallint] NOT NULL,
	[suggested_coupons] [text] NULL,
	[sales_amount] [decimal](15, 4) NOT NULL,
	[paid_amount] [decimal](15, 4) NOT NULL,
	[adjusted_amount] [decimal](15, 4) NOT NULL,
	[discount_amount] [decimal](15, 4) NULL,
	[sales_commission_amount] [decimal](15, 4) NOT NULL,
	[sponsor_commission_amount] [decimal](15, 4) NOT NULL,
	[nb_units_sold] [decimal](15, 4) NULL,
	[manual_product_description] [varchar](255) NULL,
 CONSTRAINT [PK_sales_item_to_add] PRIMARY KEY CLUSTERED 
(
	[sales_item_to_add_no] ASC,
	[sale_to_add_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[sales_item_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sales_item_to_add_sale_to_add] FOREIGN KEY([sale_to_add_id])
REFERENCES [dbo].[sale_to_add] ([sale_to_add_id])
GO
ALTER TABLE [dbo].[sales_item_to_add] CHECK CONSTRAINT [FK_sales_item_to_add_sale_to_add]
GO
ALTER TABLE [dbo].[sales_item_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sales_item_to_add_scratch_book] FOREIGN KEY([scratch_book_id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[sales_item_to_add] CHECK CONSTRAINT [FK_sales_item_to_add_scratch_book]
GO
ALTER TABLE [dbo].[sales_item_to_add]  WITH CHECK ADD  CONSTRAINT [FK_sales_item_to_add_service_type] FOREIGN KEY([service_type_id])
REFERENCES [dbo].[service_type] ([service_type_id])
GO
ALTER TABLE [dbo].[sales_item_to_add] CHECK CONSTRAINT [FK_sales_item_to_add_service_type]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_quantity_sold]  DEFAULT (0) FOR [quantity_sold]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_unit_price_sold]  DEFAULT (0) FOR [unit_price_sold]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_quantity_free]  DEFAULT (0) FOR [quantity_free]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_sales_amount]  DEFAULT (0) FOR [sales_amount]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_paid_amount]  DEFAULT (0) FOR [paid_amount]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_adjusted_amount]  DEFAULT (0) FOR [adjusted_amount]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_discount_amount]  DEFAULT (0) FOR [discount_amount]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_sales_commission_amount]  DEFAULT (0) FOR [sales_commission_amount]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_sponsor_commission_amount]  DEFAULT (0) FOR [sponsor_commission_amount]
GO
ALTER TABLE [dbo].[sales_item_to_add] ADD  CONSTRAINT [DF_sales_item_to_add_nb_units_sold]  DEFAULT (0) FOR [nb_units_sold]
GO

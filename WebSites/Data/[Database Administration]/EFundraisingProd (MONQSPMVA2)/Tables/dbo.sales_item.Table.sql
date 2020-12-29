USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[sales_item]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_item](
	[sales_id] [int] NOT NULL,
	[sales_item_no] [smallint] NOT NULL,
	[scratch_book_id] [int] NOT NULL,
	[service_type_id] [tinyint] NULL,
	[product_class_id] [int] NULL,
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
	[profit_margin] [decimal](18, 0) NULL,
	[participant_id] [int] NULL,
 CONSTRAINT [PK_sales_item] PRIMARY KEY CLUSTERED 
(
	[sales_id] ASC,
	[sales_item_no] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[sales_item]  WITH CHECK ADD  CONSTRAINT [FK_sales_item_participant] FOREIGN KEY([participant_id])
REFERENCES [dbo].[participant] ([participant_id])
GO
ALTER TABLE [dbo].[sales_item] CHECK CONSTRAINT [FK_sales_item_participant]
GO
ALTER TABLE [dbo].[sales_item]  WITH CHECK ADD  CONSTRAINT [FK_sales_item_product_class] FOREIGN KEY([product_class_id])
REFERENCES [dbo].[product_class] ([product_class_id])
GO
ALTER TABLE [dbo].[sales_item] CHECK CONSTRAINT [FK_sales_item_product_class]
GO
ALTER TABLE [dbo].[sales_item]  WITH CHECK ADD  CONSTRAINT [FK_sales_item_sale] FOREIGN KEY([sales_id])
REFERENCES [dbo].[sale] ([sales_id])
GO
ALTER TABLE [dbo].[sales_item] CHECK CONSTRAINT [FK_sales_item_sale]
GO
ALTER TABLE [dbo].[sales_item]  WITH CHECK ADD  CONSTRAINT [FK_sales_item_scratch_book] FOREIGN KEY([scratch_book_id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[sales_item] CHECK CONSTRAINT [FK_sales_item_scratch_book]
GO
ALTER TABLE [dbo].[sales_item] ADD  CONSTRAINT [DF_sales_item_quantity_free]  DEFAULT (0) FOR [quantity_free]
GO
ALTER TABLE [dbo].[sales_item] ADD  CONSTRAINT [DF_sales_item_paid_amount]  DEFAULT (0) FOR [paid_amount]
GO
ALTER TABLE [dbo].[sales_item] ADD  CONSTRAINT [DF_sales_item_adjusted_amount]  DEFAULT (0) FOR [adjusted_amount]
GO
ALTER TABLE [dbo].[sales_item] ADD  CONSTRAINT [DF_sales_item_sales_commission_amount]  DEFAULT (0) FOR [sales_commission_amount]
GO
ALTER TABLE [dbo].[sales_item] ADD  CONSTRAINT [DF_sales_item_sponsor_commission_amount]  DEFAULT (0) FOR [sponsor_commission_amount]
GO

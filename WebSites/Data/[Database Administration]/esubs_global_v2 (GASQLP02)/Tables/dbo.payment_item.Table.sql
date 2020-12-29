USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_item]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment_item](
	[payment_item_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[payment_id] [int] NOT NULL,
	[qsp_order_detail_id] [int] NOT NULL,
	[order_detail_amount] [money] NOT NULL,
	[profit_percentage] [float] NOT NULL,
	[profit_amount] [money] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[profit_id] [int] NULL,
	[profit_range_id] [int] NULL,
 CONSTRAINT [PK_payment_item] PRIMARY KEY CLUSTERED 
(
	[payment_item_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[payment_item]  WITH CHECK ADD  CONSTRAINT [FK_payment_item_payment] FOREIGN KEY([payment_id])
REFERENCES [dbo].[payment] ([payment_id])
GO
ALTER TABLE [dbo].[payment_item] CHECK CONSTRAINT [FK_payment_item_payment]
GO

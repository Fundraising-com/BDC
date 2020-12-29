USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[sale_carrier_shipping_status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sale_carrier_shipping_status](
	[carrier_shipping_status_id] [tinyint] NOT NULL,
	[sales_id] [int] NOT NULL,
	[status_entry_date] [datetime] NULL,
 CONSTRAINT [PK_sale_carrier_shipping_status] PRIMARY KEY CLUSTERED 
(
	[carrier_shipping_status_id] ASC,
	[sales_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[sale_carrier_shipping_status]  WITH CHECK ADD  CONSTRAINT [FK_sale_carrier_shipping_status_carrier_shipping_status] FOREIGN KEY([carrier_shipping_status_id])
REFERENCES [dbo].[carrier_shipping_status] ([carrier_shipping_status_id])
GO
ALTER TABLE [dbo].[sale_carrier_shipping_status] CHECK CONSTRAINT [FK_sale_carrier_shipping_status_carrier_shipping_status]
GO
ALTER TABLE [dbo].[sale_carrier_shipping_status]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_carrier_shipping_status_sale] FOREIGN KEY([sales_id])
REFERENCES [dbo].[sale] ([sales_id])
GO
ALTER TABLE [dbo].[sale_carrier_shipping_status] CHECK CONSTRAINT [FK_sale_carrier_shipping_status_sale]
GO

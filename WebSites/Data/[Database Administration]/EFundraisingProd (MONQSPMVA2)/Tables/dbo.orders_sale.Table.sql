USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[orders_sale]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders_sale](
	[order_id] [int] NOT NULL,
	[sales_id] [int] NOT NULL,
	[date_created] [datetime] NOT NULL,
 CONSTRAINT [PK_orders_sale] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[sales_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[orders_sale]  WITH CHECK ADD  CONSTRAINT [FK_orders_sale_sale] FOREIGN KEY([sales_id])
REFERENCES [dbo].[sale] ([sales_id])
GO
ALTER TABLE [dbo].[orders_sale] CHECK CONSTRAINT [FK_orders_sale_sale]
GO
ALTER TABLE [dbo].[orders_sale] ADD  CONSTRAINT [DF_orders_sale_date_created]  DEFAULT (getdate()) FOR [date_created]
GO

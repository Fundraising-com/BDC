USE [eFundstore]
GO
/****** Object:  Table [dbo].[order_sale]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_sale](
	[order_id] [int] NOT NULL,
	[sale_id] [int] NOT NULL,
	[date_created] [datetime] NOT NULL,
 CONSTRAINT [PK_orders_sale] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[sale_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[order_sale] ADD  CONSTRAINT [DF_orders_sale_date_created]  DEFAULT (getdate()) FOR [date_created]
GO

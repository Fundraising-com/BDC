USE [eFundstore]
GO
/****** Object:  Table [dbo].[accounting_class_shipping_fee]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounting_class_shipping_fee](
	[accounting_class_id] [tinyint] NOT NULL,
	[min_amount] [numeric](10, 2) NOT NULL,
	[max_amount] [numeric](10, 2) NOT NULL,
	[shipping_fee] [tinyint] NOT NULL
) ON [PRIMARY]
GO

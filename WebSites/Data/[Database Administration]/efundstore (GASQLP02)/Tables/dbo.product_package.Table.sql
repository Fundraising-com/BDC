USE [eFundstore]
GO
/****** Object:  Table [dbo].[product_package]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_package](
	[product_id] [int] NOT NULL,
	[package_id] [int] NOT NULL,
	[display_order] [tinyint] NULL,
	[display] [bit] NOT NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_products_packages] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[package_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product_package] ADD  CONSTRAINT [DF_product_package_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

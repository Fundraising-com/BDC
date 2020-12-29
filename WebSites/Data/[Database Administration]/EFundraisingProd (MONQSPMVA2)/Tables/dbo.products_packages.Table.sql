USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[products_packages]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products_packages](
	[product_id] [int] NOT NULL,
	[package_id] [tinyint] NOT NULL,
	[display_order] [tinyint] NULL,
	[displayable] [bit] NOT NULL,
 CONSTRAINT [PK_products_packages] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[package_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[products_packages]  WITH CHECK ADD  CONSTRAINT [FK_products_packages_packages] FOREIGN KEY([package_id])
REFERENCES [dbo].[packages] ([package_id])
GO
ALTER TABLE [dbo].[products_packages] CHECK CONSTRAINT [FK_products_packages_packages]
GO
ALTER TABLE [dbo].[products_packages]  WITH NOCHECK ADD  CONSTRAINT [FK_products_packages_scratch_book] FOREIGN KEY([product_id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[products_packages] CHECK CONSTRAINT [FK_products_packages_scratch_book]
GO

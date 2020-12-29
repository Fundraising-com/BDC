USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[products_cultures]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products_cultures](
	[product_id] [int] NOT NULL,
	[culture_id] [tinyint] NOT NULL,
 CONSTRAINT [PK_products_cultures] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[culture_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[products_cultures]  WITH CHECK ADD  CONSTRAINT [FK_products_cultures_cultures] FOREIGN KEY([culture_id])
REFERENCES [dbo].[cultures] ([culture_id])
GO
ALTER TABLE [dbo].[products_cultures] CHECK CONSTRAINT [FK_products_cultures_cultures]
GO
ALTER TABLE [dbo].[products_cultures]  WITH NOCHECK ADD  CONSTRAINT [FK_products_cultures_scratch_book] FOREIGN KEY([product_id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[products_cultures] CHECK CONSTRAINT [FK_products_cultures_scratch_book]
GO

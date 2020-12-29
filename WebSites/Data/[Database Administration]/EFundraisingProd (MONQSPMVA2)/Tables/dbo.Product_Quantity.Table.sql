USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Product_Quantity]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Quantity](
	[Product_Quantity_ID] [int] IDENTITY(2,1) NOT FOR REPLICATION NOT NULL,
	[Scratch_Book_ID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_Product_Quantity] PRIMARY KEY NONCLUSTERED 
(
	[Product_Quantity_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product_Quantity]  WITH NOCHECK ADD  CONSTRAINT [FK_product_quantity_scratch_book] FOREIGN KEY([Scratch_Book_ID])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[Product_Quantity] CHECK CONSTRAINT [FK_product_quantity_scratch_book]
GO

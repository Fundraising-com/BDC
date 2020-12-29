USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Sale_Item]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EFO_Sale_Item](
	[Item_ID] [int] NOT NULL,
	[Sale_ID] [int] NOT NULL,
	[Quantity] [decimal](4, 0) NOT NULL,
 CONSTRAINT [PK_EFO_Sale_Item] PRIMARY KEY NONCLUSTERED 
(
	[Item_ID] ASC,
	[Sale_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EFO_Sale_Item]  WITH NOCHECK ADD  CONSTRAINT [fk_efosi_item_id] FOREIGN KEY([Item_ID])
REFERENCES [dbo].[EFO_Item] ([Item_ID])
GO
ALTER TABLE [dbo].[EFO_Sale_Item] CHECK CONSTRAINT [fk_efosi_item_id]
GO
ALTER TABLE [dbo].[EFO_Sale_Item]  WITH NOCHECK ADD  CONSTRAINT [fk_efosi_sale_id] FOREIGN KEY([Sale_ID])
REFERENCES [dbo].[EFO_Sale] ([Sale_ID])
GO
ALTER TABLE [dbo].[EFO_Sale_Item] CHECK CONSTRAINT [fk_efosi_sale_id]
GO

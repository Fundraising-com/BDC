USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Inventory_Adjustment]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory_Adjustment](
	[Inventory_Adjustment_ID] [int] IDENTITY(21,1) NOT FOR REPLICATION NOT NULL,
	[Inventory_Adjustment_Type_ID] [int] NOT NULL,
	[Scratch_Book_Id] [int] NOT NULL,
	[Adjustment_Date] [smalldatetime] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_Inventory_Adjustment] PRIMARY KEY NONCLUSTERED 
(
	[Inventory_Adjustment_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inventory_Adjustment]  WITH NOCHECK ADD  CONSTRAINT [fk_ia_inventory_adjustment_type_id] FOREIGN KEY([Inventory_Adjustment_Type_ID])
REFERENCES [dbo].[Inventory_Adjustment_Type] ([Inventory_Adjustment_Type_ID])
GO
ALTER TABLE [dbo].[Inventory_Adjustment] CHECK CONSTRAINT [fk_ia_inventory_adjustment_type_id]
GO
ALTER TABLE [dbo].[Inventory_Adjustment]  WITH NOCHECK ADD  CONSTRAINT [FK_inventory_adjustment_scratch_book] FOREIGN KEY([Scratch_Book_Id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[Inventory_Adjustment] CHECK CONSTRAINT [FK_inventory_adjustment_scratch_book]
GO

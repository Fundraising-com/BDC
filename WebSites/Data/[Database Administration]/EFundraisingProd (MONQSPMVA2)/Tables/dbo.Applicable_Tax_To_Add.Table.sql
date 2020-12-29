USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Applicable_Tax_To_Add]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Applicable_Tax_To_Add](
	[Tax_Code] [varchar](4) NOT NULL,
	[Sale_To_Add_ID] [int] NOT NULL,
	[Tax_Amount] [decimal](15, 4) NOT NULL,
 CONSTRAINT [PK_Applicable_Tax_To_Add] PRIMARY KEY NONCLUSTERED 
(
	[Tax_Code] ASC,
	[Sale_To_Add_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Applicable_Tax_To_Add]  WITH NOCHECK ADD  CONSTRAINT [FK_applicable_tax_to_add_sale_to_add] FOREIGN KEY([Sale_To_Add_ID])
REFERENCES [dbo].[sale_to_add] ([sale_to_add_id])
GO
ALTER TABLE [dbo].[Applicable_Tax_To_Add] CHECK CONSTRAINT [FK_applicable_tax_to_add_sale_to_add]
GO
ALTER TABLE [dbo].[Applicable_Tax_To_Add]  WITH CHECK ADD  CONSTRAINT [fk_ATTA_Tax_Code] FOREIGN KEY([Tax_Code])
REFERENCES [dbo].[Tax_Table] ([Tax_Code])
GO
ALTER TABLE [dbo].[Applicable_Tax_To_Add] CHECK CONSTRAINT [fk_ATTA_Tax_Code]
GO

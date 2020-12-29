USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Campaign_Image]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Campaign_Image](
	[Campaign_Image_ID] [int] IDENTITY(4,1) NOT FOR REPLICATION NOT NULL,
	[Image_Catalog_Path] [varchar](50) NOT NULL,
	[Image_Catalog_Path_Sel] [varchar](50) NOT NULL,
	[Catalog_Category_ID] [int] NOT NULL,
	[Is_Personalized] [bit] NOT NULL,
 CONSTRAINT [PK_EFO_Campaign_Image] PRIMARY KEY NONCLUSTERED 
(
	[Campaign_Image_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EFO_Campaign_Image]  WITH NOCHECK ADD  CONSTRAINT [fk_efoci_catalog_category_id] FOREIGN KEY([Catalog_Category_ID])
REFERENCES [dbo].[EFO_Catalog_Category] ([Catalog_Category_ID])
GO
ALTER TABLE [dbo].[EFO_Campaign_Image] CHECK CONSTRAINT [fk_efoci_catalog_category_id]
GO

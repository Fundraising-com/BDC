USE [eFundweb]
GO
/****** Object:  Table [dbo].[Product_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Desc](
	[Product_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Long_Description] [varchar](1500) NULL,
	[Product_Name] [varchar](120) NULL,
 CONSTRAINT [PK_Product_Desc] PRIMARY KEY CLUSTERED 
(
	[Product_ID] ASC,
	[Language_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Product_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Product_Desc_Language] FOREIGN KEY([Language_ID])
REFERENCES [dbo].[Language] ([Language_ID])
GO
ALTER TABLE [dbo].[Product_Desc] CHECK CONSTRAINT [FK_Product_Desc_Language]
GO
ALTER TABLE [dbo].[Product_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Product_Desc_Product] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Product] ([Product_ID])
GO
ALTER TABLE [dbo].[Product_Desc] CHECK CONSTRAINT [FK_Product_Desc_Product]
GO

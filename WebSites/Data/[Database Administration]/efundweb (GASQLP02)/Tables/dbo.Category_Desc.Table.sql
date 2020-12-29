USE [eFundweb]
GO
/****** Object:  Table [dbo].[Category_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category_Desc](
	[Language_ID] [int] NOT NULL,
	[Category_ID] [int] NOT NULL,
	[Description] [varchar](1000) NULL,
	[Category_Name] [varchar](220) NULL,
 CONSTRAINT [PK_Category_Desc] PRIMARY KEY CLUSTERED 
(
	[Language_ID] ASC,
	[Category_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Category_Desc]  WITH NOCHECK ADD  CONSTRAINT [FK_Category_Desc_Category] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Category] ([Category_ID])
GO
ALTER TABLE [dbo].[Category_Desc] CHECK CONSTRAINT [FK_Category_Desc_Category]
GO
ALTER TABLE [dbo].[Category_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Category_Desc_Language] FOREIGN KEY([Language_ID])
REFERENCES [dbo].[Language] ([Language_ID])
GO
ALTER TABLE [dbo].[Category_Desc] CHECK CONSTRAINT [FK_Category_Desc_Language]
GO

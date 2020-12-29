USE [eFundweb]
GO
/****** Object:  Table [dbo].[Category_Package]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category_Package](
	[Package_ID] [int] NOT NULL,
	[Category_ID] [int] NOT NULL,
	[Package_Order] [int] NOT NULL,
	[Is_Displayable] [bit] NULL,
 CONSTRAINT [PK_Category_Package] PRIMARY KEY CLUSTERED 
(
	[Package_ID] ASC,
	[Category_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category_Package]  WITH NOCHECK ADD  CONSTRAINT [FK_Category_Package_Category] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Category] ([Category_ID])
GO
ALTER TABLE [dbo].[Category_Package] CHECK CONSTRAINT [FK_Category_Package_Category]
GO
ALTER TABLE [dbo].[Category_Package]  WITH NOCHECK ADD  CONSTRAINT [FK_Category_Package_Package] FOREIGN KEY([Package_ID])
REFERENCES [dbo].[Package] ([Package_ID])
GO
ALTER TABLE [dbo].[Category_Package] CHECK CONSTRAINT [FK_Category_Package_Package]
GO
ALTER TABLE [dbo].[Category_Package] ADD  CONSTRAINT [DF__Category___Is_Di__7D78A4E7]  DEFAULT (1) FOR [Is_Displayable]
GO

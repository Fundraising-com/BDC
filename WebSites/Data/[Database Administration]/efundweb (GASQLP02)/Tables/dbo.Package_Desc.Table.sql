USE [eFundweb]
GO
/****** Object:  Table [dbo].[Package_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Package_Desc](
	[Package_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Package_Desc] [varchar](2000) NULL,
	[Package_Title] [varchar](150) NULL,
 CONSTRAINT [PK_Package_Desc] PRIMARY KEY CLUSTERED 
(
	[Package_ID] ASC,
	[Language_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Package_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Package_Desc_Language] FOREIGN KEY([Language_ID])
REFERENCES [dbo].[Language] ([Language_ID])
GO
ALTER TABLE [dbo].[Package_Desc] CHECK CONSTRAINT [FK_Package_Desc_Language]
GO
ALTER TABLE [dbo].[Package_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Package_Desc_Package] FOREIGN KEY([Package_ID])
REFERENCES [dbo].[Package] ([Package_ID])
GO
ALTER TABLE [dbo].[Package_Desc] CHECK CONSTRAINT [FK_Package_Desc_Package]
GO

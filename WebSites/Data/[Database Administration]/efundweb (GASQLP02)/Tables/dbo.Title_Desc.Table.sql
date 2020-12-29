USE [eFundweb]
GO
/****** Object:  Table [dbo].[Title_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Title_Desc](
	[Title_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Description] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Title_Desc] PRIMARY KEY CLUSTERED 
(
	[Title_ID] ASC,
	[Language_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Title_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Title_Desc_Language] FOREIGN KEY([Language_ID])
REFERENCES [dbo].[Language] ([Language_ID])
GO
ALTER TABLE [dbo].[Title_Desc] CHECK CONSTRAINT [FK_Title_Desc_Language]
GO
ALTER TABLE [dbo].[Title_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Title_Desc_Title] FOREIGN KEY([Title_ID])
REFERENCES [dbo].[Title] ([Title_ID])
GO
ALTER TABLE [dbo].[Title_Desc] CHECK CONSTRAINT [FK_Title_Desc_Title]
GO

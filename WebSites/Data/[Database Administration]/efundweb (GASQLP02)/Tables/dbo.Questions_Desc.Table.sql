USE [eFundweb]
GO
/****** Object:  Table [dbo].[Questions_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Questions_Desc](
	[Language_ID] [int] NOT NULL,
	[Questions_Display] [varchar](2400) NOT NULL,
	[Questions_ID] [int] NOT NULL,
	[Error_Message] [varchar](600) NULL,
 CONSTRAINT [PK_Questions_Desc] PRIMARY KEY CLUSTERED 
(
	[Language_ID] ASC,
	[Questions_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Questions_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Desc_Language] FOREIGN KEY([Language_ID])
REFERENCES [dbo].[Language] ([Language_ID])
GO
ALTER TABLE [dbo].[Questions_Desc] CHECK CONSTRAINT [FK_Questions_Desc_Language]
GO
ALTER TABLE [dbo].[Questions_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Desc_Questions] FOREIGN KEY([Questions_ID])
REFERENCES [dbo].[Questions] ([Questions_ID])
GO
ALTER TABLE [dbo].[Questions_Desc] CHECK CONSTRAINT [FK_Questions_Desc_Questions]
GO

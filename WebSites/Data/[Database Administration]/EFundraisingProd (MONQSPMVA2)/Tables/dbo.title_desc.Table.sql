USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[title_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[title_desc](
	[title_id] [tinyint] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_title_desc] PRIMARY KEY CLUSTERED 
(
	[title_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[title_desc]  WITH CHECK ADD  CONSTRAINT [FK_title_desc_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[title_desc] CHECK CONSTRAINT [FK_title_desc_languages]
GO
ALTER TABLE [dbo].[title_desc]  WITH CHECK ADD  CONSTRAINT [FK_title_desc_title] FOREIGN KEY([title_id])
REFERENCES [dbo].[title] ([title_id])
GO
ALTER TABLE [dbo].[title_desc] CHECK CONSTRAINT [FK_title_desc_title]
GO

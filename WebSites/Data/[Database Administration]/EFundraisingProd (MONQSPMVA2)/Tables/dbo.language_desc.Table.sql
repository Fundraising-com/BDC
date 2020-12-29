USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[language_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[language_desc](
	[language_id] [tinyint] NOT NULL,
	[display_language_id] [tinyint] NOT NULL,
	[language_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_language_desc] PRIMARY KEY CLUSTERED 
(
	[language_id] ASC,
	[display_language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[language_desc]  WITH CHECK ADD  CONSTRAINT [FK_language_desc_display_languages] FOREIGN KEY([display_language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[language_desc] CHECK CONSTRAINT [FK_language_desc_display_languages]
GO
ALTER TABLE [dbo].[language_desc]  WITH CHECK ADD  CONSTRAINT [FK_language_desc_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[language_desc] CHECK CONSTRAINT [FK_language_desc_languages]
GO

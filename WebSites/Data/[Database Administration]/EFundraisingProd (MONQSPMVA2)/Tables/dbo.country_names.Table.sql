USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[country_names]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[country_names](
	[country_code] [char](2) NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[country_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_country_names] PRIMARY KEY CLUSTERED 
(
	[country_code] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[country_names]  WITH CHECK ADD  CONSTRAINT [FK_country_names_countries] FOREIGN KEY([country_code])
REFERENCES [dbo].[countries] ([country_code])
GO
ALTER TABLE [dbo].[country_names] CHECK CONSTRAINT [FK_country_names_countries]
GO
ALTER TABLE [dbo].[country_names]  WITH CHECK ADD  CONSTRAINT [FK_country_names_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[country_names] CHECK CONSTRAINT [FK_country_names_languages]
GO

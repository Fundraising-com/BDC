USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[culture]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[culture](
	[culture_code] [nvarchar](5) NOT NULL,
	[country_code] [nvarchar](2) NOT NULL,
	[language_code] [nvarchar](2) NOT NULL,
	[culture_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_culture] PRIMARY KEY CLUSTERED 
(
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[culture]  WITH CHECK ADD  CONSTRAINT [FK_culture_country] FOREIGN KEY([country_code])
REFERENCES [dbo].[country] ([country_code])
GO
ALTER TABLE [dbo].[culture] CHECK CONSTRAINT [FK_culture_country]
GO
ALTER TABLE [dbo].[culture]  WITH CHECK ADD  CONSTRAINT [FK_culture_language] FOREIGN KEY([language_code])
REFERENCES [dbo].[language] ([language_code])
GO
ALTER TABLE [dbo].[culture] CHECK CONSTRAINT [FK_culture_language]
GO

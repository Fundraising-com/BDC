USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[culture_country_name]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[culture_country_name](
	[culture_code] [nvarchar](5) NOT NULL,
	[country_code] [nvarchar](2) NOT NULL,
	[country_name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_culture_country_name] PRIMARY KEY CLUSTERED 
(
	[culture_code] ASC,
	[country_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[culture_country_name]  WITH CHECK ADD  CONSTRAINT [FK_culture_country_name_country] FOREIGN KEY([country_code])
REFERENCES [dbo].[country] ([country_code])
GO
ALTER TABLE [dbo].[culture_country_name] CHECK CONSTRAINT [FK_culture_country_name_country]
GO
ALTER TABLE [dbo].[culture_country_name]  WITH CHECK ADD  CONSTRAINT [FK_culture_country_name_culture] FOREIGN KEY([culture_code])
REFERENCES [dbo].[culture] ([culture_code])
GO
ALTER TABLE [dbo].[culture_country_name] CHECK CONSTRAINT [FK_culture_country_name_culture]
GO

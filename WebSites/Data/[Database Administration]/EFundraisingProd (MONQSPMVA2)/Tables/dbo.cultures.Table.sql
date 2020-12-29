USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[cultures]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cultures](
	[culture_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[language_id] [tinyint] NULL,
	[country_code] [char](2) NULL,
	[culture_name] [varchar](10) NOT NULL,
	[display_name] [varchar](50) NOT NULL,
	[culture_code] [char](6) NOT NULL,
	[iso_code] [char](3) NULL,
 CONSTRAINT [PK_cultures] PRIMARY KEY CLUSTERED 
(
	[culture_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[cultures]  WITH CHECK ADD  CONSTRAINT [FK_cultures_countries] FOREIGN KEY([country_code])
REFERENCES [dbo].[countries] ([country_code])
GO
ALTER TABLE [dbo].[cultures] CHECK CONSTRAINT [FK_cultures_countries]
GO
ALTER TABLE [dbo].[cultures]  WITH CHECK ADD  CONSTRAINT [FK_cultures_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[cultures] CHECK CONSTRAINT [FK_cultures_languages]
GO

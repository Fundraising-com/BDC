USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[package_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[package_desc](
	[package_id] [tinyint] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[package_name] [varchar](50) NOT NULL,
	[package_short_desc] [varchar](500) NOT NULL,
	[package_long_desc] [varchar](1000) NOT NULL,
	[package_extra_desc] [varchar](500) NULL,
	[package_small_img] [varchar](25) NULL,
	[package_large_img] [varchar](25) NULL,
	[page_url] [varchar](50) NULL,
 CONSTRAINT [PK_package_desc] PRIMARY KEY CLUSTERED 
(
	[package_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[package_desc]  WITH CHECK ADD  CONSTRAINT [FK_package_desc_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[package_desc] CHECK CONSTRAINT [FK_package_desc_languages]
GO
ALTER TABLE [dbo].[package_desc]  WITH CHECK ADD  CONSTRAINT [FK_package_desc_packages] FOREIGN KEY([package_id])
REFERENCES [dbo].[packages] ([package_id])
GO
ALTER TABLE [dbo].[package_desc] CHECK CONSTRAINT [FK_package_desc_packages]
GO

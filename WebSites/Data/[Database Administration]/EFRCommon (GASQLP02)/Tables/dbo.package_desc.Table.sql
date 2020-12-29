USE [EFRCommon]
GO
/****** Object:  Table [dbo].[package_desc]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[package_desc](
	[package_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[short_desc] [varchar](1000) NOT NULL,
	[long_desc] [varchar](4000) NOT NULL,
	[extra_desc] [varchar](4000) NULL,
	[page_name] [varchar](100) NULL,
	[image_name] [varchar](100) NULL,
	[template_id] [int] NULL,
	[page_title] [varchar](200) NULL,
	[image_alt_text] [varchar](100) NULL,
	[display_order] [int] NULL,
	[enabled] [bit] NULL,
	[configuration] [varchar](4000) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_package_desc] PRIMARY KEY CLUSTERED 
(
	[package_id] ASC,
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[package_desc]  WITH CHECK ADD  CONSTRAINT [FK_package_desc_package] FOREIGN KEY([package_id])
REFERENCES [dbo].[package] ([package_id])
GO
ALTER TABLE [dbo].[package_desc] CHECK CONSTRAINT [FK_package_desc_package]
GO

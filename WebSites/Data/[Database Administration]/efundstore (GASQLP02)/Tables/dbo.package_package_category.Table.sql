USE [eFundstore]
GO
/****** Object:  Table [dbo].[package_package_category]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[package_package_category](
	[package_id] [int] NOT NULL,
	[package_category_id] [nchar](10) NOT NULL,
	[display_order] [int] NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_package_package_category] PRIMARY KEY CLUSTERED 
(
	[package_id] ASC,
	[package_category_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[package_package_category] ADD  CONSTRAINT [DF_package_package_category_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

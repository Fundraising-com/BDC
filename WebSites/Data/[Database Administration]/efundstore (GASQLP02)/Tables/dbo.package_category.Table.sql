USE [eFundstore]
GO
/****** Object:  Table [dbo].[package_category]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[package_category](
	[package_category_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[package_category_title] [varchar](100) NULL,
	[package_category_desc] [varchar](500) NULL,
	[image_url] [varchar](500) NULL,
	[create_date] [datetime] NULL,
	[product_url] [varchar](200) NULL,
 CONSTRAINT [PK_package_category] PRIMARY KEY CLUSTERED 
(
	[package_category_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[package_category] ADD  CONSTRAINT [DF_package_category_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

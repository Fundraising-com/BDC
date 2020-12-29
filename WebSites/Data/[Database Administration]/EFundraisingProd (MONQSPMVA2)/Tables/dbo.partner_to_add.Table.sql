USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[partner_to_add]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_to_add](
	[partner_id] [nvarchar](50) NULL,
	[partner_type_id] [nvarchar](50) NULL,
	[partner_name] [nvarchar](50) NULL,
	[has_collection_site] [nvarchar](50) NULL,
	[guid] [nvarchar](50) NULL,
	[create_date] [nvarchar](50) NULL,
	[partner_path] [nvarchar](50) NULL,
	[esubs_url] [nvarchar](50) NULL,
	[Partner Name] [nvarchar](50) NULL,
	[image_url] [nvarchar](50) NULL
) ON [PRIMARY]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[partner_from_esubs_20080414]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner_from_esubs_20080414](
	[partner_id] [int] NOT NULL,
	[partner_type_id] [int] NOT NULL,
	[partner_name] [varchar](50) NOT NULL,
	[has_collection_site] [bit] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[partner_path] [varchar](255) NULL,
	[esubs_url] [varchar](255) NULL,
	[Partner Name] [varchar](255) NULL,
	[image_url] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

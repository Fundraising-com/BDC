USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_postal_address]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_postal_address](
	[postal_address_id] [int] IDENTITY(1,1) NOT NULL,
	[check_id] [int] NULL,
	[organization_id] [int] NULL,
	[address] [varchar](100) NULL,
	[city] [varchar](30) NULL,
	[zip] [varchar](10) NULL,
	[create_date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

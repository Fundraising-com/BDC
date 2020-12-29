USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_customer]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_customer](
	[lead_email] [varchar](50) NULL,
	[partner_id] [int] NULL,
	[organization_id] [int] NOT NULL,
	[lead_id] [int] NULL,
	[organizer_id] [int] NOT NULL,
	[org_creation_channel] [tinyint] NULL,
	[org_email] [varchar](75) NULL,
	[org_name] [varchar](50) NOT NULL,
	[campaign_id] [int] NOT NULL,
	[group_name] [varchar](1000) NULL,
	[campaign_type_id] [int] NULL,
	[season_id] [int] NULL,
	[participant_id] [int] NOT NULL,
	[part_creation_channel] [tinyint] NULL,
	[part_email] [varchar](50) NULL,
	[identification] [int] NULL,
	[part_name] [varchar](75) NULL,
	[part_default] [bit] NOT NULL,
	[supporter_id] [int] NULL,
	[supp_creation_channel] [tinyint] NULL,
	[supp_email] [varchar](75) NULL,
	[supp_name] [varchar](75) NULL,
	[supp_identification] [int] NULL,
	[supp_default] [bit] NULL,
	[customerid] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

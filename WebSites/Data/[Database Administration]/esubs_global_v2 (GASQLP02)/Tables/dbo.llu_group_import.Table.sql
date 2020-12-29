USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[llu_group_import]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[llu_group_import](
	[external_group_id] [int] NULL,
	[group_name] [varchar](8000) NULL,
	[sponsor_name] [varchar](8000) NULL,
	[address] [varchar](8000) NULL,
	[city] [varchar](8000) NULL,
	[state] [varchar](8000) NULL,
	[zip] [varchar](8000) NULL,
	[password] [varchar](8000) NULL,
	[email] [varchar](8000) NULL,
	[country] [varchar](8000) NULL,
	[phone] [varchar](8000) NULL,
	[member_count] [varchar](8000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

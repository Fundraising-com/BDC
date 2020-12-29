USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[kiwanisgroup]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kiwanisgroup](
	[group_id] [int] NOT NULL,
	[member_hierarchy_id] [int] NOT NULL,
	[parent_member_hierarchy_id] [int] NULL,
	[member_id] [int] NOT NULL,
	[creation_channel_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[unsubscribe] [bit] NOT NULL,
	[unsubscribe_date] [datetime] NULL,
 CONSTRAINT [PK_kiwanisgroup] PRIMARY KEY CLUSTERED 
(
	[member_hierarchy_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

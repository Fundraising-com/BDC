USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[kd_repair]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kd_repair](
	[member_hierarchy_id] [int] NULL,
	[member_id] [int] NULL,
	[email_address] [varchar](255) NULL,
	[new_member_id] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

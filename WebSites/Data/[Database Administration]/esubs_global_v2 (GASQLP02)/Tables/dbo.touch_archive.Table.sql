USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[touch_archive]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touch_archive](
	[touch_id] [int] IDENTITY(100000,1) NOT NULL,
	[event_participation_id] [int] NULL,
	[member_hierarchy_id] [int] NULL,
	[touch_info_id] [int] NOT NULL,
	[processed] [tinyint] NOT NULL,
	[create_date] [datetime] NULL,
	[msrepl_tran_version] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_touch_info]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_touch_info](
	[touch_info_id] [int] IDENTITY(1,1) NOT NULL,
	[touch_id] [int] NULL,
	[event_participation_id] [int] NULL,
	[processed] [bit] NULL,
	[rule_id] [smallint] NULL,
	[create_date] [datetime] NULL
) ON [PRIMARY]
GO

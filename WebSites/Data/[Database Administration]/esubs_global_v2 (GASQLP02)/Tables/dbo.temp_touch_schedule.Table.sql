USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_touch_schedule]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_touch_schedule](
	[identification] [int] NULL,
	[rule_id] [int] NULL,
	[datestamp] [datetime] NULL
) ON [PRIMARY]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[member_hierarchy_identification]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[member_hierarchy_identification](
	[member_hierarchy_id] [int] NULL,
	[organizer_id] [int] NULL,
	[participant_id] [int] NULL,
	[supporter_id] [int] NULL
) ON [PRIMARY]
GO

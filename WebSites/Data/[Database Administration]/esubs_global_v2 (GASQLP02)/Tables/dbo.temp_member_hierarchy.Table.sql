USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_member_hierarchy]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_member_hierarchy](
	[member_hierarchy_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_member_hierarchy_id] [int] NULL,
	[member_id] [int] NULL,
	[organization_id] [int] NULL,
	[participant_id] [int] NULL,
	[supporter_id] [int] NULL
) ON [PRIMARY]
GO

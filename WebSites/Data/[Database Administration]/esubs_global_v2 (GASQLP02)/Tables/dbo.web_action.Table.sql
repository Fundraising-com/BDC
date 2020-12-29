USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[web_action]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[web_action](
	[web_action_id] [int] IDENTITY(1,1) NOT NULL,
	[event_participation_id] [int] NOT NULL,
	[member_hierarchy_id] [int] NOT NULL,
	[type] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[value] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

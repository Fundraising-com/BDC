USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[featured_event_mainpage]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[featured_event_mainpage](
	[event_id] [int] NULL,
	[event_name] [varchar](255) NULL,
	[state] [varchar](2) NULL,
	[nb_member] [int] NULL,
	[nb_sub] [int] NULL,
	[amount] [money] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event_change]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[event_change](
	[event_id] [int] NOT NULL,
	[new_displayable_value] [bit] NOT NULL,
	[old_displayable_value] [bit] NOT NULL,
	[user] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[event_change] ADD  CONSTRAINT [DF_event_change_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event_participation]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[event_participation](
	[event_participation_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[event_id] [int] NOT NULL,
	[member_hierarchy_id] [int] NOT NULL,
	[participation_channel_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
	[salutation] [varchar](200) NULL,
	[coppa_month] [int] NULL,
	[coppa_year] [int] NULL,
	[agree_term_services] [bit] NOT NULL,
	[holiday_reminders] [bit] NOT NULL,
 CONSTRAINT [PK_event_participation] PRIMARY KEY CLUSTERED 
(
	[event_participation_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[event_participation]  WITH NOCHECK ADD  CONSTRAINT [FK_event_participation_event] FOREIGN KEY([event_id])
REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[event_participation] CHECK CONSTRAINT [FK_event_participation_event]
GO
ALTER TABLE [dbo].[event_participation]  WITH CHECK ADD  CONSTRAINT [FK_event_participation_member_hierarchy] FOREIGN KEY([member_hierarchy_id])
REFERENCES [dbo].[member_hierarchy] ([member_hierarchy_id])
GO
ALTER TABLE [dbo].[event_participation] CHECK CONSTRAINT [FK_event_participation_member_hierarchy]
GO
ALTER TABLE [dbo].[event_participation]  WITH CHECK ADD  CONSTRAINT [FK_event_participation_participation_channel] FOREIGN KEY([participation_channel_id])
REFERENCES [dbo].[participation_channel] ([participation_channel_id])
GO
ALTER TABLE [dbo].[event_participation] CHECK CONSTRAINT [FK_event_participation_participation_channel]
GO
ALTER TABLE [dbo].[event_participation] ADD  CONSTRAINT [DF_event_participation_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[event_participation] ADD  DEFAULT ((0)) FOR [agree_term_services]
GO
ALTER TABLE [dbo].[event_participation] ADD  DEFAULT ((0)) FOR [holiday_reminders]
GO

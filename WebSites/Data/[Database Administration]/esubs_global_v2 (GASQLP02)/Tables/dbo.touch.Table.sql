USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[touch]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touch](
	[touch_id] [int] IDENTITY(100000,1) NOT FOR REPLICATION NOT NULL,
	[event_participation_id] [int] NULL,
	[member_hierarchy_id] [int] NULL,
	[touch_info_id] [int] NOT NULL,
	[processed] [tinyint] NOT NULL,
	[create_date] [datetime] NULL,
	[msrepl_tran_version] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_touch] PRIMARY KEY CLUSTERED 
(
	[touch_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[touch]  WITH CHECK ADD  CONSTRAINT [FK_touch_event_participation] FOREIGN KEY([event_participation_id])
REFERENCES [dbo].[event_participation] ([event_participation_id])
GO
ALTER TABLE [dbo].[touch] CHECK CONSTRAINT [FK_touch_event_participation]
GO
ALTER TABLE [dbo].[touch]  WITH CHECK ADD  CONSTRAINT [FK_touch_member_hierarchy_id] FOREIGN KEY([member_hierarchy_id])
REFERENCES [dbo].[member_hierarchy] ([member_hierarchy_id])
GO
ALTER TABLE [dbo].[touch] CHECK CONSTRAINT [FK_touch_member_hierarchy_id]
GO
ALTER TABLE [dbo].[touch]  WITH CHECK ADD  CONSTRAINT [FK_touch_touch_info] FOREIGN KEY([touch_info_id])
REFERENCES [dbo].[touch_info] ([touch_info_id])
GO
ALTER TABLE [dbo].[touch] CHECK CONSTRAINT [FK_touch_touch_info]
GO
ALTER TABLE [dbo].[touch] ADD  CONSTRAINT [DF_touch_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[touch] ADD  DEFAULT (newid()) FOR [msrepl_tran_version]
GO

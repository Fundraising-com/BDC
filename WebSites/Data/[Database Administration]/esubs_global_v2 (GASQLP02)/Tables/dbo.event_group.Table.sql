USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event_group]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[event_group](
	[event_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_event_group] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC,
	[group_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[event_group]  WITH CHECK ADD  CONSTRAINT [FK_event_group_event] FOREIGN KEY([event_id])
REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[event_group] CHECK CONSTRAINT [FK_event_group_event]
GO
ALTER TABLE [dbo].[event_group]  WITH CHECK ADD  CONSTRAINT [FK_event_group_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[group] ([group_id])
GO
ALTER TABLE [dbo].[event_group] CHECK CONSTRAINT [FK_event_group_group]
GO
ALTER TABLE [dbo].[event_group] ADD  CONSTRAINT [DF_event_group_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

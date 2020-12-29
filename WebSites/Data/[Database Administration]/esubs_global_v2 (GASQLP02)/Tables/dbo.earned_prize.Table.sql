USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[earned_prize]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[earned_prize](
	[prize_item_id] [int] NOT NULL,
	[event_participation_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[prize_item_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[earned_prize]  WITH CHECK ADD  CONSTRAINT [FK_earned_prize_event_participation_id] FOREIGN KEY([event_participation_id])
REFERENCES [dbo].[event_participation] ([event_participation_id])
GO
ALTER TABLE [dbo].[earned_prize] CHECK CONSTRAINT [FK_earned_prize_event_participation_id]
GO
ALTER TABLE [dbo].[earned_prize]  WITH CHECK ADD  CONSTRAINT [FK_earned_prize_prize_item] FOREIGN KEY([prize_item_id])
REFERENCES [dbo].[prize_item] ([prize_item_id])
GO
ALTER TABLE [dbo].[earned_prize] CHECK CONSTRAINT [FK_earned_prize_prize_item]
GO
ALTER TABLE [dbo].[earned_prize] ADD  CONSTRAINT [DF_earned_prize_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

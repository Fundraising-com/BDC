USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[stats_personalization]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stats_personalization](
	[stats_personalization_id] [int] IDENTITY(1,1) NOT NULL,
	[event_participation_id] [int] NOT NULL,
	[stats_personalization_item_id] [int] NOT NULL,
	[stats_personalization_section_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_stat_personalization] PRIMARY KEY CLUSTERED 
(
	[stats_personalization_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[stats_personalization]  WITH NOCHECK ADD  CONSTRAINT [FK_stats_perso_event_part] FOREIGN KEY([event_participation_id])
REFERENCES [dbo].[event_participation] ([event_participation_id])
GO
ALTER TABLE [dbo].[stats_personalization] CHECK CONSTRAINT [FK_stats_perso_event_part]
GO
ALTER TABLE [dbo].[stats_personalization]  WITH NOCHECK ADD  CONSTRAINT [FK_stats_perso_item] FOREIGN KEY([stats_personalization_item_id])
REFERENCES [dbo].[stats_personalization_item] ([stats_personalization_item_id])
GO
ALTER TABLE [dbo].[stats_personalization] CHECK CONSTRAINT [FK_stats_perso_item]
GO
ALTER TABLE [dbo].[stats_personalization]  WITH NOCHECK ADD  CONSTRAINT [FK_stats_personalization_stats_personalization_section] FOREIGN KEY([stats_personalization_section_id])
REFERENCES [dbo].[stats_personalization_section] ([stats_personalization_section_id])
GO
ALTER TABLE [dbo].[stats_personalization] CHECK CONSTRAINT [FK_stats_personalization_stats_personalization_section]
GO
ALTER TABLE [dbo].[stats_personalization] ADD  CONSTRAINT [DF_stat_personalization_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

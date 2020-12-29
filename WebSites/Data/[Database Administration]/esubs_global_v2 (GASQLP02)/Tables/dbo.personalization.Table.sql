USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[personalization]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[personalization](
	[personalization_id] [int] IDENTITY(500000,1) NOT FOR REPLICATION NOT NULL,
	[event_participation_id] [int] NOT NULL,
	[header_title1] [varchar](100) NULL,
	[header_title2] [varchar](100) NULL,
	[body] [varchar](2000) NULL,
	[fundraising_goal] [money] NULL,
	[site_bgcolor] [varchar](7) NULL,
	[header_bgcolor] [varchar](7) NULL,
	[header_color] [varchar](7) NULL,
	[group_url] [varchar](255) NULL,
	[image_url] [varchar](255) NULL,
	[create_date] [datetime] NOT NULL,
	[image_motivator] [tinyint] NOT NULL,
	[skip] [bit] NOT NULL,
	[redirect] [varchar](255) NULL,
	[displayGroupMessage] [bit] NULL,
	[remind_later] [bit] NULL,
 CONSTRAINT [PK_personalization] PRIMARY KEY CLUSTERED 
(
	[personalization_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[personalization]  WITH NOCHECK ADD  CONSTRAINT [FK_personalization_event_participation] FOREIGN KEY([event_participation_id])
REFERENCES [dbo].[event_participation] ([event_participation_id])
GO
ALTER TABLE [dbo].[personalization] CHECK CONSTRAINT [FK_personalization_event_participation]
GO
ALTER TABLE [dbo].[personalization]  WITH CHECK ADD  CONSTRAINT [image_motivator_ck] CHECK  (([image_motivator]>=(0)))
GO
ALTER TABLE [dbo].[personalization] CHECK CONSTRAINT [image_motivator_ck]
GO
ALTER TABLE [dbo].[personalization] ADD  CONSTRAINT [DF_personalization_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[personalization] ADD  DEFAULT ((0)) FOR [image_motivator]
GO
ALTER TABLE [dbo].[personalization] ADD  CONSTRAINT [DF__personaliz__skip__36E6F7C4]  DEFAULT ((0)) FOR [skip]
GO

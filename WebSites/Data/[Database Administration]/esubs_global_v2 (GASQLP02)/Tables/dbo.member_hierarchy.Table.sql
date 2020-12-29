USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[member_hierarchy]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[member_hierarchy](
	[member_hierarchy_id] [int] IDENTITY(1000000,1) NOT FOR REPLICATION NOT NULL,
	[parent_member_hierarchy_id] [int] NULL,
	[member_id] [int] NOT NULL,
	[creation_channel_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[unsubscribe] [bit] NOT NULL,
	[unsubscribe_date] [datetime] NULL,
 CONSTRAINT [PK_member_hierarchy] PRIMARY KEY CLUSTERED 
(
	[member_hierarchy_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[member_hierarchy]  WITH CHECK ADD  CONSTRAINT [FK_member_hierarchy_creation_channel] FOREIGN KEY([creation_channel_id])
REFERENCES [dbo].[creation_channel] ([creation_channel_id])
GO
ALTER TABLE [dbo].[member_hierarchy] CHECK CONSTRAINT [FK_member_hierarchy_creation_channel]
GO
ALTER TABLE [dbo].[member_hierarchy]  WITH CHECK ADD  CONSTRAINT [FK_member_hierarchy_member] FOREIGN KEY([member_id])
REFERENCES [dbo].[member] ([member_id])
GO
ALTER TABLE [dbo].[member_hierarchy] CHECK CONSTRAINT [FK_member_hierarchy_member]
GO
ALTER TABLE [dbo].[member_hierarchy]  WITH CHECK ADD  CONSTRAINT [FK_member_hierarchy_member_hierarchy] FOREIGN KEY([parent_member_hierarchy_id])
REFERENCES [dbo].[member_hierarchy] ([member_hierarchy_id])
GO
ALTER TABLE [dbo].[member_hierarchy] CHECK CONSTRAINT [FK_member_hierarchy_member_hierarchy]
GO
ALTER TABLE [dbo].[member_hierarchy] ADD  CONSTRAINT [DF_member_hierarchy_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[member_hierarchy] ADD  DEFAULT (1) FOR [active]
GO
ALTER TABLE [dbo].[member_hierarchy] ADD  DEFAULT (0) FOR [unsubscribe]
GO
ALTER TABLE [dbo].[member_hierarchy] ADD  CONSTRAINT [DF_member_hierarchy_unsubscribe_date]  DEFAULT (null) FOR [unsubscribe_date]
GO

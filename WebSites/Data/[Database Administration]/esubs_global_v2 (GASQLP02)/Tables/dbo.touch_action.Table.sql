USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[touch_action]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[touch_action](
	[touch_action_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[touch_id] [int] NOT NULL,
	[action_date] [datetime] NOT NULL,
	[action_id] [int] NOT NULL,
	[action_desc] [varchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_touch_action] PRIMARY KEY CLUSTERED 
(
	[touch_action_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[touch_action]  WITH NOCHECK ADD  CONSTRAINT [FK_touch_action_action] FOREIGN KEY([action_id])
REFERENCES [dbo].[action] ([action_id])
GO
ALTER TABLE [dbo].[touch_action] CHECK CONSTRAINT [FK_touch_action_action]
GO
ALTER TABLE [dbo].[touch_action] ADD  CONSTRAINT [DF_touch_action_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

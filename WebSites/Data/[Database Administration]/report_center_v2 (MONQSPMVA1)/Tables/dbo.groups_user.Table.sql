USE [report_center_v2]
GO
/****** Object:  Table [dbo].[groups_user]    Script Date: 02/14/2014 16:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[groups_user](
	[group_id] [smallint] NOT NULL,
	[user_name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_groups_user] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC,
	[user_name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[groups_user]  WITH CHECK ADD  CONSTRAINT [FK_groups_user_user] FOREIGN KEY([user_name])
REFERENCES [dbo].[user] ([user_name])
GO
ALTER TABLE [dbo].[groups_user] CHECK CONSTRAINT [FK_groups_user_user]
GO

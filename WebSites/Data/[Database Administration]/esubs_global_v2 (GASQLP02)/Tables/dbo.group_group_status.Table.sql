USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[group_group_status]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group_group_status](
	[group_id] [int] NOT NULL,
	[group_status_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_group_group_status] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC,
	[group_status_id] ASC,
	[create_date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[group_group_status]  WITH CHECK ADD  CONSTRAINT [FK_group_group_status_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[group] ([group_id])
GO
ALTER TABLE [dbo].[group_group_status] CHECK CONSTRAINT [FK_group_group_status_group]
GO
ALTER TABLE [dbo].[group_group_status]  WITH CHECK ADD  CONSTRAINT [FK_group_group_status_group_status] FOREIGN KEY([group_status_id])
REFERENCES [dbo].[group_status] ([group_status_id])
GO
ALTER TABLE [dbo].[group_group_status] CHECK CONSTRAINT [FK_group_group_status_group_status]
GO
ALTER TABLE [dbo].[group_group_status] ADD  CONSTRAINT [DF_group_group_status_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

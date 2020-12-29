USE [report_center_v2]
GO
/****** Object:  Table [dbo].[reports_group]    Script Date: 02/14/2014 16:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reports_group](
	[report_id] [smallint] NOT NULL,
	[group_id] [smallint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[reports_group]  WITH CHECK ADD  CONSTRAINT [FK_reports_group_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[group] ([group_id])
GO
ALTER TABLE [dbo].[reports_group] CHECK CONSTRAINT [FK_reports_group_group]
GO
ALTER TABLE [dbo].[reports_group]  WITH CHECK ADD  CONSTRAINT [FK_reports_group_reports] FOREIGN KEY([report_id])
REFERENCES [dbo].[reports] ([report_id])
GO
ALTER TABLE [dbo].[reports_group] CHECK CONSTRAINT [FK_reports_group_reports]
GO

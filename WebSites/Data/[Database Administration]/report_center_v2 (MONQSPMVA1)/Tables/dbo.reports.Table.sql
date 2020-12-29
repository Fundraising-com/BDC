USE [report_center_v2]
GO
/****** Object:  Table [dbo].[reports]    Script Date: 02/14/2014 16:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[reports](
	[report_id] [smallint] NOT NULL,
	[server_id] [tinyint] NOT NULL,
	[database_id] [tinyint] NOT NULL,
	[report_label] [varchar](100) NOT NULL,
	[report_desc] [varchar](200) NULL,
	[report_sp] [varchar](200) NOT NULL,
	[displayable] [bit] NOT NULL,
 CONSTRAINT [PK_reports] PRIMARY KEY CLUSTERED 
(
	[report_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[reports]  WITH NOCHECK ADD  CONSTRAINT [FK_reports_databases] FOREIGN KEY([database_id])
REFERENCES [dbo].[databases] ([database_id])
GO
ALTER TABLE [dbo].[reports] CHECK CONSTRAINT [FK_reports_databases]
GO
ALTER TABLE [dbo].[reports]  WITH NOCHECK ADD  CONSTRAINT [FK_reports_servers] FOREIGN KEY([server_id])
REFERENCES [dbo].[servers] ([server_id])
GO
ALTER TABLE [dbo].[reports] CHECK CONSTRAINT [FK_reports_servers]
GO
ALTER TABLE [dbo].[reports] ADD  CONSTRAINT [DF_reports_displayable]  DEFAULT (1) FOR [displayable]
GO

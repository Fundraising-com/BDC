USE [report_center_v2]
GO
/****** Object:  Table [dbo].[report_param]    Script Date: 02/14/2014 16:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[report_param](
	[report_param_id] [smallint] NOT NULL,
	[param_ctrl_id] [tinyint] NOT NULL,
	[param_label] [varchar](100) NOT NULL,
	[param_desc] [varchar](200) NULL,
	[param_name] [varchar](100) NOT NULL,
	[param_type] [varchar](100) NOT NULL,
	[nullable] [bit] NOT NULL,
	[param_default_value] [varchar](200) NULL,
	[param_sql_query] [varchar](300) NULL,
 CONSTRAINT [PK_report_param] PRIMARY KEY CLUSTERED 
(
	[report_param_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[report_param]  WITH NOCHECK ADD  CONSTRAINT [FK_report_param_param_ctrl] FOREIGN KEY([param_ctrl_id])
REFERENCES [dbo].[param_ctrl] ([param_ctrl_id])
GO
ALTER TABLE [dbo].[report_param] CHECK CONSTRAINT [FK_report_param_param_ctrl]
GO
ALTER TABLE [dbo].[report_param] ADD  CONSTRAINT [DF_report_param_nullable]  DEFAULT (0) FOR [nullable]
GO

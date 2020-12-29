USE [report_center_v2]
GO
/****** Object:  Table [dbo].[report_param_value]    Script Date: 02/14/2014 16:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[report_param_value](
	[report_id] [smallint] NOT NULL,
	[report_param_id] [smallint] NOT NULL,
	[displayable] [bit] NOT NULL,
 CONSTRAINT [PK_report_param_value] PRIMARY KEY CLUSTERED 
(
	[report_id] ASC,
	[report_param_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[report_param_value]  WITH NOCHECK ADD  CONSTRAINT [FK_report_param_value_report] FOREIGN KEY([report_id])
REFERENCES [dbo].[reports] ([report_id])
GO
ALTER TABLE [dbo].[report_param_value] CHECK CONSTRAINT [FK_report_param_value_report]
GO
ALTER TABLE [dbo].[report_param_value]  WITH NOCHECK ADD  CONSTRAINT [FK_report_param_value_report_param] FOREIGN KEY([report_param_id])
REFERENCES [dbo].[report_param] ([report_param_id])
GO
ALTER TABLE [dbo].[report_param_value] CHECK CONSTRAINT [FK_report_param_value_report_param]
GO
ALTER TABLE [dbo].[report_param_value] ADD  CONSTRAINT [DF_report_param_value_displayable]  DEFAULT (1) FOR [displayable]
GO

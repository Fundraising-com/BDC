USE [esubs_global_v2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touch_change_log](
	[touch_change_log_id] [int] IDENTITY(1,1) NOT NULL,
	[email_template_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,	
	[create_date] [datetime] NOT NULL,
	[created_by] [varchar](100) NOT NULL
 CONSTRAINT [PK_touch_change_log] PRIMARY KEY CLUSTERED 
(
	[touch_change_log_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[touch_change_log]  WITH CHECK ADD  CONSTRAINT [FK_touch_change_log_email_template] FOREIGN KEY([email_template_id])
REFERENCES [dbo].[email_template] ([email_template_id])
GO

ALTER TABLE [dbo].[touch_change_log] CHECK CONSTRAINT [FK_touch_change_log_email_template]
GO

ALTER TABLE [dbo].[touch_change_log] ALTER COLUMN [culture_code] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AI
GO

ALTER TABLE [dbo].[touch_change_log]  WITH CHECK ADD  CONSTRAINT [FK_touch_change_log_culture] FOREIGN KEY([culture_code])
REFERENCES [dbo].[culture] ([culture_code])
GO

ALTER TABLE [dbo].[touch_change_log] CHECK CONSTRAINT [FK_touch_change_log_culture]
GO

ALTER TABLE [dbo].[touch_change_log] ADD  CONSTRAINT [DF_touch_change_log_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
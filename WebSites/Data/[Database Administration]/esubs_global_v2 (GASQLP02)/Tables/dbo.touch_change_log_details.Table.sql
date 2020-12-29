USE [esubs_global_v2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touch_change_log_details](
	[touch_change_log_id] [int] NOT NULL,
	[email_template_field_id] [int] NOT NULL,
	[value] [varchar](8000) NOT NULL,
	[prod_refreshed] BIT NOT NULL,
	[refreshed_by] [varchar](100) NULL,
	[refreshed_date] [datetime] NULL,
	[create_date] [datetime] NOT NULL
 CONSTRAINT [PK_touch_change_log_details] PRIMARY KEY CLUSTERED 
(
	[touch_change_log_id] ASC,
	[email_template_field_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[touch_change_log_details]  WITH CHECK ADD  CONSTRAINT [FK_touch_change_log_details_touch_change_log] FOREIGN KEY([touch_change_log_id])
REFERENCES [dbo].[touch_change_log] ([touch_change_log_id])
GO

ALTER TABLE [dbo].[touch_change_log_details] CHECK CONSTRAINT [FK_touch_change_log_details_touch_change_log]
GO

ALTER TABLE [dbo].[touch_change_log_details]  WITH CHECK ADD  CONSTRAINT [FK_touch_change_log_details_email_template_field] FOREIGN KEY([email_template_field_id])
REFERENCES [dbo].[email_template_field] ([email_template_field_id])
GO

ALTER TABLE [dbo].[touch_change_log_details] CHECK CONSTRAINT [FK_touch_change_log_details_email_template_field]
GO

ALTER TABLE [dbo].[touch_change_log_details] ADD  CONSTRAINT [DF_prod_refreshed]  DEFAULT (0) FOR [prod_refreshed]
GO

ALTER TABLE [dbo].[touch_change_log_details] ADD  CONSTRAINT [DF_touch_change_log_details_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
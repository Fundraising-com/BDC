USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[custom_email_template]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[custom_email_template](
	[custom_email_template_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[touch_info_id] [int] NOT NULL,
	[subject] [varchar](100) NULL,
	[body_txt] [varchar](8000) NULL,
	[body_html] [varchar](8000) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_custom_email_template] PRIMARY KEY CLUSTERED 
(
	[custom_email_template_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[custom_email_template]  WITH CHECK ADD  CONSTRAINT [FK_custom_email_template_touch_info] FOREIGN KEY([touch_info_id])
REFERENCES [dbo].[touch_info] ([touch_info_id])
GO
ALTER TABLE [dbo].[custom_email_template] CHECK CONSTRAINT [FK_custom_email_template_touch_info]
GO
ALTER TABLE [dbo].[custom_email_template] ADD  CONSTRAINT [DF_custom_email_template_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

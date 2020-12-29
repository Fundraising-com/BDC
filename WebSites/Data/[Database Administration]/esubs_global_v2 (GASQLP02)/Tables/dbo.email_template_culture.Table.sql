USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[email_template_culture]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[email_template_culture](
	[email_template_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[subject] [varchar](100) NOT NULL,
	[body_html] [varchar](8000) NOT NULL,
	[body_text] [varchar](8000) NOT NULL,
	[footer_text] [varchar](1000) NULL,
	[footer_html] [varchar](2000) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_email_template_culture] PRIMARY KEY CLUSTERED 
(
	[email_template_id] ASC,
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[email_template_culture]  WITH CHECK ADD  CONSTRAINT [FK_email_template_culture_culture] FOREIGN KEY([culture_code])
REFERENCES [dbo].[culture] ([culture_code])
GO
ALTER TABLE [dbo].[email_template_culture] CHECK CONSTRAINT [FK_email_template_culture_culture]
GO
ALTER TABLE [dbo].[email_template_culture]  WITH CHECK ADD  CONSTRAINT [FK_email_template_culture_email_template] FOREIGN KEY([email_template_id])
REFERENCES [dbo].[email_template] ([email_template_id])
GO
ALTER TABLE [dbo].[email_template_culture] CHECK CONSTRAINT [FK_email_template_culture_email_template]
GO
ALTER TABLE [dbo].[email_template_culture] ADD  CONSTRAINT [DF_email_template_culture_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

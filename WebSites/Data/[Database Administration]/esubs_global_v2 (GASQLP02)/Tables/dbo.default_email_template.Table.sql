USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[default_email_template]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[default_email_template](
	[theme_id] [int] NOT NULL,
	[email_template_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_default_email_template] PRIMARY KEY CLUSTERED 
(
	[theme_id] ASC,
	[email_template_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[default_email_template]  WITH CHECK ADD  CONSTRAINT [FK_default_email_template_email_template] FOREIGN KEY([email_template_id])
REFERENCES [dbo].[email_template] ([email_template_id])
GO
ALTER TABLE [dbo].[default_email_template] CHECK CONSTRAINT [FK_default_email_template_email_template]
GO
ALTER TABLE [dbo].[default_email_template]  WITH CHECK ADD  CONSTRAINT [FK_default_email_template_theme] FOREIGN KEY([theme_id])
REFERENCES [dbo].[theme] ([theme_id])
GO
ALTER TABLE [dbo].[default_email_template] CHECK CONSTRAINT [FK_default_email_template_theme]
GO
ALTER TABLE [dbo].[default_email_template] ADD  CONSTRAINT [DF_default_email_template_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

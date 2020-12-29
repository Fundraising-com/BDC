USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[partner_payment_config]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_payment_config](
	[partner_id] [int] NOT NULL,
	[profit_percentage] [int] NULL,
	[payment_to] [int] NULL,
	[email_template_id] [int] NULL,
	[first_email_template_id] [int] NULL,
	[is_default] [bit] NOT NULL,
	[partner_payment_info_id] [int] NULL,
	[excluded] [bit] NULL,
 CONSTRAINT [PK_partner_payment_config] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[partner_payment_config]  WITH CHECK ADD  CONSTRAINT [FK_partner_payment_config_email_template] FOREIGN KEY([email_template_id])
REFERENCES [dbo].[email_template] ([email_template_id])
GO
ALTER TABLE [dbo].[partner_payment_config] CHECK CONSTRAINT [FK_partner_payment_config_email_template]
GO
ALTER TABLE [dbo].[partner_payment_config]  WITH CHECK ADD  CONSTRAINT [FK_partner_payment_config_email_template1] FOREIGN KEY([first_email_template_id])
REFERENCES [dbo].[email_template] ([email_template_id])
GO
ALTER TABLE [dbo].[partner_payment_config] CHECK CONSTRAINT [FK_partner_payment_config_email_template1]
GO
ALTER TABLE [dbo].[partner_payment_config] ADD  CONSTRAINT [DF_partner_payment_config_is_default]  DEFAULT (0) FOR [is_default]
GO

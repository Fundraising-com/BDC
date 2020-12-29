USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[packages]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[packages](
	[package_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[parent_package_id] [tinyint] NULL,
	[package_template_id] [tinyint] NULL,
	[accounting_class_id] [tinyint] NULL,
	[package_name] [varchar](50) NOT NULL,
	[profit_percentage] [tinyint] NULL,
	[display_order] [tinyint] NULL,
	[package_enabled] [bit] NOT NULL,
	[contains_products] [bit] NOT NULL,
	[nb_participants_per_case] [tinyint] NOT NULL,
 CONSTRAINT [PK_packages] PRIMARY KEY CLUSTERED 
(
	[package_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[packages]  WITH CHECK ADD  CONSTRAINT [FK_packages_accounting_class] FOREIGN KEY([accounting_class_id])
REFERENCES [dbo].[accounting_class] ([accounting_class_id])
GO
ALTER TABLE [dbo].[packages] CHECK CONSTRAINT [FK_packages_accounting_class]
GO
ALTER TABLE [dbo].[packages]  WITH CHECK ADD  CONSTRAINT [FK_packages_package_templates] FOREIGN KEY([package_template_id])
REFERENCES [dbo].[package_templates] ([package_template_id])
GO
ALTER TABLE [dbo].[packages] CHECK CONSTRAINT [FK_packages_package_templates]
GO
ALTER TABLE [dbo].[packages]  WITH CHECK ADD  CONSTRAINT [FK_packages_packages] FOREIGN KEY([parent_package_id])
REFERENCES [dbo].[packages] ([package_id])
GO
ALTER TABLE [dbo].[packages] CHECK CONSTRAINT [FK_packages_packages]
GO
ALTER TABLE [dbo].[packages] ADD  CONSTRAINT [DF_packages_package_enabled]  DEFAULT (1) FOR [package_enabled]
GO
ALTER TABLE [dbo].[packages] ADD  CONSTRAINT [DF_packages_contains_products]  DEFAULT (0) FOR [contains_products]
GO
ALTER TABLE [dbo].[packages] ADD  CONSTRAINT [DF_packages_nb_participants_per_case]  DEFAULT (1) FOR [nb_participants_per_case]
GO

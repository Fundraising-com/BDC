USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[partner_contacts]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner_contacts](
	[partner_contact_id] [smallint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[partner_id] [int] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[section_name] [varchar](50) NOT NULL,
	[section_value] [varchar](500) NOT NULL,
	[display_order] [tinyint] NOT NULL,
 CONSTRAINT [PK_partner_contacts] PRIMARY KEY NONCLUSTERED 
(
	[partner_contact_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[partner_contacts]  WITH CHECK ADD  CONSTRAINT [FK_partner_contacts_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[partner_contacts] CHECK CONSTRAINT [FK_partner_contacts_languages]
GO

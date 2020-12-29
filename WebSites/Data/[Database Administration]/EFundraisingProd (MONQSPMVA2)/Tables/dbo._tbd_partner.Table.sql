USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[_tbd_partner]    Script Date: 02/14/2014 16:28:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_partner](
	[partner_id] [int] NOT NULL,
	[partner_group_type_id] [tinyint] NOT NULL,
	[partner_subgroup_type_id] [tinyint] NOT NULL,
	[partner_name] [varchar](50) NOT NULL,
	[partner_path] [varchar](50) NULL,
	[esubs_url] [varchar](150) NULL,
	[estore_url] [varchar](150) NULL,
	[free_kit_url] [varchar](150) NULL,
	[logo] [varchar](50) NULL,
	[phone_number] [varchar](25) NULL,
	[email_ext] [varchar](50) NULL,
	[url] [varchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[prize_eligible] [bit] NOT NULL,
	[has_collection_site] [bit] NOT NULL,
 CONSTRAINT [PK_partner] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[_tbd_partner]  WITH CHECK ADD  CONSTRAINT [FK_partner_partner_group_types] FOREIGN KEY([partner_group_type_id])
REFERENCES [dbo].[partner_group_types] ([partner_group_type_id])
GO
ALTER TABLE [dbo].[_tbd_partner] CHECK CONSTRAINT [FK_partner_partner_group_types]
GO
ALTER TABLE [dbo].[_tbd_partner]  WITH CHECK ADD  CONSTRAINT [FK_partner_partner_subgroup_types] FOREIGN KEY([partner_subgroup_type_id])
REFERENCES [dbo].[partner_group_types] ([partner_group_type_id])
GO
ALTER TABLE [dbo].[_tbd_partner] CHECK CONSTRAINT [FK_partner_partner_subgroup_types]
GO
ALTER TABLE [dbo].[_tbd_partner] ADD  CONSTRAINT [DF_partner_movie_tickets_eligible]  DEFAULT (1) FOR [prize_eligible]
GO
ALTER TABLE [dbo].[_tbd_partner] ADD  CONSTRAINT [DF_partner_has_collection_site]  DEFAULT (0) FOR [has_collection_site]
GO

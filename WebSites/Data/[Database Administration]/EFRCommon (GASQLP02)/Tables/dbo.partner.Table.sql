USE [EFRCommon]
GO
/****** Object:  Table [dbo].[partner]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner](
	[partner_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[partner_type_id] [int] NOT NULL,
	[partner_name] [varchar](100) NOT NULL,
	[has_collection_site] [bit] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_partner] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[partner]  WITH CHECK ADD  CONSTRAINT [FK_partner_partner_type] FOREIGN KEY([partner_type_id])
REFERENCES [dbo].[partner_type] ([partner_type_id])
GO
ALTER TABLE [dbo].[partner] CHECK CONSTRAINT [FK_partner_partner_type]
GO
ALTER TABLE [dbo].[partner] ADD  CONSTRAINT [DF_is_active]  DEFAULT ((1)) FOR [is_active]
GO

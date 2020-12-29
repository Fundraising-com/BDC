USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[member_postal_address]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[member_postal_address](
	[member_postal_address_id] [int] IDENTITY(1000000,1) NOT FOR REPLICATION NOT NULL,
	[member_id] [int] NOT NULL,
	[postal_address_id] [int] NOT NULL,
	[postal_address_type_id] [int] NOT NULL,
	[active] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_member_postal_address] PRIMARY KEY CLUSTERED 
(
	[member_postal_address_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[member_postal_address]  WITH CHECK ADD  CONSTRAINT [FK_member_postal_address_member] FOREIGN KEY([member_id])
REFERENCES [dbo].[member] ([member_id])
GO
ALTER TABLE [dbo].[member_postal_address] CHECK CONSTRAINT [FK_member_postal_address_member]
GO
ALTER TABLE [dbo].[member_postal_address]  WITH CHECK ADD  CONSTRAINT [FK_member_postal_address_postal_address] FOREIGN KEY([postal_address_id])
REFERENCES [dbo].[postal_address] ([postal_address_id])
GO
ALTER TABLE [dbo].[member_postal_address] CHECK CONSTRAINT [FK_member_postal_address_postal_address]
GO
ALTER TABLE [dbo].[member_postal_address]  WITH CHECK ADD  CONSTRAINT [FK_member_postal_address_postal_address_type] FOREIGN KEY([postal_address_type_id])
REFERENCES [dbo].[postal_address_type] ([postal_address_type_id])
GO
ALTER TABLE [dbo].[member_postal_address] CHECK CONSTRAINT [FK_member_postal_address_postal_address_type]
GO
ALTER TABLE [dbo].[member_postal_address] ADD  CONSTRAINT [DF_member_postal_address_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[partner_packages]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_packages](
	[partner_id] [int] NOT NULL,
	[package_id] [tinyint] NOT NULL,
 CONSTRAINT [PK_partner_packages] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC,
	[package_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[partner_packages]  WITH CHECK ADD  CONSTRAINT [FK_partner_packages_packages] FOREIGN KEY([package_id])
REFERENCES [dbo].[packages] ([package_id])
GO
ALTER TABLE [dbo].[partner_packages] CHECK CONSTRAINT [FK_partner_packages_packages]
GO

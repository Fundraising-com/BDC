USE [eFundstore]
GO
/****** Object:  Table [dbo].[partner_package]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_package](
	[partner_id] [int] NOT NULL,
	[package_id] [int] NOT NULL,
 CONSTRAINT [PK_partner_packages] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC,
	[package_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

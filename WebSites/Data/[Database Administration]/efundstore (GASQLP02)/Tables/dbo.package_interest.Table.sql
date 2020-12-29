USE [eFundstore]
GO
/****** Object:  Table [dbo].[package_interest]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[package_interest](
	[package_id] [int] NOT NULL,
	[package_interest_id] [int] NOT NULL,
	[enabled] [bit] NOT NULL,
 CONSTRAINT [PK_package_interest] PRIMARY KEY CLUSTERED 
(
	[package_id] ASC,
	[package_interest_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

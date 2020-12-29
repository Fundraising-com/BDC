USE [EFRCommon]
GO
/****** Object:  Table [dbo].[package]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[package](
	[package_id] [int] NOT NULL,
	[parent_package_id] [int] NULL,
	[name] [varchar](100) NOT NULL,
	[profit_percentage] [tinyint] NULL,
	[enabled] [bit] NOT NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_package] PRIMARY KEY CLUSTERED 
(
	[package_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

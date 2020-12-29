USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[ewnchapter]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ewnchapter](
	[Chapter] [varchar](8000) NULL,
	[Last Name] [varchar](8000) NULL,
	[First Name] [varchar](8000) NULL,
	[Email address] [varchar](8000) NULL,
	[city] [varchar](8000) NULL,
	[state] [varchar](50) NULL,
	[id] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

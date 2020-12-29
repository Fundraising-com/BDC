USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[KAT]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KAT](
	[id] [int] NOT NULL,
	[name] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[short_name] [varchar](255) NULL,
	[univ_name] [varchar](255) NULL,
	[redirect] [varchar](255) NULL,
	[done] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[aoii_chapters]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[aoii_chapters](
	[id] [int] NOT NULL,
	[Group Name] [varchar](255) NULL,
	[F2] [varchar](255) NULL,
	[Email/ID] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[done] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

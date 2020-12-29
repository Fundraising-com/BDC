USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[kd_alumni]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kd_alumni](
	[id] [int] NOT NULL,
	[Chapter] [varchar](255) NULL,
	[Alumni Name] [varchar](255) NULL,
	[redirect] [varchar](255) NULL,
	[EMAIL ID] [varchar](255) NULL,
	[PW] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[State] [varchar](255) NULL,
	[Zip] [varchar](255) NULL,
	[done] [bit] NOT NULL,
 CONSTRAINT [PK_kd_alumni] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

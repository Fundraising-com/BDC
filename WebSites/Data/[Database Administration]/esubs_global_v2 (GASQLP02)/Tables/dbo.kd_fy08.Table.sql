USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[kd_fy08]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kd_fy08](
	[id] [int] NOT NULL,
	[EventID] [float] NULL,
	[Chapter] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[Sponsor] [nvarchar](255) NULL,
	[Action] [nvarchar](255) NULL,
	[done] [float] NULL
) ON [PRIMARY]
GO

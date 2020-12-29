USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[MD08]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MD08](
	[id] [int] NOT NULL,
	[Chapter] [nvarchar](255) NULL,
	[Last Name] [nvarchar](255) NULL,
	[First Name] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[Zip code] [nvarchar](255) NULL,
	[Email address] [nvarchar](255) NULL,
	[group_id] [int] NULL,
	[done] [bit] NOT NULL,
	[error] [int] NOT NULL
) ON [PRIMARY]
GO

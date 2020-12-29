USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[tpa_fy09]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tpa_fy09](
	[id] [int] NOT NULL,
	[Chapter Name] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[First Name] [nvarchar](255) NULL,
	[Last Name] [nvarchar](255) NULL,
	[Phone Number] [nvarchar](255) NULL,
	[Email Address] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[done] [bit] NOT NULL,
	[error] [bit] NOT NULL
) ON [PRIMARY]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[MDImport]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MDImport](
	[id] [int] NOT NULL,
	[Chapter_name] [nvarchar](255) NULL,
	[Chapter] [nvarchar](255) NULL,
	[Last_Name] [nvarchar](255) NULL,
	[First_Name] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[done] [bit] NOT NULL
) ON [PRIMARY]
GO

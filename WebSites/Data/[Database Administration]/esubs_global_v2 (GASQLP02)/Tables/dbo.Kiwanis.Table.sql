USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[Kiwanis]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kiwanis](
	[id] [int] NOT NULL,
	[MbrLast] [nvarchar](255) NULL,
	[MbrFirst] [nvarchar](255) NULL,
	[Division] [nvarchar](255) NULL,
	[Member ID] [nvarchar](255) NULL,
	[Club Name] [nvarchar](255) NULL,
	[OffcHeld] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Re-enter Password] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[Zip Code] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[done] [bit] NOT NULL,
	[error] [int] NOT NULL
) ON [PRIMARY]
GO

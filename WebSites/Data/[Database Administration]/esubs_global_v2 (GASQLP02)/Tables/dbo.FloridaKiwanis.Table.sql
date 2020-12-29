USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[FloridaKiwanis]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FloridaKiwanis](
	[MbrLast] [nvarchar](255) NULL,
	[MbrFirst] [nvarchar](255) NULL,
	[Division] [nvarchar](255) NULL,
	[Club ID] [nvarchar](255) NULL,
	[Club Name] [nvarchar](255) NULL,
	[OffcHeld] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[Zip Code] [nvarchar](255) NULL,
	[Phone ] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Lead ID] [float] NULL,
	[F16] [nvarchar](255) NULL
) ON [PRIMARY]
GO

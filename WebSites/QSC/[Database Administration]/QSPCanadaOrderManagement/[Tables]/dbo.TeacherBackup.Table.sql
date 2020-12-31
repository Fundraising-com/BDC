USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TeacherBackup]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TeacherBackup](
	[Instance] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[Name] [varchar](101) NULL,
	[Classroom] [varchar](10) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
	[Title] [varchar](10) NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleInitial] [varchar](10) NULL,
	[LastName] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

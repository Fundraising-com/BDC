USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[StudentBackup]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentBackup](
	[Instance] [int] NOT NULL,
	[TeacherInstance] [int] NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](101) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

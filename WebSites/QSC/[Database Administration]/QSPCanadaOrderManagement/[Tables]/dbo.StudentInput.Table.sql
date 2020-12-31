USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[StudentInput]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentInput](
	[MatchJobID] [int] NOT NULL,
	[Instance] [int] NOT NULL,
	[TeacherInstance] [int] NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
 CONSTRAINT [PK_StudentInput] PRIMARY KEY CLUSTERED 
(
	[MatchJobID] ASC,
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

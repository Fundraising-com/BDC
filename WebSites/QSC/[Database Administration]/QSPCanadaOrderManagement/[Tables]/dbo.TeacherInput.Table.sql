USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TeacherInput]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TeacherInput](
	[MatchJobID] [int] NOT NULL,
	[AccountCampaignID] [char](10) NOT NULL,
	[Instance] [int] NOT NULL,
	[Name] [varchar](200) NULL,
	[Classroom] [varchar](20) NULL,
 CONSTRAINT [PK_TeacherInput] PRIMARY KEY CLUSTERED 
(
	[MatchJobID] ASC,
	[AccountCampaignID] ASC,
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

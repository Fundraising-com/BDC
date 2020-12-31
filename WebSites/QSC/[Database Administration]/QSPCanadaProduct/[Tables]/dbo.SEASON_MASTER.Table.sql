USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[SEASON_MASTER]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SEASON_MASTER](
	[Season_Code] [smallint] NOT NULL,
	[SeasonYr] [varchar](11) NULL,
	[Semester] [varchar](6) NULL,
	[CalendarYear] [int] NULL,
	[FiscalYear] [int] NULL,
	[SeasonStart_Dt] [smalldatetime] NULL,
	[SeasonEnd_Dt] [smalldatetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

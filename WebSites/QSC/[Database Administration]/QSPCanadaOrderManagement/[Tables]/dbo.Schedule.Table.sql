USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[BatchJobID] [int] NOT NULL,
	[StartTime] [smalldatetime] NULL,
	[Frequency] [int] NULL,
	[IncludeWeekends] [bit] NOT NULL,
	[StartDay] [int] NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[BatchJobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

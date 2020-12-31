USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[DailyCount]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DailyCount](
	[DailyCountDate] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[PackageCount] [int] NOT NULL,
 CONSTRAINT [aaaaaDailyCount_PK] PRIMARY KEY CLUSTERED 
(
	[DailyCountDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DailyCount] ADD  CONSTRAINT [DF__DailyCoun__Daily__4FD1D5C8]  DEFAULT ('1/1/1995') FOR [DailyCountDate]
GO
ALTER TABLE [dbo].[DailyCount] ADD  CONSTRAINT [DF__DailyCoun__DateC__50C5FA01]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[DailyCount] ADD  CONSTRAINT [DF__DailyCoun__UserI__51BA1E3A]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[DailyCount] ADD  CONSTRAINT [DF__DailyCoun__Packa__52AE4273]  DEFAULT (0) FOR [PackageCount]
GO

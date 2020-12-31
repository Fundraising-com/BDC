USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteTasksMailExtra]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToteTasksMailExtra](
	[ToteInstance] [int] NOT NULL,
	[TaskInstance] [int] NOT NULL,
	[Coupons] [int] NOT NULL,
	[DateUploaded] [datetime] NULL,
 CONSTRAINT [aaaaaToteTasksMailExtra_PK] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC,
	[TaskInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ToteTasksMailExtra] ADD  CONSTRAINT [DF__ToteTasks__ToteI__4A18FC72]  DEFAULT (0) FOR [ToteInstance]
GO
ALTER TABLE [dbo].[ToteTasksMailExtra] ADD  CONSTRAINT [DF__ToteTasks__TaskI__4B0D20AB]  DEFAULT (0) FOR [TaskInstance]
GO
ALTER TABLE [dbo].[ToteTasksMailExtra] ADD  CONSTRAINT [DF__ToteTasks__Coupo__4C0144E4]  DEFAULT (0) FOR [Coupons]
GO
ALTER TABLE [dbo].[ToteTasksMailExtra] ADD  CONSTRAINT [DF_ToteTasksM_DateUploade1__79]  DEFAULT (1 / 1 / 95) FOR [DateUploaded]
GO

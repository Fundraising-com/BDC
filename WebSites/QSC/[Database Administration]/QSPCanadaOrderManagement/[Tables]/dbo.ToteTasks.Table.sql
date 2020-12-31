USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteTasks]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ToteTasks](
	[ToteInstance] [int] NOT NULL,
	[TaskInstance] [int] NOT NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[OutSource] [int] NULL,
 CONSTRAINT [aaaaaToteTasks_PK] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC,
	[TaskInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ToteTasks] ADD  CONSTRAINT [DF__ToteTasks__ToteI__436BFEE3]  DEFAULT (0) FOR [ToteInstance]
GO
ALTER TABLE [dbo].[ToteTasks] ADD  CONSTRAINT [DF__ToteTasks__TaskI__4460231C]  DEFAULT (0) FOR [TaskInstance]
GO
ALTER TABLE [dbo].[ToteTasks] ADD  CONSTRAINT [DF__ToteTasks__DateC__45544755]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[ToteTasks] ADD  CONSTRAINT [DF__ToteTasks__UserI__46486B8E]  DEFAULT (' ') FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[ToteTasks] ADD  CONSTRAINT [DF__ToteTasks__OutSo__44B528D7]  DEFAULT (0) FOR [OutSource]
GO

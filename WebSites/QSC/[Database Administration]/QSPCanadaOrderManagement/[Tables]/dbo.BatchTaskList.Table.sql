USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[BatchTaskList]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchTaskList](
	[ID] [int] NOT NULL,
	[BatchJobID] [int] NULL,
	[TaskID] [int] NULL,
	[PreviousTaskID] [int] NULL,
 CONSTRAINT [PK_BatchTaskList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

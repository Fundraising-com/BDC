USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ProgramTaskDependencies]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramTaskDependencies](
	[ProgramTypeInstance] [int] NOT NULL,
	[TaskTypeInstance] [int] NOT NULL,
	[TaskOrder] [int] NULL,
 CONSTRAINT [aaaaaProgTaskDep_PK] PRIMARY KEY CLUSTERED 
(
	[ProgramTypeInstance] ASC,
	[TaskTypeInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProgramTaskDependencies] ADD  CONSTRAINT [DF__ProgramTa__Progr__6F4A8121]  DEFAULT (0) FOR [ProgramTypeInstance]
GO
ALTER TABLE [dbo].[ProgramTaskDependencies] ADD  CONSTRAINT [DF__ProgramTa__TaskT__703EA55A]  DEFAULT (0) FOR [TaskTypeInstance]
GO
ALTER TABLE [dbo].[ProgramTaskDependencies] ADD  CONSTRAINT [DF__ProgramTa__TaskO__3B2BBE9D]  DEFAULT (0) FOR [TaskOrder]
GO

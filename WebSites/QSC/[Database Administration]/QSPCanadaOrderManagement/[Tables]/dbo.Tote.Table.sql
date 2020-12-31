USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Tote]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tote](
	[Instance] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[ProgramTypeInstance] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[PackageDateCreated] [datetime] NULL,
 CONSTRAINT [aaaaaTote_PK] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF__Tote__Instance__2F650636]  DEFAULT (0) FOR [Instance]
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF__Tote__AccountID__30592A6F]  DEFAULT (0) FOR [AccountID]
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF__Tote__ProgramTyp__314D4EA8]  DEFAULT (0) FOR [ProgramTypeInstance]
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF__Tote__DateCreate__324172E1]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF__Tote__UserIDCrea__3335971A]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF__Tote__DateChange__3429BB53]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF__Tote__UserIDChan__351DDF8C]  DEFAULT (null) FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[Tote] ADD  CONSTRAINT [DF_Tote_PackageDateCreate1__20]  DEFAULT ('1/1/1995') FOR [PackageDateCreated]
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[Instance] [int] NOT NULL,
	[TeacherInstance] [int] NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](101) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
 CONSTRAINT [aaaaaStudent_PK] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__Instanc__3B75D760]  DEFAULT (0) FOR [Instance]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__Teacher__3C69FB99]  DEFAULT (0) FOR [TeacherInstance]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__LastNam__3D5E1FD2]  DEFAULT (null) FOR [LastName]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__FirstNa__3E52440B]  DEFAULT (null) FOR [FirstName]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__DateCre__1EF99443]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__UserIDC__1FEDB87C]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__DateCha__20E1DCB5]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF__Student__UserIDC__21D600EE]  DEFAULT (' ') FOR [UserIDChanged]
GO

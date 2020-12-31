USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teacher](
	[Instance] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[Name] [varchar](101) NULL,
	[Classroom] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
	[Title] [varchar](10) NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleInitial] [varchar](10) NULL,
	[LastName] [varchar](50) NULL,
 CONSTRAINT [aaaaaTeacher_PK] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__Instanc__4F7CD00D]  DEFAULT (0) FOR [Instance]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__Account__5070F446]  DEFAULT (0) FOR [AccountID]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__Name__5165187F]  DEFAULT (null) FOR [Name]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__Classro__52593CB8]  DEFAULT (null) FOR [Classroom]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__DateCre__1B29035F]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__UserIDC__1C1D2798]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__DateCha__1D114BD1]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF__Teacher__UserIDC__1E05700A]  DEFAULT (' ') FOR [UserIDChanged]
GO

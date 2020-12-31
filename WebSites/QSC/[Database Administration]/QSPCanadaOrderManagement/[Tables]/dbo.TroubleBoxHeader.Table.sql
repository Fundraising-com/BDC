USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TroubleBoxHeader]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TroubleBoxHeader](
	[Instance] [int] NOT NULL,
	[PackageID] [varchar](40) NULL,
	[NextDetailTransID] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[FieldManagerNo] [varchar](4) NULL,
	[ProgramTypeInstance] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[ReasonCodeInstance] [int] NOT NULL,
	[FixCodeInstance] [int] NOT NULL,
	[Description] [varchar](200) NULL,
	[ToteInstance] [int] NULL,
 CONSTRAINT [aaaaaTroubleBoxHeader_PK] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__Insta__567ED357]  DEFAULT (0) FOR [Instance]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__Packa__5772F790]  DEFAULT (null) FOR [PackageID]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__NextD__58671BC9]  DEFAULT (0) FOR [NextDetailTransID]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__Accou__595B4002]  DEFAULT (0) FOR [AccountID]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__Field__5A4F643B]  DEFAULT (null) FOR [FieldManagerNo]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__Progr__5B438874]  DEFAULT (0) FOR [ProgramTypeInstance]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__DateC__5C37ACAD]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__UserI__5D2BD0E6]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__DateC__5E1FF51F]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__UserI__5F141958]  DEFAULT (null) FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__Reaso__60083D91]  DEFAULT (0) FOR [ReasonCodeInstance]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__FixCo__60FC61CA]  DEFAULT (0) FOR [FixCodeInstance]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__Descr__61F08603]  DEFAULT (null) FOR [Description]
GO
ALTER TABLE [dbo].[TroubleBoxHeader] ADD  CONSTRAINT [DF__TroubleBo__ToteI__3B4BBA2E]  DEFAULT (0) FOR [ToteInstance]
GO

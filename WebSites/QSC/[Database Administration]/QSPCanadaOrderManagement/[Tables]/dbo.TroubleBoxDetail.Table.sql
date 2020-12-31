USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TroubleBoxDetail]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TroubleBoxDetail](
	[TroubleBoxHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [aaaaaTroubleBoxDetail_PK] PRIMARY KEY CLUSTERED 
(
	[TroubleBoxHeaderInstance] ASC,
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TroubleBoxDetail] ADD  CONSTRAINT [DF__TroubleBo__Troub__65C116E7]  DEFAULT (0) FOR [TroubleBoxHeaderInstance]
GO
ALTER TABLE [dbo].[TroubleBoxDetail] ADD  CONSTRAINT [DF__TroubleBo__Trans__66B53B20]  DEFAULT (0) FOR [TransID]
GO
ALTER TABLE [dbo].[TroubleBoxDetail] ADD  CONSTRAINT [DF__TroubleBo__DateC__67A95F59]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[TroubleBoxDetail] ADD  CONSTRAINT [DF__TroubleBo__UserI__689D8392]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[TroubleBoxDetail] ADD  CONSTRAINT [DF__TroubleBo__DateC__6991A7CB]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[TroubleBoxDetail] ADD  CONSTRAINT [DF__TroubleBo__UserI__6A85CC04]  DEFAULT (null) FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[TroubleBoxDetail] ADD  CONSTRAINT [DF__TroubleBo__Descr__6B79F03D]  DEFAULT (null) FOR [Description]
GO

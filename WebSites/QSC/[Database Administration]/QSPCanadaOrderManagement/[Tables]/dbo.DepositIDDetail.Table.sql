USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[DepositIDDetail]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DepositIDDetail](
	[DepositDate] [datetime] NOT NULL,
	[DepositID] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[Description] [varchar](200) NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [aaaaaDepositIDDetail_PK] PRIMARY KEY CLUSTERED 
(
	[DepositDate] ASC,
	[DepositID] ASC,
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__Depos__35F20834]  DEFAULT ('1/1/1995') FOR [DepositDate]
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__Depos__36E62C6D]  DEFAULT (0) FOR [DepositID]
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__Trans__37DA50A6]  DEFAULT (0) FOR [TransID]
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__Amoun__38CE74DF]  DEFAULT (0.0) FOR [Amount]
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__DateC__39C29918]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__UserI__3AB6BD51]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__Descr__3BAAE18A]  DEFAULT (null) FOR [Description]
GO
ALTER TABLE [dbo].[DepositIDDetail] ADD  CONSTRAINT [DF__DepositID__Statu__3C9F05C3]  DEFAULT (0) FOR [Status]
GO

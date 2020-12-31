USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CUserProfile]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CUserProfile](
	[Instance] [int] NOT NULL,
	[EmployeeInstance] [int] NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[Locked] [bit] NOT NULL,
	[Hold] [bit] NOT NULL,
	[FailedAttempts] [smallint] NOT NULL,
	[ChangePasswordOnNextLogin] [bit] NOT NULL,
	[LoggedIn] [int] NOT NULL,
	[MakeCheckPayableTo] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[FMNumber] [varchar](4) NOT NULL,
	[MailAddress1] [varchar](50) NULL,
	[MailAddress2] [varchar](50) NULL,
	[MailCity] [varchar](30) NULL,
	[MailState] [varchar](2) NULL,
	[MailPostalCode] [varchar](10) NULL,
	[ShipAddress1] [varchar](50) NULL,
	[ShipAddress2] [varchar](50) NULL,
	[ShipCity] [varchar](30) NULL,
	[ShipState] [varchar](2) NULL,
	[ShipPostalCode] [varchar](10) NULL,
	[VoiceMailExt] [varchar](4) NULL,
	[HomePhone] [varchar](20) NULL,
	[WorkPhone] [varchar](20) NULL,
	[FaxPhone] [varchar](20) NULL,
	[TollFreePhone] [varchar](20) NULL,
	[MobilePhone] [varchar](20) NULL,
	[PagerPhone] [varchar](20) NULL,
	[SignificantOther] [varchar](30) NULL,
	[LockedCounter] [int] NULL,
	[LockedDateTimeStamp] [datetime] NULL,
	[Region] [varchar](2) NULL,
	[AreaManager] [varchar](4) NULL,
	[InvoiceTerm] [int] NULL,
	[DefaultInvMsg1] [varchar](200) NULL,
	[DefaultInvMsg2] [varchar](200) NULL,
	[DefaultInvMsg3] [varchar](200) NULL,
	[UnitsGoal] [int] NULL,
	[Modified_By] [int] NULL,
	[Created_By] [int] NULL,
	[Deleted_TF] [int] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[CorporateEmail] [varchar](40) NULL,
	[CUserProfileStatusId] [int] NULL,
	[TopNodeId] [int] NULL,
	[DefectorTF] [bit] NULL,
	[InvCompany] [varchar](75) NULL,
	[InvAddress1] [varchar](50) NULL,
	[InvAddress2] [varchar](50) NULL,
	[InvCity] [varchar](30) NULL,
	[InvState] [varchar](2) NULL,
	[InvPostalCode] [varchar](10) NULL,
	[InvPhone] [varchar](20) NULL,
	[TimeZone] [smallint] NULL,
	[DST] [bit] NULL,
	[DefaultLang] [varchar](5) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [dbo].[UserID_UDDT] NULL,
 CONSTRAINT [PK_CUserProfile] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UNIQUE_CUserProfile_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF__CUserProf__Modif__69279377]  DEFAULT (null) FOR [Modified_By]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF__CUserProf__Creat__6CF8245B]  DEFAULT (null) FOR [Created_By]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF__CUserProf__Delet__70C8B53F]  DEFAULT (0) FOR [Deleted_TF]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF__CUserProf__DateC__74994623]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF__CUserProf__DateM__7869D707]  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF_CUserProfile_CUserProfileStatusId]  DEFAULT (1) FOR [CUserProfileStatusId]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF_CUserProfile_DefectorTF]  DEFAULT (0) FOR [DefectorTF]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF_CUserProf_TZ]  DEFAULT (0) FOR [TimeZone]
GO
ALTER TABLE [dbo].[CUserProfile] ADD  CONSTRAINT [DF_CUserProf_DST]  DEFAULT (1) FOR [DST]
GO

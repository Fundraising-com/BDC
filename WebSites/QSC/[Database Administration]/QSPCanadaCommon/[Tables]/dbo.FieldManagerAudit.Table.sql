USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[FieldManagerAudit]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FieldManagerAudit](
	[AuditID] [int] IDENTITY(1,1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[FMID] [varchar](4) NOT NULL,
	[Status] [int] NULL,
	[Country] [dbo].[CountryCode_UDDT] NULL,
	[PhoneListID] [int] NULL,
	[AddressListID] [int] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[MiddleInitial] [varchar](10) NULL,
	[Email] [varchar](60) NULL,
	[DMID] [varchar](4) NULL,
	[UserIDModified] [dbo].[UserID_UDDT] NULL,
	[DateModified] [datetime] NULL,
	[Comment] [varchar](256) NULL,
	[DMIndicator] [char](1) NULL,
	[Lang] [varchar](10) NULL,
	[DeletedTF] [bit] NOT NULL,
 CONSTRAINT [PK_FieldManagerAudit] PRIMARY KEY CLUSTERED 
(
	[AuditID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FieldManagerAudit] ADD  CONSTRAINT [DF_FieldManagerAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO

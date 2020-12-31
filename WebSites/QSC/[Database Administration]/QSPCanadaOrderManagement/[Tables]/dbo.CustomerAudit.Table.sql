USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerAudit]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerAudit](
	[AuditDate] [datetime] NOT NULL,
	[Instance] [int] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[County] [varchar](31) NULL,
	[State] [varchar](10) NULL,
	[Zip] [varchar](10) NULL,
	[ZipPlusFour] [varchar](4) NULL,
	[OverrideAddress] [bit] NOT NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NOT NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](25) NULL,
	[Type] [int] NULL,
	[AuditInstance] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CustomerAudit] PRIMARY KEY CLUSTERED 
(
	[AuditInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

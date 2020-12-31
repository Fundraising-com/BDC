USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerRemitHistoryAudit]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerRemitHistoryAudit](
	[AuditDate] [datetime] NOT NULL,
	[RemitBatchID] [int] NOT NULL,
	[Instance] [int] NOT NULL,
	[CustomerInstance] [int] NULL,
	[StatusInstance] [int] NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](10) NULL,
	[Zip] [varchar](20) NULL,
	[ZipPlusFour] [varchar](4) NULL,
	[DateModified] [datetime] NULL,
	[UserIDModified] [dbo].[UserID_UDDT] NULL,
 CONSTRAINT [PK_CustomerRemitHistoryAudit] PRIMARY KEY NONCLUSTERED 
(
	[AuditDate] ASC,
	[RemitBatchID] ASC,
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

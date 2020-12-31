USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CAccountAudit]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CAccountAudit](
	[AuditDate] [datetime] NOT NULL,
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Country] [varchar](10) NOT NULL,
	[Lang] [varchar](10) NULL,
	[CAccountCodeClass] [varchar](10) NULL,
	[CAccountCodeGroup] [varchar](50) NULL,
	[PhoneListID] [int] NULL,
	[AddressListID] [int] NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [char](2) NULL,
	[Zip] [varchar](12) NULL,
	[Zip4] [varchar](12) NULL,
	[County] [varchar](20) NULL,
	[StatusID] [int] NULL,
	[Enrollment] [int] NULL,
	[Comment] [varchar](1000) NULL,
	[EMail] [varchar](75) NULL,
	[IsPrivateOrg] [char](1) NULL,
	[IsAdultGroup] [char](1) NULL,
	[ParentID] [int] NULL,
	[SalesRegionID] [int] NULL,
	[StatementPrintCycleID] [int] NULL,
	[StatementPrintSlot] [int] NULL,
	[DateCreatedTOSSthis] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[UserIDModified] [dbo].[UserID_UDDT] NOT NULL,
	[VendorNumber] [varchar](30) NULL,
	[VendorSiteName] [varchar](15) NULL,
	[VendorPayGroup] [varchar](25) NULL,
	[OriginalAddress1] [varchar](50) NULL,
	[OriginalAddress2] [varchar](50) NULL,
	[OriginalCity] [varchar](50) NULL,
	[OriginalState] [char](2) NULL,
	[OriginalZip] [varchar](6) NULL,
	[OriginalZip4] [varchar](4) NULL,
	[ShipToAddress1] [varchar](50) NULL,
	[ShipToAddress2] [varchar](50) NULL,
	[ShipToCity] [varchar](50) NULL,
	[ShipToState] [char](2) NULL,
	[ShipToZip] [varchar](6) NULL,
	[ShipToZip4] [varchar](4) NULL,
	[Sponsor] [varchar](50) NULL,
 CONSTRAINT [PK_CAccountAudit] PRIMARY KEY CLUSTERED 
(
	[AuditDate] ASC,
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

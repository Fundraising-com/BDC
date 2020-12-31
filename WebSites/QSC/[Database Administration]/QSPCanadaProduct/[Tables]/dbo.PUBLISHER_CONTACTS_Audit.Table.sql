USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[PUBLISHER_CONTACTS_Audit]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PUBLISHER_CONTACTS_Audit](
	[AuditInstance] [int] IDENTITY(1,1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[PContact_Instance] [int] NOT NULL,
	[Product_Code] [varchar](10) NULL,
	[Pub_Nbr] [varchar](4) NOT NULL,
	[Pub_Contact_Nbr] [int] NOT NULL,
	[PContact_Prefix] [varchar](5) NULL,
	[PContact_FName] [varchar](30) NULL,
	[PContact_LName] [varchar](30) NULL,
	[PContact_Addr_1] [varchar](35) NULL,
	[PContact_Addr_2] [varchar](35) NULL,
	[PContact_City] [varchar](25) NULL,
	[PContact_State] [varchar](2) NULL,
	[PContact_Zip] [varchar](5) NULL,
	[PContact_Zip_Four] [varchar](4) NULL,
	[PContact_Tel] [varchar](14) NULL,
	[PContact_Tel_Extn] [varchar](10) NULL,
	[PContact_Fax] [varchar](14) NULL,
	[PContact_Email] [varchar](50) NULL,
	[PContact_DateChanged] [datetime] NULL,
	[PContact_ChangedBy] [int] NULL,
	[PhoneListID] [int] NULL,
	[PContact_Title] [varchar](50) NULL,
 CONSTRAINT [PK_PUBLISHER_CONTACTS_Audit] PRIMARY KEY CLUSTERED 
(
	[AuditInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

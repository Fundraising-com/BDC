USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CompanyInfo]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CompanyInfo](
	[CompanyID] [int] NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[ShipAddress1] [varchar](100) NOT NULL,
	[ShipAddress2] [varchar](100) NULL,
	[ShipCity] [varchar](100) NOT NULL,
	[ShipProvince] [varchar](2) NOT NULL,
	[ShipPostalCode] [varchar](20) NOT NULL,
	[ShipCountry] [varchar](2) NOT NULL,
	[ShipPhone1] [varchar](20) NOT NULL,
	[ShipPhone2] [varchar](20) NULL,
	[ShipFax1] [varchar](20) NULL,
	[ShipFax2] [varchar](20) NULL,
	[BillAddress1] [varchar](100) NOT NULL,
	[BillAddress2] [varchar](100) NULL,
	[BillCity] [varchar](100) NOT NULL,
	[BillProvince] [varchar](2) NOT NULL,
	[BillPostalCode] [varchar](20) NOT NULL,
	[BillCountry] [varchar](2) NOT NULL,
	[BillPhone1] [varchar](20) NOT NULL,
	[BillPhone2] [varchar](20) NULL,
	[BillFax1] [varchar](20) NULL,
	[BillFax2] [varchar](20) NULL,
	[Email] [varchar](20) NULL,
	[Web] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

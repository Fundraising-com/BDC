USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[fr_all_customers]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fr_all_customers](
	[vif] [varchar](21) NULL,
	[custnmbr] [varchar](15) NULL,
	[custname] [varchar](65) NULL,
	[address1] [varchar](31) NULL,
	[address2] [varchar](31) NULL,
	[country] [varchar](21) NULL,
	[city] [varchar](31) NULL,
	[state] [varchar](29) NULL,
	[zip] [varchar](11) NULL,
	[phone1] [varchar](21) NULL,
	[phone2] [varchar](21) NULL,
	[fax] [varchar](21) NULL,
	[creatddt] [varchar](10) NULL,
	[emailaddress] [varchar](255) NULL,
	[mindate] [varchar](10) NULL,
	[source] [varchar](50) NULL,
	[matching_code] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CACCOUNT_Address]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CACCOUNT_Address](
	[AccountID] [int] NOT NULL,
	[street1] [varchar](50) NULL,
	[street2] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[stateProvince] [varchar](10) NULL,
	[postal_code] [varchar](7) NULL,
	[zip4] [varchar](4) NULL,
	[country] [varchar](10) NULL,
	[address_type] [int] NOT NULL,
	[AddressListID] [int] NULL,
 CONSTRAINT [PK_CACCOUNT_Address] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC,
	[address_type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

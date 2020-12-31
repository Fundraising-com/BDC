USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[FulfContactFix]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FulfContactFix](
	[FULFILLMENT_HOUSE_ID] [int] NOT NULL,
	[FIRST_NAME] [varchar](50) NULL,
	[LAST_NAME] [varchar](50) NULL,
	[ADDRESS_LINE1] [varchar](50) NULL,
	[ADDRESS_LINE2] [varchar](50) NULL,
	[CITY] [varchar](50) NULL,
	[PROVINCE_CODE] [varchar](10) NOT NULL,
	[POSTAL_CODE] [varchar](20) NULL,
	[COUNTRY_CODE] [varchar](10) NOT NULL,
	[FUNCTION] [varchar](100) NULL,
	[EMAIL] [varchar](60) NULL,
	[PHONE_NUMBER] [varchar](50) NOT NULL,
	[FAX] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

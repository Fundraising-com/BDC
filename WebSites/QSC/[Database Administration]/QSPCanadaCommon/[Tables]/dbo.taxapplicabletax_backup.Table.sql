USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[taxapplicabletax_backup]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[taxapplicabletax_backup](
	[TAX_ID] [int] NOT NULL,
	[SECTION_TYPE_ID] [int] NOT NULL,
	[COUNTRY_CODE] [varchar](10) NOT NULL,
	[PROVINCE_CODE] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

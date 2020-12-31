USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[BANK_ACCOUNT]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BANK_ACCOUNT](
	[BANK_ACCOUNT_ID] [int] NOT NULL,
	[BANK_ACCOUNT_NUMBER] [varchar](50) NULL,
	[BANK_ACCOUNT_DESCRIPTION] [varchar](50) NULL,
	[COUNTRY_CODE] [varchar](10) NULL,
	[BANK_ACCOUNT_NAME] [varchar](50) NULL,
 CONSTRAINT [PK_BANK_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[BANK_ACCOUNT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

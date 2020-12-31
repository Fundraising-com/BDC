USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[Tax_Backup]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tax_Backup](
	[TAX_ID] [int] NOT NULL,
	[TAX_DESC] [varchar](100) NULL,
	[TAX_FUNCTION] [varchar](50) NULL,
	[TAX_REGISTRATION] [varchar](50) NULL,
	[TAX_RATE] [numeric](4, 2) NULL,
	[TAX_EFFECTIVE_DATE] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

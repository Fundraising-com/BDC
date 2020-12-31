USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[TaxRegion]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxRegion](
	[ID] [int] NOT NULL,
	[Description] [varchar](50) NULL,
	[ConsolidatedRate] [numeric](10, 4) NULL,
	[EffectiveDate] [datetime] NULL,
 CONSTRAINT [PK_TaxRegion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

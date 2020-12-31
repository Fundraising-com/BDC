USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[Tax]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tax](
	[TAX_ID] [int] NOT NULL,
	[TAX_DESC] [varchar](100) NULL,
	[TAX_FUNCTION] [varchar](50) NULL,
	[TAX_REGISTRATION] [varchar](50) NULL,
	[TAX_RATE] [numeric](5, 3) NULL,
	[TAX_EFFECTIVE_DATE] [datetime] NULL,
 CONSTRAINT [PK_Tax] PRIMARY KEY CLUSTERED 
(
	[TAX_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

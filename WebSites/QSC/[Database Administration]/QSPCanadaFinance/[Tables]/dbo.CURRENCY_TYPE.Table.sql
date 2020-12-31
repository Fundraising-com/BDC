USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[CURRENCY_TYPE]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CURRENCY_TYPE](
	[CURRENCY_ID] [int] NOT NULL,
	[NAME_RESOURCE_ID] [int] NULL,
 CONSTRAINT [PK_CURRENCY_TYPE] PRIMARY KEY CLUSTERED 
(
	[CURRENCY_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[Program]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Program](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](50) NULL,
	[FundraisingProcedureID] [int] NULL,
	[ProgramTypeID] [int] NULL,
	[Name] [varchar](50) NULL,
	[MajorProductLineID] [int] NULL,
	[DefaultProfit] [numeric](10, 2) NULL,
	[MinProfit] [numeric](10, 2) NULL,
	[MaxProfit] [numeric](10, 2) NULL,
	[OrdefuPrintInAR] [char](1) NULL,
	[ActiveForFiscal_TF] [bit] NOT NULL,
	[Abr] [varchar](15) NULL,
	[FrenchName] [varchar](50) NULL,
	[PrintInvoice] [int] NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Program] ADD  CONSTRAINT [DF_Program_ActiveForFiscal_TF]  DEFAULT (1) FOR [ActiveForFiscal_TF]
GO

USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GL_Trans_Audit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GL_Trans_Audit](
	[GLTransAuditID] [int] IDENTITY(1,1) NOT NULL,
	[GLEntryID] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[TransType] [varchar](50) NOT NULL,
	[TransactionAmount] [decimal](10, 2) NOT NULL,
	[GLEntryDate] [datetime] NOT NULL,
	[AccountingYear] [int] NOT NULL,
	[AccountingPeriod] [int] NOT NULL,
	[UserDeleted] [int] NOT NULL,
	[DateDeleted] [datetime] NOT NULL,
	[CatalystAdjusted] [char](1) NOT NULL,
	[DateAdjusted] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

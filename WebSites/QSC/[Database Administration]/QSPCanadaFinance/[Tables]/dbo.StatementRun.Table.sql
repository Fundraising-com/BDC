USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementRun]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementRun](
	[StatementRunID] [int] IDENTITY(1,1) NOT NULL,
	[StatementRunDate] [datetime] NOT NULL,
	[FiscalYearEnd] [bit] NOT NULL,
	[StatementRunClosed] [bit] NOT NULL,
	[StatementsInOwingOnly] [bit] NOT NULL,
 CONSTRAINT [PK_StatementRun] PRIMARY KEY CLUSTERED 
(
	[StatementRunID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementRun] ADD  CONSTRAINT [DF_StatementRun_StatementRunClosed]  DEFAULT ((0)) FOR [StatementRunClosed]
GO

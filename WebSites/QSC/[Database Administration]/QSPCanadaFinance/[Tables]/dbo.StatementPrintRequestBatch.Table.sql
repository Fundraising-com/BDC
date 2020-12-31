USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementPrintRequestBatch]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementPrintRequestBatch](
	[StatementPrintRequestBatchID] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Filename] [nvarchar](200) NULL,
 CONSTRAINT [PK_StatementPrintRequestBatch] PRIMARY KEY CLUSTERED 
(
	[StatementPrintRequestBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

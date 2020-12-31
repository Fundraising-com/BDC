USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GLAccountBalance]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLAccountBalance](
	[GLAccountBalanceID] [int] IDENTITY(1,1) NOT NULL,
	[GLAccountID] [int] NOT NULL,
	[AccountingYear] [int] NOT NULL,
	[AccountingPeriod] [int] NOT NULL,
	[OpeningBalance] [numeric](12, 2) NULL,
	[ClosingBalance] [numeric](12, 2) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GLAccountBalance] PRIMARY KEY CLUSTERED 
(
	[GLAccountBalanceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GLAccountBalance]  WITH CHECK ADD  CONSTRAINT [FK_GLAccountBalance_GLAccount] FOREIGN KEY([GLAccountID])
REFERENCES [dbo].[GLAccount] ([GLAccountID])
GO
ALTER TABLE [dbo].[GLAccountBalance] CHECK CONSTRAINT [FK_GLAccountBalance_GLAccount]
GO
ALTER TABLE [dbo].[GLAccountBalance] ADD  CONSTRAINT [DF_GLAccountBalance_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

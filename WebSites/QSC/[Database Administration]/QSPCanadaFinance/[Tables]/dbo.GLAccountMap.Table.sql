USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GLAccountMap]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLAccountMap](
	[GLAccountMapID] [int] IDENTITY(1,1) NOT NULL,
	[GLEntryTypeID] [int] NOT NULL,
	[ProductLineID] [int] NULL,
	[TaxID] [int] NULL,
	[Debit] [bit] NOT NULL,
	[GLAccountID] [int] NOT NULL,
	[CurrencyID] [int] NULL,
	[PaymentMethodID] [int] NULL,
	[BusinessUnitID] [int] NULL,
 CONSTRAINT [PK_GLAccountMap] PRIMARY KEY CLUSTERED 
(
	[GLAccountMapID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GLAccountMap]  WITH CHECK ADD  CONSTRAINT [FK_GLAccountMap_GLAccount] FOREIGN KEY([GLAccountID])
REFERENCES [dbo].[GLAccount] ([GLAccountID])
GO
ALTER TABLE [dbo].[GLAccountMap] CHECK CONSTRAINT [FK_GLAccountMap_GLAccount]
GO
ALTER TABLE [dbo].[GLAccountMap]  WITH CHECK ADD  CONSTRAINT [FK_GLAccountMap_GLEntryType] FOREIGN KEY([GLEntryTypeID])
REFERENCES [dbo].[GLEntryType] ([GLEntryTypeID])
GO
ALTER TABLE [dbo].[GLAccountMap] CHECK CONSTRAINT [FK_GLAccountMap_GLEntryType]
GO

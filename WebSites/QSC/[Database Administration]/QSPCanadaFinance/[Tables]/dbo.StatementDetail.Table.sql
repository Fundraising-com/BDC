USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementDetail]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementDetail](
	[StatementDetailID] [int] IDENTITY(1,1) NOT NULL,
	[StatementID] [int] NOT NULL,
	[StatementDetailTypeID] [int] NOT NULL,
	[TransactionDate] [datetime] NULL,
	[TransactionID] [int] NULL,
	[OrderID] [int] NULL,
	[TransactionTypeName] [nvarchar](100) NOT NULL,
	[TransactionReference] [nvarchar](100) NULL,
	[TransactionAmount] [numeric](12, 2) NOT NULL,
 CONSTRAINT [PK_StatementDetail] PRIMARY KEY CLUSTERED 
(
	[StatementDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementDetail]  WITH CHECK ADD  CONSTRAINT [FK_StatementDetail_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementDetail] CHECK CONSTRAINT [FK_StatementDetail_Statement]
GO
ALTER TABLE [dbo].[StatementDetail]  WITH CHECK ADD  CONSTRAINT [FK_StatementDetail_StatementDetailType] FOREIGN KEY([StatementDetailTypeID])
REFERENCES [dbo].[StatementDetailType] ([StatementDetailTypeID])
GO
ALTER TABLE [dbo].[StatementDetail] CHECK CONSTRAINT [FK_StatementDetail_StatementDetailType]
GO

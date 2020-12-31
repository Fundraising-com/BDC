USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GLAccountAdjustmentType]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLAccountAdjustmentType](
	[GLAccountAdjustmentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[GLAccountID] [int] NOT NULL,
	[AdjustmentTypeID] [int] NOT NULL,
	[Debit] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GLAccountAdjustmentType] PRIMARY KEY CLUSTERED 
(
	[GLAccountAdjustmentTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GLAccountAdjustmentType]  WITH CHECK ADD  CONSTRAINT [FK_GLAccountAdjustmentType_ADJUSTMENT_TYPE] FOREIGN KEY([AdjustmentTypeID])
REFERENCES [dbo].[ADJUSTMENT_TYPE] ([ADJUSTMENT_TYPE_ID])
GO
ALTER TABLE [dbo].[GLAccountAdjustmentType] CHECK CONSTRAINT [FK_GLAccountAdjustmentType_ADJUSTMENT_TYPE]
GO
ALTER TABLE [dbo].[GLAccountAdjustmentType]  WITH CHECK ADD  CONSTRAINT [FK_GLAccountAdjustmentType_GLAccount] FOREIGN KEY([GLAccountID])
REFERENCES [dbo].[GLAccount] ([GLAccountID])
GO
ALTER TABLE [dbo].[GLAccountAdjustmentType] CHECK CONSTRAINT [FK_GLAccountAdjustmentType_GLAccount]
GO
ALTER TABLE [dbo].[GLAccountAdjustmentType] ADD  CONSTRAINT [DF_GLAccountAdjustmentType_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

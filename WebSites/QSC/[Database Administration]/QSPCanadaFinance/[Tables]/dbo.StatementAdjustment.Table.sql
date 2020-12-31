USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementAdjustment]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementAdjustment](
	[StatementAdjustmentID] [int] IDENTITY(1,1) NOT NULL,
	[StatementID] [int] NOT NULL,
	[AdjustmentID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StatementAdjustment] PRIMARY KEY CLUSTERED 
(
	[StatementAdjustmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_StatementAdjustment_Adjustment] FOREIGN KEY([AdjustmentID])
REFERENCES [dbo].[ADJUSTMENT] ([ADJUSTMENT_ID])
GO
ALTER TABLE [dbo].[StatementAdjustment] CHECK CONSTRAINT [FK_StatementAdjustment_Adjustment]
GO
ALTER TABLE [dbo].[StatementAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_StatementAdjustment_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementAdjustment] CHECK CONSTRAINT [FK_StatementAdjustment_Statement]
GO
ALTER TABLE [dbo].[StatementAdjustment] ADD  CONSTRAINT [DF_StatementAdjustment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementPrintRequest]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementPrintRequest](
	[StatementPrintRequestID] [int] IDENTITY(1,1) NOT NULL,
	[StatementPrintRequestBatchID] [int] NULL,
	[StatementID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[FMNotificationDate] [datetime] NULL,
 CONSTRAINT [PK_StatementPrintRequest] PRIMARY KEY CLUSTERED 
(
	[StatementPrintRequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementPrintRequest]  WITH CHECK ADD  CONSTRAINT [FK_StatementPrintRequest_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementPrintRequest] CHECK CONSTRAINT [FK_StatementPrintRequest_Statement]
GO
ALTER TABLE [dbo].[StatementPrintRequest]  WITH CHECK ADD  CONSTRAINT [FK_StatementPrintRequest_StatementPrintRequestBatch] FOREIGN KEY([StatementPrintRequestBatchID])
REFERENCES [dbo].[StatementPrintRequestBatch] ([StatementPrintRequestBatchID])
GO
ALTER TABLE [dbo].[StatementPrintRequest] CHECK CONSTRAINT [FK_StatementPrintRequest_StatementPrintRequestBatch]
GO

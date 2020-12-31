USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementPrintRequestError]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementPrintRequestError](
	[StatementPrintRequestErrorID] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[StatementID] [int] NOT NULL,
	[StatementPrintRequestErrorTypeID] [int] NOT NULL,
	[IsReviewed] [bit] NOT NULL,
	[IsFixed] [bit] NOT NULL,
 CONSTRAINT [PK__StatementPrintRequestError__6D4D2A16] PRIMARY KEY CLUSTERED 
(
	[StatementPrintRequestErrorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementPrintRequestError]  WITH CHECK ADD  CONSTRAINT [FK_StatementPrintRequestError_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementPrintRequestError] CHECK CONSTRAINT [FK_StatementPrintRequestError_Statement]
GO
ALTER TABLE [dbo].[StatementPrintRequestError]  WITH CHECK ADD  CONSTRAINT [FK_StatementPrintRequestError_StatementPrintRequestErrorType] FOREIGN KEY([StatementPrintRequestErrorTypeID])
REFERENCES [dbo].[StatementPrintRequestErrorType] ([StatementPrintRequestErrorTypeID])
GO
ALTER TABLE [dbo].[StatementPrintRequestError] CHECK CONSTRAINT [FK_StatementPrintRequestError_StatementPrintRequestErrorType]
GO
ALTER TABLE [dbo].[StatementPrintRequestError] ADD  CONSTRAINT [DF_StatementPrintRequestError_IsReviewed]  DEFAULT ((0)) FOR [IsReviewed]
GO
ALTER TABLE [dbo].[StatementPrintRequestError] ADD  CONSTRAINT [DF_StatementPrintRequestError_IsFixed]  DEFAULT ((0)) FOR [IsFixed]
GO

USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementError]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementError](
	[StatementErrorID] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CampaignID] [int] NOT NULL,
	[StatementErrorTypeID] [int] NOT NULL,
	[IsReviewed] [bit] NOT NULL,
	[IsFixed] [bit] NOT NULL,
 CONSTRAINT [PK__StatementError__6D4D2A16] PRIMARY KEY CLUSTERED 
(
	[StatementErrorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementError]  WITH CHECK ADD  CONSTRAINT [FK_StatementError_StatementErrorType] FOREIGN KEY([StatementErrorTypeID])
REFERENCES [dbo].[StatementErrorType] ([StatementErrorTypeID])
GO
ALTER TABLE [dbo].[StatementError] CHECK CONSTRAINT [FK_StatementError_StatementErrorType]
GO
ALTER TABLE [dbo].[StatementError] ADD  CONSTRAINT [DF_StatementError_IsReviewed]  DEFAULT ((0)) FOR [IsReviewed]
GO
ALTER TABLE [dbo].[StatementError] ADD  CONSTRAINT [DF_StatementError_IsFixed]  DEFAULT ((0)) FOR [IsFixed]
GO

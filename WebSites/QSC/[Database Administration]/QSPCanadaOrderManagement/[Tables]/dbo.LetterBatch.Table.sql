USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[LetterBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LetterBatch](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LetterTemplateID] [int] NOT NULL,
	[LetterBatchType] [int] NOT NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[RunID] [int] NULL,
	[IsPrinted] [bit] NOT NULL,
	[DatePrinted] [datetime] NULL,
	[IsLocked] [bit] NOT NULL,
	[UserIDCreated] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DeletedTF] [bit] NOT NULL,
 CONSTRAINT [PK_LetterBatch] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LetterBatch]  WITH NOCHECK ADD  CONSTRAINT [FK_LetterBatch_LetterTemplate] FOREIGN KEY([LetterTemplateID])
REFERENCES [dbo].[LetterTemplate] ([ID])
GO
ALTER TABLE [dbo].[LetterBatch] CHECK CONSTRAINT [FK_LetterBatch_LetterTemplate]
GO

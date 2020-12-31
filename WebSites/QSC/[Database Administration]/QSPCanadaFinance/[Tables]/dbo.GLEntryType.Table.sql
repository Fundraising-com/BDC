USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GLEntryType]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GLEntryType](
	[GLEntryTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionTypeID] [int] NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_GLEntryType] PRIMARY KEY CLUSTERED 
(
	[GLEntryTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[GLEntryType]  WITH CHECK ADD  CONSTRAINT [FK_GLEntryType_TransactionType] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TransactionType] ([TransactionTypeID])
GO
ALTER TABLE [dbo].[GLEntryType] CHECK CONSTRAINT [FK_GLEntryType_TransactionType]
GO

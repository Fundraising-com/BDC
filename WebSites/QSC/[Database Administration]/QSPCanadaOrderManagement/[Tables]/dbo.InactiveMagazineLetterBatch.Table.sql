USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[InactiveMagazineLetterBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InactiveMagazineLetterBatch](
	[LetterBatchID] [int] NOT NULL,
	[ProductCode] [varchar](50) NOT NULL,
	[Reason] [int] NOT NULL,
 CONSTRAINT [PK_InactiveMagazineLetterTemplate] PRIMARY KEY CLUSTERED 
(
	[LetterBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[OrderOutput]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderOutput](
	[RemitBatchId] [int] NOT NULL,
	[FileContent] [text] NOT NULL,
 CONSTRAINT [PK_OrderOutput] PRIMARY KEY CLUSTERED 
(
	[RemitBatchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[xmldoc]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[xmldoc](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[doc] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

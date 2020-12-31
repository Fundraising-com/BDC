USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[OrderInEnvelopeMap]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInEnvelopeMap](
	[EnvelopeID] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
 CONSTRAINT [PK_StudentInEnvelopeMap] PRIMARY KEY CLUSTERED 
(
	[EnvelopeID] ASC,
	[CustomerOrderHeaderInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

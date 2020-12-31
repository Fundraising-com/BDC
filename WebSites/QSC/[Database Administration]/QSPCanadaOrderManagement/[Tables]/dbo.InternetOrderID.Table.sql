USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[InternetOrderID]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternetOrderID](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[InternetOrderID] [int] NULL,
 CONSTRAINT [PK_InternetOrderID] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

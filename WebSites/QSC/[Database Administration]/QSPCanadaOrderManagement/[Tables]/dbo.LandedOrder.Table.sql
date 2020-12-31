USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[LandedOrder]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandedOrder](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[LandedOrderID] [int] NULL,
 CONSTRAINT [PK_LandedOrder] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

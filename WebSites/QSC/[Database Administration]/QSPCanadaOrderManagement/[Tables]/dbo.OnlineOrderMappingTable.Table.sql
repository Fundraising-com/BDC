USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[OnlineOrderMappingTable]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlineOrderMappingTable](
	[LandedOrderID] [int] NOT NULL,
	[OnlineOrderID] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[StudentInstance] [int] NOT NULL,
 CONSTRAINT [PK_OnlineOrderMappingTable] PRIMARY KEY CLUSTERED 
(
	[LandedOrderID] ASC,
	[OnlineOrderID] ASC,
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC,
	[StudentInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

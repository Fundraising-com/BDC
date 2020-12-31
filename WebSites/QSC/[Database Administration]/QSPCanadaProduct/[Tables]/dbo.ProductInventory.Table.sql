USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[ProductInventory]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductInventory](
	[DistributionCenterName] [varchar](50) NOT NULL,
	[OracleCode] [varchar](50) NOT NULL,
	[QtyOnHand] [int] NULL,
	[QtyReserved] [int] NULL,
	[InventoryLoadDate] [datetime] NULL,
 CONSTRAINT [PK_ProductInventory] PRIMARY KEY CLUSTERED 
(
	[DistributionCenterName] ASC,
	[OracleCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

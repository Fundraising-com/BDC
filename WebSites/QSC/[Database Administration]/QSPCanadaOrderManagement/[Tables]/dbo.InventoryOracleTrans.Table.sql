USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[InventoryOracleTrans]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventoryOracleTrans](
	[ShipmentBatchID] [int] NOT NULL,
	[TransactionNumber] [int] IDENTITY(90000,1) NOT NULL,
	[QSPProductLine] [int] NULL,
	[OraLanguageCode] [varchar](10) NULL,
	[OracleCode] [varchar](20) NULL,
	[TransactionType] [varchar](1) NULL,
	[TransactionQty] [int] NULL,
	[Channel] [varchar](2) NULL,
	[NumberOfLine] [int] NULL,
	[DistributionCenterCode] [varchar](10) NULL,
	[ProductCondition] [varchar](1) NULL,
 CONSTRAINT [PK_InventoryOracleTrans] PRIMARY KEY CLUSTERED 
(
	[ShipmentBatchID] ASC,
	[TransactionNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

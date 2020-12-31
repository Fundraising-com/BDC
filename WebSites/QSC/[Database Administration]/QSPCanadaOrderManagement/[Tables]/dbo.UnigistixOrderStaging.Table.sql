USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[UnigistixOrderStaging]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnigistixOrderStaging](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[OrderID] [int] NULL,
	[EnvelopeID] [int] NULL,
	[StudentInstance] [int] NULL,
	[CustomerInstance] [int] NULL,
	[QtyShipped] [int] NULL,
	[ReplacedItemCode] [varchar](50) NULL,
	[ReplacedItemQty] [int] NULL,
	[PaymentStatusInstance] [int] NULL,
	[Recipient] [varchar](101) NULL,
	[ProductCode] [varchar](20) NULL,
	[ProductName] [varchar](50) NULL,
	[QuantityOrdered] [int] NULL,
	[Renewal] [char](1) NULL,
	[Price] [numeric](10, 2) NULL,
	[SupporterName] [varchar](50) NULL,
	[PriceOverrideID] [int] NULL,
	[CatalogPrice] [numeric](10, 2) NULL,
	[OracleCode] [varchar](50) NULL,
	[CatalogProductCode] [varchar](50) NULL,
	[Type] [int] NULL,
	[LevelCode] [varchar](10) NULL,
	[StatusInstance] [int] NULL,
 CONSTRAINT [PK_UnigistixOrderStaging] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

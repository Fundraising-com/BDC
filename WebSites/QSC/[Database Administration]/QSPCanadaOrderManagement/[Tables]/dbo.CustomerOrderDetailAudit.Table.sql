USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerOrderDetailAudit]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerOrderDetailAudit](
	[AuditDate] [datetime] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[Instance] [int] IDENTITY(1,1) NOT NULL,
	[CustomerShipToInstance] [int] NULL,
	[ProductCode] [varchar](20) NULL,
	[ProductName] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[Price] [numeric](10, 2) NULL,
	[PriceA] [numeric](10, 2) NULL,
	[Tax] [numeric](10, 2) NULL,
	[TaxA] [numeric](10, 2) NULL,
	[StatusInstance] [int] NULL,
	[DelFlag] [bit] NULL,
	[Renewal] [varchar](1) NULL,
	[Recipient] [varchar](81) NULL,
	[OverrideProduct] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CrossedBridgeDate] [datetime] NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NULL,
	[InvoiceNumber] [int] NULL,
	[AlphaProductCode] [varchar](4) NULL,
	[CouponPage] [varchar](2) NULL,
	[FDIndicator] [varchar](1) NULL,
	[MktgIndicator] [varchar](10) NULL,
	[ToteInstance] [int] NULL,
	[GiftCD] [varchar](3) NULL,
	[IsGift] [bit] NULL,
	[IsGiftCardSent] [bit] NULL,
	[SendGiftCardBeforeDate] [datetime] NULL,
	[ProgramSectionID] [int] NULL,
	[CatalogPrice] [numeric](10, 2) NULL,
	[QuantityReserved] [int] NULL,
	[PriceOverrideID] [int] NULL,
	[ProductType] [int] NULL,
	[PricingDetailsID] [int] NULL,
	[Tax2] [numeric](10, 2) NULL,
	[Tax2A] [numeric](10, 2) NULL,
	[Net] [numeric](10, 2) NULL,
	[Gross] [numeric](10, 2) NULL,
	[SupporterName] [varchar](81) NULL,
	[SendGiftCard] [bit] NULL,
	[QuantityShipped] [int] NULL,
	[ShipmentID] [int] NULL,
	[ReplacedProductCode] [varchar](20) NULL,
	[ReplacedProductQty] [int] NULL,
	[DistributionCenterID] [int] NULL,
	[Comment] [varchar](255) NULL,
	[CustomerComment] [varchar](255) NULL,
 CONSTRAINT [aaaaaCustomerOrderDetailAudit_PK] PRIMARY KEY CLUSTERED 
(
	[AuditDate] ASC,
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC,
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

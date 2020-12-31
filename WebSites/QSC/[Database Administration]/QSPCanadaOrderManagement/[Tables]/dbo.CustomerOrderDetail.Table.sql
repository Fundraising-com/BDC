USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerOrderDetail]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerOrderDetail](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[CustomerShipToInstance] [int] NULL,
	[ProductCode] [varchar](20) NULL,
	[ProductName] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[Price] [numeric](10, 2) NULL,
	[PriceA] [numeric](10, 2) NULL,
	[Tax] [numeric](14, 6) NULL,
	[TaxA] [numeric](14, 6) NULL,
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
	[Tax2] [numeric](14, 6) NULL,
	[Tax2A] [numeric](14, 6) NULL,
	[Net] [numeric](14, 6) NULL,
	[Gross] [numeric](14, 6) NULL,
	[SupporterName] [varchar](81) NULL,
	[SendGiftCard] [bit] NULL,
	[QuantityShipped] [int] NULL,
	[ShipmentID] [int] NULL,
	[ReplacedProductCode] [varchar](20) NULL,
	[ReplacedProductQty] [int] NULL,
	[DistributionCenterID] [int] NULL,
	[Comment] [varchar](255) NULL,
	[CustomerComment] [varchar](255) NULL,
	[IsVoucherRedemption] [bit] NULL,
	[ProductReplacementReasonID] [int] NULL,
	[IsShippedToAccount] [bit] NOT NULL,
 CONSTRAINT [aaaaaCustomerOrderDetail_PK] PRIMARY KEY NONCLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Custo__40A4A0D1]  DEFAULT (0) FOR [CustomerOrderHeaderInstance]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Trans__4198C50A]  DEFAULT (0) FOR [TransID]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Custo__428CE943]  DEFAULT (0) FOR [CustomerShipToInstance]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Produ__43810D7C]  DEFAULT (null) FOR [ProductCode]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Produ__447531B5]  DEFAULT (null) FOR [ProductName]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Quant__456955EE]  DEFAULT (0) FOR [Quantity]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Price__465D7A27]  DEFAULT (0.0) FOR [Price]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Price__47519E60]  DEFAULT (0.0) FOR [PriceA]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerOrd__Tax__4845C299]  DEFAULT (0.0) FOR [Tax]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerOr__TaxA__4939E6D2]  DEFAULT (0.0) FOR [TaxA]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Statu__4A2E0B0B]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Renew__4B222F44]  DEFAULT (null) FOR [Renewal]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Recip__4C16537D]  DEFAULT (null) FOR [Recipient]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Creat__4D0A77B6]  DEFAULT ('1/1/1995') FOR [CreationDate]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Cross__4DFE9BEF]  DEFAULT ('1/1/1995') FOR [CrossedBridgeDate]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Chang__4EF2C028]  DEFAULT (' ') FOR [ChangeUserID]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Chang__4FE6E461]  DEFAULT ('1/1/1995') FOR [ChangeDate]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Invoi__257187A8]  DEFAULT (0) FOR [InvoiceNumber]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Alpha__2665ABE1]  DEFAULT ('') FOR [AlphaProductCode]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__Coupo__2759D01A]  DEFAULT ('') FOR [CouponPage]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__FDInd__284DF453]  DEFAULT ('') FOR [FDIndicator]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__MktgI__7775B2CE]  DEFAULT ('') FOR [MktgIndicator]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__ToteI__5A054B78]  DEFAULT (0) FOR [ToteInstance]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF__CustomerO__GiftC__5046D714]  DEFAULT (null) FOR [GiftCD]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF_CustomerOrderDetail_IsGift]  DEFAULT (0) FOR [IsGift]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF_CustomerOrderDetail_SendGiftCardBeforeDate]  DEFAULT (1 / 1 / 95) FOR [SendGiftCardBeforeDate]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF_CustomerOrderDetail_QuantityShipped]  DEFAULT (0) FOR [QuantityShipped]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  CONSTRAINT [DF_CustomerOrderDetail_ReplacedProductCode]  DEFAULT (null) FOR [ReplacedProductCode]
GO
ALTER TABLE [dbo].[CustomerOrderDetail] ADD  DEFAULT ((0)) FOR [IsShippedToAccount]
GO

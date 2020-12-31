USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerOrderDetailRemitHistoryAudit]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerOrderDetailRemitHistoryAudit](
	[AuditDate] [datetime] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[RemitBatchID] [int] NOT NULL,
	[CustomerRemitHistoryInstance] [int] NOT NULL,
	[CountryCode] [dbo].[CountryCode_UDDT] NOT NULL,
	[Status] [int] NULL,
	[Quantity] [int] NULL,
	[RemitRate] [numeric](10, 2) NULL,
	[BasePrice] [numeric](10, 2) NULL,
	[CurrencyID] [int] NULL,
	[Lang] [varchar](10) NULL,
	[PremiumIndicator] [int] NULL,
	[PremiumCode] [varchar](20) NULL,
	[PremiumDescription] [varchar](50) NULL,
	[ABCCode] [varchar](20) NULL,
	[Renewal] [char](1) NULL,
	[TitleCode] [varchar](4) NULL,
	[MagazineTitle] [varchar](55) NULL,
	[CatalogPrice] [numeric](10, 2) NULL,
	[ItemPriceTotal] [numeric](18, 0) NULL,
	[NumberOfIssues] [int] NULL,
	[DefaultGrossValue] [numeric](10, 2) NULL,
	[Comment] [varchar](500) NULL,
	[SwitchLetterBatchID] [int] NULL,
	[GiftOrderType] [char](1) NULL,
	[GiftOrderStatus] [int] NULL,
	[GiftCardDateGenerated] [datetime] NULL,
	[SupporterName] [varchar](80) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [dbo].[UserID_UDDT] NULL,
	[EffortKey] [varchar](40) NULL,
	[tax] [numeric](14, 6) NULL,
	[tax2] [numeric](14, 6) NULL,
	[RemitCode] [varchar](4) NULL,
 CONSTRAINT [PK_CustomerOrderDetailRemitHistoryAudit] PRIMARY KEY NONCLUSTERED 
(
	[AuditDate] ASC,
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC,
	[RemitBatchID] ASC,
	[CustomerRemitHistoryInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

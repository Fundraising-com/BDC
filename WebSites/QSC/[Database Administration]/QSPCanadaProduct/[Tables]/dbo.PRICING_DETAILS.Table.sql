USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[PRICING_DETAILS]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PRICING_DETAILS](
	[MagPrice_Instance] [int] IDENTITY(1,1) NOT NULL,
	[Product_Instance] [int] NOT NULL,
	[Pricing_Year] [int] NOT NULL,
	[Pricing_Season] [char](1) NOT NULL,
	[Product_Code] [varchar](20) NOT NULL,
	[Program_ID] [int] NULL,
	[Program_Type] [varchar](25) NOT NULL,
	[ProgramSectionID] [int] NOT NULL,
	[Offer_Code] [int] NOT NULL,
	[Status] [int] NULL,
	[Remit_Rate] [numeric](10, 4) NULL,
	[Nbr_of_Issues] [int] NOT NULL,
	[Duration] [int] NULL,
	[Duration_Measure] [varchar](10) NULL,
	[NewsStand_Price_Yr] [numeric](12, 2) NULL,
	[Basic_Price_Yr] [numeric](12, 2) NULL,
	[QSP_Price] [numeric](12, 2) NOT NULL,
	[EffortKeyRequired] [bit] NULL,
	[Effort_Key] [varchar](40) NULL,
	[Logged_By] [varchar](15) NULL,
	[Log_Dt] [datetime] NULL,
	[EffectiveDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[NewsStandPriceOriginalCurrency] [numeric](9, 2) NULL,
	[ConversionRate] [numeric](10, 2) NULL,
	[Comment] [varchar](200) NULL,
	[DateSubmitted] [datetime] NULL,
	[BasePriceOriginalCurrency] [numeric](10, 2) NULL,
	[TaxRegionID] [int] NOT NULL,
	[DefaultGrossValue] [numeric](10, 2) NULL,
	[FSExtra_Limit_Rate] [numeric](18, 0) NULL,
	[FSIsBrochure] [bit] NULL,
	[FSApplicabilityId] [numeric](18, 0) NULL,
	[FSDistributionLevelId] [numeric](18, 0) NULL,
	[Language_Code] [varchar](10) NOT NULL,
	[FSCatalog_Product_Code] [varchar](50) NULL,
	[FSContent_Catalog_Code] [varchar](50) NULL,
	[FSProgram_Id] [int] NULL,
	[OracleCode] [varchar](50) NULL,
	[InternetApproval] [bit] NULL,
	[ABCCode] [varchar](20) NULL,
	[MagProgram_Instance] [int] NULL,
	[AdInCatalog] [bit] NULL,
	[AdPageSizeID] [int] NULL,
	[AdCost] [decimal](18, 0) NULL,
	[AdCostCurrency] [int] NULL,
	[ListingLevelID] [int] NULL,
	[ListingCopyText] [varchar](500) NULL,
	[QSPPremiumID] [int] NULL,
	[prdPremiumInd] [varchar](1) NULL,
	[prdPremiumCode] [varchar](50) NULL,
	[prdPremiumCopy] [varchar](500) NULL,
	[FSProvinceCode] [varchar](10) NULL,
	[ContractFormReceived] [int] NULL,
	[MagazineCoverFilename] [varchar](100) NULL,
	[CatalogAdFilename] [varchar](100) NULL,
	[CatalogPageNumber] [int] NULL,
	[PlacementLevel] [int] NULL,
	[ContractComment] [varchar](500) NULL,
	[PrinterComment] [varchar](500) NULL,
	[QSPCAListingCopyText] [varchar](500) NULL,
	[BasePriceSansPostage] [numeric](10, 2) NULL,
	[PostageRemitRate] [numeric](10, 4) NULL,
	[PostageAmount] [numeric](10, 2) NULL,
	[BaseRemitRate] [numeric](10, 4) NULL,
	[ListAgentCode] [varchar](5) NULL,
	[AddlHandlingFee] [money] NULL,
	[QSPAgencyCode] [varchar](20) NULL,
 CONSTRAINT [PK_PRICING_DETAILS] PRIMARY KEY CLUSTERED 
(
	[MagPrice_Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRICING_DETAILS', @level2type=N'COLUMN',@level2name=N'Language_Code'
GO
ALTER TABLE [dbo].[PRICING_DETAILS]  WITH CHECK ADD  CONSTRAINT [FK_PRICING_DETAILS_Product] FOREIGN KEY([Product_Instance])
REFERENCES [dbo].[Product] ([Product_Instance])
GO
ALTER TABLE [dbo].[PRICING_DETAILS] CHECK CONSTRAINT [FK_PRICING_DETAILS_Product]
GO
ALTER TABLE [dbo].[PRICING_DETAILS] ADD  CONSTRAINT [DF_PRICING_DETAILS_Language_Code]  DEFAULT ('EN') FOR [Language_Code]
GO
ALTER TABLE [dbo].[PRICING_DETAILS] ADD  CONSTRAINT [DF_PRICING_DETAILS_InternetApproval]  DEFAULT (0) FOR [InternetApproval]
GO

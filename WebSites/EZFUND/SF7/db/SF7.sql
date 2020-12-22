/* ==================================================================================================== */
/*
BEGIN 	TABLE REMOVAL
*/
/* ==================================================================================================== */
if exists (select * from dbo.sysobjects where id = object_id(N'[AddProductStyle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [AddProductStyle]

if exists (select * from dbo.sysobjects where id = object_id(N'[Addresses]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Addresses]

if exists (select * from dbo.sysobjects where id = object_id(N'[Admin]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Admin]

if exists (select * from dbo.sysobjects where id = object_id(N'[Administrators]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Administrators]

if exists (select * from dbo.sysobjects where id = object_id(N'[AffiliatePayments]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [AffiliatePayments]

if exists (select * from dbo.sysobjects where id = object_id(N'[AffiliateSettings]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [AffiliateSettings]

if exists (select * from dbo.sysobjects where id = object_id(N'[Affiliates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Affiliates]

if exists (select * from dbo.sysobjects where id = object_id(N'[AttributeDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [AttributeDetail]

if exists (select * from dbo.sysobjects where id = object_id(N'[Attributes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Attributes]

if exists (select * from dbo.sysobjects where id = object_id(N'[BundleGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [BundleGroups]

if exists (select * from dbo.sysobjects where id = object_id(N'[BundleOrderItems]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [BundleOrderItems]

if exists (select * from dbo.sysobjects where id = object_id(N'[BundleProductDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [BundleProductDetail]

if exists (select * from dbo.sysobjects where id = object_id(N'[BundleTemplateDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [BundleTemplateDetail]

if exists (select * from dbo.sysobjects where id = object_id(N'[CSROrders]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CSROrders]

if exists (select * from dbo.sysobjects where id = object_id(N'[Categories]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Categories]

if exists (select * from dbo.sysobjects where id = object_id(N'[CustomerDefinedBundleRules]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CustomerDefinedBundleRules]

if exists (select * from dbo.sysobjects where id = object_id(N'[CustomerPriceGroupCategories]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CustomerPriceGroupCategories]

if exists (select * from dbo.sysobjects where id = object_id(N'[CustomerPriceGroupManufacturers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CustomerPriceGroupManufacturers]

if exists (select * from dbo.sysobjects where id = object_id(N'[CustomerPriceGroupVendors]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CustomerPriceGroupVendors]

if exists (select * from dbo.sysobjects where id = object_id(N'[CustomerPriceGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CustomerPriceGroups]

if exists (select * from dbo.sysobjects where id = object_id(N'[CustomerSpecificPricing]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CustomerSpecificPricing]

if exists (select * from dbo.sysobjects where id = object_id(N'[Customers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Customers]

if exists (select * from dbo.sysobjects where id = object_id(N'[Designs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Designs]

if exists (select * from dbo.sysobjects where id = object_id(N'[Discounts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Discounts]

if exists (select * from dbo.sysobjects where id = object_id(N'[EMailContent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [EMailContent]

if exists (select * from dbo.sysobjects where id = object_id(N'[Employees]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Employees]

if exists (select * from dbo.sysobjects where id = object_id(N'[GiftCertificates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [GiftCertificates]

if exists (select * from dbo.sysobjects where id = object_id(N'[GiftWrap]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [GiftWrap]

if exists (select * from dbo.sysobjects where id = object_id(N'[HomePage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [HomePage]

if exists (select * from dbo.sysobjects where id = object_id(N'[Images]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Images]

if exists (select * from dbo.sysobjects where id = object_id(N'[ImagesPreview]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ImagesPreview]

if exists (select * from dbo.sysobjects where id = object_id(N'[ImportXMLLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ImportXMLLog]

if exists (select * from dbo.sysobjects where id = object_id(N'[Instructions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Instructions]

if exists (select * from dbo.sysobjects where id = object_id(N'[Integration]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Integration]

if exists (select * from dbo.sysobjects where id = object_id(N'[IntegrationCustomers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [IntegrationCustomers]

if exists (select * from dbo.sysobjects where id = object_id(N'[Inventory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Inventory]

if exists (select * from dbo.sysobjects where id = object_id(N'[InventoryInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [InventoryInfo]

if exists (select * from dbo.sysobjects where id = object_id(N'[Labels]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Labels]

if exists (select * from dbo.sysobjects where id = object_id(N'[Layout]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Layout]

if exists (select * from dbo.sysobjects where id = object_id(N'[LayoutPreview]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [LayoutPreview]

if exists (select * from dbo.sysobjects where id = object_id(N'[Manufacturers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Manufacturers]

if exists (select * from dbo.sysobjects where id = object_id(N'[MenuBar]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [MenuBar]

if exists (select * from dbo.sysobjects where id = object_id(N'[MenuBarPreview]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [MenuBarPreview]

if exists (select * from dbo.sysobjects where id = object_id(N'[MerchantToolsMenuDetails]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [MerchantToolsMenuDetails]

if exists (select * from dbo.sysobjects where id = object_id(N'[MerchantToolsMenuHeaders]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [MerchantToolsMenuHeaders]

if exists (select * from dbo.sysobjects where id = object_id(N'[Messages]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Messages]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderAddresses]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderAddresses]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderDiscounts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderDiscounts]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderGiftCertificates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderGiftCertificates]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderGiftWrap]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderGiftWrap]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderItemDiscounts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderItemDiscounts]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderItems]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderItems]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderItemsAttributes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderItemsAttributes]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderPayment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderPayment]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrderTracking]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrderTracking]

if exists (select * from dbo.sysobjects where id = object_id(N'[Orders]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Orders]

if exists (select * from dbo.sysobjects where id = object_id(N'[OrdersBackOrderBilling]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [OrdersBackOrderBilling]

if exists (select * from dbo.sysobjects where id = object_id(N'[PayPalUsers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [PayPalUsers]

if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentMethods]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [PaymentMethods]

if exists (select * from dbo.sysobjects where id = object_id(N'[PaymentProcessors]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [PaymentProcessors]

if exists (select * from dbo.sysobjects where id = object_id(N'[ProcessorResponse]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ProcessorResponse]

if exists (select * from dbo.sysobjects where id = object_id(N'[ProductCategory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ProductCategory]

if exists (select * from dbo.sysobjects where id = object_id(N'[ProductDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ProductDetail]

if exists (select * from dbo.sysobjects where id = object_id(N'[Products]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Products]

if exists (select * from dbo.sysobjects where id = object_id(N'[ProductsBundle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ProductsBundle]

if exists (select * from dbo.sysobjects where id = object_id(N'[QBAccounts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [QBAccounts]

if exists (select * from dbo.sysobjects where id = object_id(N'[QBConversion]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [QBConversion]

if exists (select * from dbo.sysobjects where id = object_id(N'[QBSettings]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [QBSettings]

if exists (select * from dbo.sysobjects where id = object_id(N'[QBTaxGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [QBTaxGroups]

if exists (select * from dbo.sysobjects where id = object_id(N'[QBTaxRates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [QBTaxRates]

if exists (select * from dbo.sysobjects where id = object_id(N'[QBTemplates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [QBTemplates]

if exists (select * from dbo.sysobjects where id = object_id(N'[RecurringTransactionResponse]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [RecurringTransactionResponse]

if exists (select * from dbo.sysobjects where id = object_id(N'[RelatedProducts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [RelatedProducts]

if exists (select * from dbo.sysobjects where id = object_id(N'[RoleTasks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [RoleTasks]

if exists (select * from dbo.sysobjects where id = object_id(N'[SavedCartAttributes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SavedCartAttributes]

if exists (select * from dbo.sysobjects where id = object_id(N'[SavedCartItems]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SavedCartItems]

if exists (select * from dbo.sysobjects where id = object_id(N'[SavedCarts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SavedCarts]

if exists (select * from dbo.sysobjects where id = object_id(N'[SearchFilters]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SearchFilters]

if exists (select * from dbo.sysobjects where id = object_id(N'[SearchOptions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SearchOptions]

if exists (select * from dbo.sysobjects where id = object_id(N'[SearchResults]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SearchResults]

if exists (select * from dbo.sysobjects where id = object_id(N'[SecurityLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SecurityLog]

if exists (select * from dbo.sysobjects where id = object_id(N'[SelectCountry]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SelectCountry]

if exists (select * from dbo.sysobjects where id = object_id(N'[SelectISOCurrency]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SelectISOCurrency]

if exists (select * from dbo.sysobjects where id = object_id(N'[SelectLocal]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SelectLocal]

if exists (select * from dbo.sysobjects where id = object_id(N'[SelectState]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [SelectState]

if exists (select * from dbo.sysobjects where id = object_id(N'[Shipping]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Shipping]

if exists (select * from dbo.sysobjects where id = object_id(N'[ShippingCarriers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ShippingCarriers]

if exists (select * from dbo.sysobjects where id = object_id(N'[ShoppingCart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ShoppingCart]

if exists (select * from dbo.sysobjects where id = object_id(N'[ShoppingCartAttributes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ShoppingCartAttributes]

if exists (select * from dbo.sysobjects where id = object_id(N'[ShoppingCartGiftWrap]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ShoppingCartGiftWrap]

if exists (select * from dbo.sysobjects where id = object_id(N'[ShoppingCartItems]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ShoppingCartItems]

if exists (select * from dbo.sysobjects where id = object_id(N'[Swatch]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Swatch]

if exists (select * from dbo.sysobjects where id = object_id(N'[TaxRates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TaxRates]

if exists (select * from dbo.sysobjects where id = object_id(N'[TaxTypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TaxTypes]

if exists (select * from dbo.sysobjects where id = object_id(N'[UserRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [UserRoles]

if exists (select * from dbo.sysobjects where id = object_id(N'[ValueShipping]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ValueShipping]

if exists (select * from dbo.sysobjects where id = object_id(N'[Vendors]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Vendors]

if exists (select * from dbo.sysobjects where id = object_id(N'[VolumePricing]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [VolumePricing]

if exists (select * from dbo.sysobjects where id = object_id(N'[Walkin]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Walkin]
GO
/* ==================================================================================================== */
/*
END 	TABLE REMOVAL
BEGIN	TABLE CREATION
*/
/* ==================================================================================================== */
CREATE TABLE [AddProductStyle] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DisplayProductCode] [int] NULL ,
	[DisplayProductName] [int] NULL ,
	[DisplayQuantity] [int] NULL ,
	[DisplayUpSellMessage] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [Addresses] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CustomerID] [int] NULL ,
	[Type] [int] NULL ,
	[NickName] [nvarchar] (50) NULL ,
	[FirstName] [nvarchar] (100) NULL ,
	[MI] [nvarchar] (2) NULL ,
	[LastName] [nvarchar] (100) NULL ,
	[Company] [nvarchar] (75) NULL ,
	[Address1] [nvarchar] (255) NULL ,
	[Address2] [nvarchar] (255) NULL ,
	[City] [nvarchar] (50) NULL ,
	[State] [nvarchar] (50) NULL ,
	[Zip] [nvarchar] (50) NULL ,
	[Country] [nvarchar] (50) NULL ,
	[Phone] [nvarchar] (50) NULL ,
	[Fax] [nvarchar] (50) NULL ,
	[EMail] [nvarchar] (255) NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_CUSTOMERID] ON [Addresses]([CustomerID]) ON [PRIMARY]

CREATE TABLE [Admin] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[PrimaryEmail] [nvarchar] (100) NULL ,
	[SecondaryEmail] [nvarchar] (100) NULL ,
	[EMailMethod] [nvarchar] (25) NULL ,
	[EMailServer] [nvarchar] (255) NULL ,
	[SSLPath] [nvarchar] (255) NULL ,
	[SiteURL] [nvarchar] (255) NULL ,
	[WebLicense] [ntext] NULL ,
	[SearchOnFields] [nvarchar] (255) NULL ,
	[PromoMailServer] [varchar] (50) NULL ,
	[ComponentLicense] [ntext] NULL ,
	[TransMethod] [nvarchar] (50) NULL ,
	[StoreName] [nvarchar] (50) NULL ,
	[AddressID] [int] NULL ,
	[ShipType] [smallint] NULL ,
	[ShipType2] [smallint] NULL ,
	[PrmShipIsActive] [smallint] NULL ,
	[HandlingIsActive] [smallint] NULL ,
	[DeletePolicy] [nvarchar] (1) NULL ,
	[DeleteSchedule] [smallint] NULL ,
	[ISOCurrency] [nvarchar] (50) NULL ,
	[LCID] [nvarchar] (5) NULL ,
	[ShipMin] [nvarchar] (10) NULL ,
	[CODAmount] [nvarchar] (50) NULL ,
	[SpcShipAmt] [nvarchar] (10) NULL ,
	[OandaID] [nvarchar] (50) NULL ,
	[ActivateOanda] [smallint] NULL ,
	[LivePersonID] [nvarchar] (50) NULL ,
	[LivePersonActive] [smallint] NULL ,
	[SFID] [nvarchar] (255) NULL ,
	[PhysPath] [ntext] NULL ,
	[BackOrderBilling] [smallint] NULL ,
	[TopMenuBarNav] [int] NULL ,
	[FooterNav] [int] NULL ,
	[LeftNav] [int] NULL ,
	[RightNav] [int] NULL ,
	[AllowMultipleCoupons] [int] NULL ,
	[AllowCouponWithDiscount] [int] NULL ,
	[TaxShipping] [int] NULL ,
	[TaxHandling] [int] NULL ,
	[AdditionalAddressHandling] [nvarchar] (50) NULL ,
	[AllOrderHandling] [int] NULL ,
	[HandlingAmount] [nvarchar] (50) NULL ,
	[AddProductStyle] [int] NULL ,
	[MetaTag] [ntext] NULL ,
	[MetaDescription] [nvarchar] (255) NULL ,
	[NextOrderNumber] [numeric](10, 0) NULL ,
	[DatabaseType] [int] NULL ,
	[CCOnline] [int] NULL ,
	[ECheckOnline] [int] NULL ,
	[CVVIsActive] [int] NULL ,
	[ProductSort] [nvarchar] (50) NULL ,
	[PremShipLabel] [nvarchar] (50) NULL ,
	[AcceptCC] [int] NULL ,
	[MMPassword] [nvarchar] (100) NULL ,
	[MMUserName] [nvarchar] (100) NULL ,
	[SiteKeywords] [ntext] NULL ,
	[SiteDescription] [ntext] NULL ,
	[sfLogin] [varchar] (100) NULL ,
	[sfPassword] [varchar] (100) NULL ,
	[AllowMultiShip] [int] NULL ,
	[AdminGuid] [nvarchar] (50) NULL ,
	[EncryptionPublicKey] [text] NULL ,
	[ConvertedFrom3DES] [bit] NULL DEFAULT (0),
	[StoreFrontLinkValue] [int] NULL ,
	[AcceptPayPalExpress] [int] NULL DEFAULT (0),
	[PayPalFirstPartyCertificate] [int] NULL DEFAULT (0),
	[AllowAnonymous] [bit] NOT NULL DEFAULT (0),
	[AccountCreationAllowed] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [Administrators] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[FirstName] [varchar] (50) NOT NULL ,
	[LastName] [varchar] (50) NOT NULL ,
	[UserName] [varchar] (100) NOT NULL ,
	[Password] [varchar] (500) NOT NULL ,
	[RoleId] [int] NOT NULL ,
	[IsSuperUser] [bit] NOT NULL DEFAULT (0),
	[NumFailedAttempt] [int] NOT NULL DEFAULT (0),
	[isLocked] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [AffiliatePayments] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[paymentDate] [nvarchar] (20) NULL ,
	[affId] [int] NULL ,
	[Void] [int] NULL ,
	[AmountPaid] [nvarchar] (20) NULL 
) ON [PRIMARY]

CREATE TABLE [AffiliateSettings] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Active] [int] NULL ,
	[terms] [ntext] NULL ,
	[Payout] [nvarchar] (20) NULL ,
	[MinPayout] [nvarchar] (10) NULL ,
	[PayOutRule] [int] NULL ,
	[CommissionType] [int] NOT NULL ,
	[FlatFee] [nvarchar] (20) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [Affiliates] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar] (50) NULL ,
	[AddressID] [int] NULL ,
	[HomePage] [nvarchar] (100) NULL ,
	[CurrentEarnings] [nvarchar] (50) NULL ,
	[Pass] [nvarchar] (50) NULL ,
	[Email] [nvarchar] (100) NULL ,
	[CommissionType] [int] NULL ,
	[PayOutRule] [int] NULL ,
	[PayOut] [nvarchar] (10) NULL ,
	[MinPayOut] [nvarchar] (10) NULL 
) ON [PRIMARY]

CREATE TABLE [AttributeDetail] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[AttributeID] [int] NULL ,
	[Name] [nvarchar] (255) NULL ,
	[Price] [nvarchar] (50) NULL ,
	[Weight] [nvarchar] (50) NULL ,
	[PriceType] [tinyint] NULL ,
	[WeightType] [tinyint] NULL ,
	[AttributeOrder] [int] NULL ,
	[SmallImage] [nvarchar] (100) NULL ,
	[LargeImage] [nvarchar] (100) NULL ,
	[FileLocation] [nvarchar] (255) NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_ATTRIBUTEID] ON [AttributeDetail]([AttributeID]) ON [PRIMARY]

CREATE TABLE [Attributes] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NULL ,
	[Name] [nvarchar] (200) NULL ,
	[Type] [tinyint] NULL ,
	[Required] [int] NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_PRODUCTID] ON [Attributes]([ProductID]) ON [PRIMARY]
CREATE  INDEX [IX_NAME] ON [Attributes]([Name]) ON [PRIMARY]

CREATE TABLE [BundleGroups] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[BundleGroupName] [nvarchar] (100) NOT NULL 
) ON [PRIMARY]

CREATE TABLE [BundleOrderItems] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderItemID] [int] NOT NULL DEFAULT (0),
	[ProductID] [int] NOT NULL DEFAULT (0),
	[Quantity] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [BundleProductDetail] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[StepID] [int] NOT NULL DEFAULT (0),
	[ProductID] [int] NOT NULL DEFAULT (0),
	[Quantity] [int] NOT NULL DEFAULT (0),
	[DisplayOrder] [int] NOT NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [BundleTemplateDetail] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NOT NULL ,
	[StepName] [nvarchar] (50) NOT NULL DEFAULT (''),
	[SelectableQty] [int] NOT NULL DEFAULT (0),
	[DisplayOrder] [int] NOT NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [CSROrders] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderID] [int] NOT NULL ,
	[CSRID] [int] NOT NULL 
) ON [PRIMARY]

CREATE TABLE [Categories] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[IsActive] [smallint] NULL ,
	[ParentLevel] [int] NULL ,
	[ParentID] [int] NULL ,
	[Name] [nvarchar] (255) NULL ,
	[Description] [nvarchar] (50) NULL ,
	[ImagePath] [nvarchar] (255) NULL ,
	[SortOrder] [int] NULL ,
	[Featured] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY]
CREATE  INDEX [IX_ISACTIVE] ON [Categories]([IsActive]) ON [PRIMARY]
CREATE  INDEX [IX_PARENTLEVEL] ON [Categories]([ParentLevel]) ON [PRIMARY]
CREATE  INDEX [IX_PARENTID] ON [Categories]([ParentID]) ON [PRIMARY]
CREATE  INDEX [IX_NAME] ON [Categories]([Name]) ON [PRIMARY]

CREATE TABLE [CustomerDefinedBundleRules] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NOT NULL ,
	[BundleRuleDetail] [varchar] (1000) NOT NULL 
) ON [PRIMARY]

CREATE TABLE [CustomerPriceGroupCategories] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CustomerPriceGroupId] [int] NOT NULL ,
	[CategoryId] [int] NOT NULL 
) ON [PRIMARY]

CREATE TABLE [CustomerPriceGroupManufacturers] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CustomerPriceGroupId] [int] NOT NULL ,
	[ManufacturerId] [int] NOT NULL 
) ON [PRIMARY]

CREATE TABLE [CustomerPriceGroupVendors] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CustomerPriceGroupId] [int] NOT NULL ,
	[VendorId] [int] NOT NULL 
) ON [PRIMARY]

CREATE TABLE [CustomerPriceGroups] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[GroupName] [nvarchar] (50) NULL ,
	[Amount] [nvarchar] (20) NULL ,
	[GroupTypeID] [int] NULL ,
	[AllProducts] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [CustomerSpecificPricing] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NULL ,
	[GroupID] [int] NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_PRODUCTID] ON [CustomerSpecificPricing]([ProductID]) ON [PRIMARY]
CREATE  INDEX [IX_GROUPID] ON [CustomerSpecificPricing]([GroupID]) ON [PRIMARY]

CREATE TABLE [Customers] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[FirstName] [nvarchar] (255) NULL ,
	[LastName] [nvarchar] (255) NULL ,
	[Pass] [nvarchar] (255) NULL ,
	[EMail] [nvarchar] (255) NULL ,
	[Subscribed] [int] NULL ,
	[CustGroupID] [int] NULL ,
	[Referrer] [int] NULL ,
	[HttpReferrer] [nvarchar] (255) NULL ,
	[ReferrerDate] [nvarchar] (50) NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_CUSTGROUPID] ON [Customers]([CustGroupID]) ON [PRIMARY]

CREATE TABLE [Designs] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar] (100) NULL ,
	[IsActive] [int] NULL ,
	[Path] [nvarchar] (255) NULL DEFAULT (N'~/'),
	[Group] [nvarchar] (100) NULL ,
	[Thumbnail] [nvarchar] (255) NULL ,
	[Categories] [nvarchar] (50) NULL 
) ON [PRIMARY]

CREATE TABLE [Discounts] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar] (50) NULL ,
	[DiscountType] [int] NULL ,
	[PercentOff] [nvarchar] (50) NULL ,
	[DollarOff] [nvarchar] (50) NULL ,
	[Expires] [nvarchar] (50) NULL ,
	[MinAmount] [nvarchar] (50) NULL ,
	[IsActive] [int] NULL ,
	[AppliedTo] [int] NULL ,
	[AppliedToID] [int] NULL ,
	[Code] [nvarchar] (100) NULL ,
	[MinAmountType] [int] NULL ,
	[Description] [nvarchar] (100) NULL ,
	[ApplyOnce] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [EMailContent] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Type] [int] NULL ,
	[Subject] [nvarchar] (255) NULL ,
	[Body] [ntext] NULL ,
	[Format] [nvarchar] (4) NULL ,
	[IsActive] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [Employees] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[UserName] [nvarchar] (255) NULL ,
	[PassWord] [nvarchar] (255) NULL ,
	[OverridePricing] [bit] NULL ,
	[OverrideShippingCharges] [bit] NULL ,
	[OverrideTaxes] [bit] NULL 
) ON [PRIMARY]

CREATE TABLE [GiftCertificates] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar] (50) NULL ,
	[DollarOff] [nvarchar] (50) NULL ,
	[Expires] [nvarchar] (50) NULL ,
	[Code] [nvarchar] (100) NULL ,
	[IsActive] [int] NULL ,
	[Description] [nvarchar] (100) NULL 
) ON [PRIMARY]

CREATE TABLE [GiftWrap] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NULL ,
	[IsActive] [int] NULL ,
	[Price] [nvarchar] (10) NULL ,
	[Message] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE  INDEX [IX_PRODUCTID] ON [GiftWrap]([ProductID]) ON [PRIMARY]

CREATE TABLE [HomePage] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[IsActive] [bit] NOT NULL 
) ON [PRIMARY]

CREATE TABLE [Images] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DesignID] [int] NULL ,
	[Name] [nvarchar] (50) NULL ,
	[Filename] [nvarchar] (50) NULL 
) ON [PRIMARY]

CREATE TABLE [ImagesPreview] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DesignID] [int] NULL ,
	[Name] [nvarchar] (50) NULL ,
	[Filename] [nvarchar] (255) NULL 
) ON [PRIMARY]

CREATE TABLE [ImportXMLLog] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Action] [varchar] (50) NOT NULL ,
	[Date] [datetime] NOT NULL ,
	[XML] [ntext] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [Instructions] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[PageName] [nvarchar] (50) NULL ,
	[Instruction] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [Integration] (
	[uid] [int] IDENTITY (0, 1) NOT NULL PRIMARY KEY,
	[LastExport] [datetime] NULL ,
	[LastOrderIDExported] [int] NULL ,
	[SubmitOrderToWebService] [bit] NULL DEFAULT (0),
	[NewOrderWebServiceUrl] [nvarchar] (1000) NULL ,
	[CheckInvOnAddToCart] [bit] NULL DEFAULT (0),
	[CheckInvOnOrderPlaced] [bit] NULL DEFAULT (0),
	[ProductCheckWebServiceUrl] [nvarchar] (1000) NULL ,
	[OutboundUsername] [nvarchar] (255) NULL ,
	[OutboundPassword] [nvarchar] (255) NULL ,
	[InboundUsername] [nvarchar] (255) NULL ,
	[InboundPassword] [nvarchar] (255) NULL ,
	[ErrorEmail] [nvarchar] (255) NULL 
) ON [PRIMARY]

CREATE TABLE [IntegrationCustomers] (
	[uid] [int] IDENTITY (0, 1) NOT NULL PRIMARY KEY,
	[CustomerUID] [int] NULL ,
	[AccountingNumber] [nvarchar] (200) NULL 
) ON [PRIMARY]

CREATE TABLE [Inventory] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NULL ,
	[AttributeDetailID] [nvarchar] (100) NULL ,
	[AttributeName] [nvarchar] (255) NULL ,
	[QtyInStock] [int] NULL ,
	[QtyLowFlag] [int] NULL ,
	[OnOrder] [int] NULL ,
	[Sku] [nvarchar] (255) NULL ,
	[Valid] [bit] NOT NULL DEFAULT (1)
) ON [PRIMARY]
CREATE  INDEX [IX_PRODUCTID] ON [Inventory]([ProductID]) ON [PRIMARY]
CREATE  INDEX [IX_ATTRIBUTEDETAILID] ON [Inventory]([AttributeDetailID]) ON [PRIMARY]

CREATE TABLE [InventoryInfo] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProdID] [int] NULL ,
	[OnOrder] [int] NULL ,
	[Tracked] [int] NULL ,
	[Status] [int] NULL ,
	[Notify] [int] NULL ,
	[InStock] [int] NULL ,
	[LowFlag] [int] NULL ,
	[CanBackOrder] [int] NULL ,
	[DefaultQTY] [int] NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_PRODID] ON [InventoryInfo]([ProdID]) ON [PRIMARY]

CREATE TABLE [Labels] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductCode] [nvarchar] (50) NULL ,
	[ProductName] [nvarchar] (50) NULL ,
	[Description] [nvarchar] (50) NULL ,
	[Price] [nvarchar] (50) NULL ,
	[VolumePrice] [nvarchar] (50) NULL ,
	[Stock] [nvarchar] (50) NULL ,
	[Category] [nvarchar] (50) NULL ,
	[CategoryPlural] [nvarchar] (50) NULL ,
	[Manufacturer] [nvarchar] (50) NULL ,
	[ManufacturerPlural] [nvarchar] (50) NULL ,
	[Vendor] [nvarchar] (50) NULL ,
	[VendorPlural] [nvarchar] (50) NULL ,
	[MoreInfo] [nvarchar] (50) NULL ,
	[SalePrice] [nvarchar] (50) NULL ,
	[NoPriceGroup] [nvarchar] (50) NULL ,
	[SubscriptionPrice] [nvarchar] (50) NULL ,
	[RecurringPrice] [nvarchar] (50) NULL ,
	[ClearancePrice] [nvarchar] (50) NULL 
) ON [PRIMARY]

CREATE TABLE [Layout] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DesignID] [int] NULL ,
	[Name] [nvarchar] (50) NULL ,
	[TableWidth] [nvarchar] (50) NULL ,
	[CellPadding] [nvarchar] (50) NULL ,
	[CellSpacing] [nvarchar] (50) NULL ,
	[BorderSize] [nvarchar] (50) NULL ,
	[BorderColor] [nvarchar] (10) NULL ,
	[HorizontalAlignment] [nvarchar] (50) NULL ,
	[VerticalAlignment] [nvarchar] (50) NULL ,
	[TopMargin] [int] NULL ,
	[RightMargin] [int] NULL ,
	[BottomMargin] [int] NULL ,
	[LeftMargin] [int] NULL ,
	[BackgroundImageURL] [nvarchar] (255) NULL ,
	[BackgroundColor] [nvarchar] (10) NULL ,
	[FontFace] [nvarchar] (50) NULL ,
	[FontSize] [int] NULL ,
	[FontColor] [nvarchar] (10) NULL ,
	[FontStyle] [nvarchar] (50) NULL ,
	[ImageURL] [nvarchar] (255) NULL ,
	[DisplayStyle] [int] NULL ,
	[Visible] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [LayoutPreview] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DesignID] [int] NULL ,
	[Name] [nvarchar] (50) NULL ,
	[TableWidth] [nvarchar] (50) NULL ,
	[CellPadding] [nvarchar] (50) NULL ,
	[CellSpacing] [nvarchar] (50) NULL ,
	[BorderSize] [nvarchar] (50) NULL ,
	[BorderColor] [nvarchar] (10) NULL ,
	[HorizontalAlignment] [nvarchar] (50) NULL ,
	[VerticalAlignment] [nvarchar] (50) NULL ,
	[TopMargin] [int] NULL ,
	[RightMargin] [int] NULL ,
	[BottomMargin] [int] NULL ,
	[LeftMargin] [int] NULL ,
	[BackgroundImageURL] [nvarchar] (255) NULL ,
	[BackgroundColor] [nvarchar] (10) NULL ,
	[FontFace] [nvarchar] (50) NULL ,
	[FontSize] [int] NULL ,
	[FontColor] [nvarchar] (10) NULL ,
	[FontStyle] [nvarchar] (50) NULL ,
	[ImageURL] [nvarchar] (255) NULL ,
	[DisplayStyle] [int] NULL ,
	[Visible] [int] NULL
) ON [PRIMARY]

CREATE TABLE [Manufacturers] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[AddressID] [int] NULL ,
	[Active] [int] NOT NULL DEFAULT (0)
) ON [PRIMARY]
CREATE  INDEX [IX_ADDRESSID] ON [Manufacturers]([AddressID]) ON [PRIMARY]

CREATE TABLE [MenuBar] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DesignID] [int] NULL ,
	[PageName] [nvarchar] (50) NULL ,
	[MenuText] [nvarchar] (50) NULL ,
	[MenuImage] [nvarchar] (255) NULL ,
	[MenuOffImage] [nvarchar] (255) NULL ,
	[Link] [nvarchar] (255) NULL ,
	[MenuPosition] [int] NULL ,
	[LinkVisibility] [int] NULL ,
	[OrderPosition] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [MenuBarPreview] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DesignID] [int] NULL ,
	[PageName] [nvarchar] (50) NULL ,
	[MenuText] [nvarchar] (50) NULL ,
	[MenuImage] [nvarchar] (255) NULL ,
	[MenuOffImage] [nvarchar] (255) NULL ,
	[Link] [nvarchar] (255) NULL ,
	[MenuPosition] [int] NULL ,
	[LinkVisibility] [int] NULL ,
	[OrderPosition] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [MerchantToolsMenuDetails] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[HeaderId] [int] NOT NULL ,
	[DisplayName] [varchar] (255) NOT NULL ,
	[Link] [varchar] (255) NOT NULL ,
	[DisplayOrder] [int] NOT NULL ,
	[Version] [bit] NOT NULL 
) ON [PRIMARY]

CREATE TABLE [MerchantToolsMenuHeaders] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DisplayName] [varchar] (255) NOT NULL ,
	[DisplayOrder] [int] NOT NULL 
) ON [PRIMARY]

CREATE TABLE [Messages] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Page] [nvarchar] (50) NULL ,
	[Action] [nvarchar] (50) NULL ,
	[Condition] [nvarchar] (50) NULL ,
	[Message] [varchar] (2000) NULL 
) ON [PRIMARY]

CREATE TABLE [OrderAddresses] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderID] [int] NULL ,
	[Type] [int] NULL ,
	[FirstName] [nvarchar] (100) NULL ,
	[MI] [nvarchar] (2) NULL ,
	[LastName] [nvarchar] (100) NULL ,
	[Company] [nvarchar] (75) NULL ,
	[Address1] [nvarchar] (255) NULL ,
	[Address2] [nvarchar] (255) NULL ,
	[City] [nvarchar] (50) NULL ,
	[State] [nvarchar] (50) NULL ,
	[Zip] [nvarchar] (50) NULL ,
	[Country] [nvarchar] (50) NULL ,
	[Phone] [nvarchar] (50) NULL ,
	[Fax] [nvarchar] (50) NULL ,
	[EMail] [nvarchar] (255) NULL ,
	[ShipMethod] [nvarchar] (255) NULL ,
	[SpecialInstruction] [ntext] NULL ,
	[CountryTaxRate] [nvarchar] (50) NULL ,
	[StateTaxRate] [nvarchar] (50) NULL ,
	[LocalTaxRate] [nvarchar] (50) NULL ,
	[DiscountTotal] [nvarchar] (50) NULL ,
	[IsShippingTaxed] [int] NULL ,
	[ShippableShippingTotal] [nvarchar] (50) NULL ,
	[HandlingTotal] [nvarchar] (50) NULL ,
	[ShipType] [int] NULL ,
	[BackOrderShippingTotal] [nvarchar] (50) NULL ,
	[BOQuantity] [int] NULL ,
	[NickName] [nvarchar] (50) NULL ,
	[BOPending] [int] NULL ,
	[Pending] [int] NULL ,
	[ShipCarrierCode] [nvarchar] (10) NULL ,
	[TotalBilled] [nvarchar] (50) NULL ,
	[AmountRemaining] [nvarchar] (50) NULL ,
	[CODAmount] [nvarchar] (20) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE  INDEX [IX_ORDERID] ON [OrderAddresses]([OrderID]) ON [PRIMARY]

CREATE TABLE [OrderDiscounts] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[DiscountID] [int] NOT NULL ,
	[Name] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (100) NOT NULL ,
	[Code] [nvarchar] (100) NULL 
) ON [PRIMARY]

CREATE TABLE [OrderGiftCertificates] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderId] [int] NOT NULL ,
	[GiftCertificateId] [int] NOT NULL ,
	[Name] [nvarchar] (50) NULL ,
	[Expires] [nvarchar] (50) NULL ,
	[Code] [nvarchar] (50) NULL ,
	[AmountUsed] [nvarchar] (50) NULL 
) ON [PRIMARY]

CREATE TABLE [OrderGiftWrap] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderItemID] [int] NULL ,
	[MessageTo] [nvarchar] (50) NULL ,
	[MessageFrom] [nvarchar] (50) NULL ,
	[Message] [nvarchar] (255) NULL 
) ON [PRIMARY]

CREATE TABLE [OrderItemDiscounts] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderDiscountID] [int] NOT NULL ,
	[OrderItemID] [int] NOT NULL ,
	[DiscountAmount] [decimal](19, 2) NOT NULL 
) ON [PRIMARY]

CREATE TABLE [OrderItems] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderID] [int] NULL ,
	[ProductID] [int] NULL ,
	[AddressID] [int] NULL ,
	[Quantity] [int] NULL ,
	[ProductName] [nvarchar] (255) NULL ,
	[IsOnSale] [int] NULL ,
	[Price] [nvarchar] (20) NULL ,
	[SalePrice] [nvarchar] (20) NULL ,
	[IsGiftWrap] [int] NULL ,
	[GiftwrapPrice] [nvarchar] (20) NULL ,
	[GiftWrapQuantity] [int] NULL ,
	[ProductCode] [nvarchar] (50) NULL ,
	[IsShipable] [int] NULL ,
	[ShipPrice] [nvarchar] (50) NULL ,
	[Cost] [nvarchar] (50) NULL ,
	[HasCountryTax] [int] NULL ,
	[HasStateTax] [int] NULL ,
	[HasLocalTax] [int] NULL ,
	[FileName] [nvarchar] (255) NULL ,
	[Downloaded] [int] NULL ,
	[ItemPrice] [nvarchar] (50) NULL ,
	[BackOrderedQty] [int] NULL ,
	[Category] [ntext] NULL ,
	[itemTotal] [nvarchar] (50) NULL ,
	[VendorName] [nvarchar] (75) NULL ,
	[Weight] [nvarchar] (20) NULL ,
	[VendorEmail] [nvarchar] (100) NULL ,
	[DownloadOneTime] [int] NULL ,
	[DownloadExpire] [varchar] (50) NULL ,
	[IsItemEbay] [bit] NOT NULL DEFAULT (0),
	[ProductType] [int] NOT NULL DEFAULT (0),
	[RecurringSubscriptionPrice] [nvarchar] (20) NOT NULL DEFAULT (0),
	[PaymentPeriod] [int] NOT NULL DEFAULT (-1),
	[Term] [int] NOT NULL DEFAULT (0),
	[BillingStartDate] [datetime] NULL DEFAULT('01/01/1990')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [OrderItemsAttributes] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderItemID] [int] NULL ,
	[AttributeName] [nvarchar] (255) NULL ,
	[AttributeType] [tinyint] NULL ,
	[AttributeDetailName] [nvarchar] (255) NULL ,
	[CustomAttribute] [nvarchar] (100) NULL ,
	[Price] [nvarchar] (10) NULL ,
	[PriceType] [tinyint] NULL ,
	[Weight] [nvarchar] (10) NULL ,
	[WeightType] [tinyint] NULL ,
	[AttributeID] [int] NULL ,
	[AttributeDetailID] [int] NULL ,
	[FileLocation] [varchar] (100) NULL ,
	[BundleOrderItemID] [int] NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [OrderPayment] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Type] [int] NULL ,
	[PONumber] [nvarchar] (100) NULL ,
	[CheckNumber] [nvarchar] (100) NULL ,
	[BankName] [nvarchar] (255) NULL ,
	[RoutingNumber] [nvarchar] (100) NULL ,
	[AccountNumber] [nvarchar] (100) NULL ,
	[CreditCardNumber] [ntext] NULL ,
	[CardType] [nvarchar] (100) NULL ,
	[SecurityCode] [nvarchar] (50) NULL ,
	[ExpireMonth] [nvarchar] (2) NULL ,
	[ExpireYear] [nvarchar] (4) NULL ,
	[OrderID] [int] NULL ,
	[StartMonth] [nvarchar] (2) NULL ,
	[StartYear] [nvarchar] (4) NULL ,
	[IssueNumber] [nvarchar] (50) NULL ,
	[Last4Digits] [char] (4) NULL ,
	[IsDirty] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [OrderTracking] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ShipToAddressID] [int] NULL ,
	[BackOrderFlag] [int] NULL ,
	[VendorName] [nvarchar] (75) NULL ,
	[TrackingNumber] [nvarchar] (50) NULL ,
	[TrackingMessage] [nvarchar] (255) NULL ,
	[SentDate] [nvarchar] (20) NULL 
) ON [PRIMARY]

CREATE TABLE [Orders] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CustomerID] [int] NULL ,
	[DateOrdered] [nvarchar] (50) NULL ,
	[SubTotal] [nvarchar] (20) NULL ,
	[ShippingTotal] [nvarchar] (20) NULL ,
	[HandlingTotal] [nvarchar] (20) NULL ,
	[CountryTax] [nvarchar] (20) NULL ,
	[StateTax] [nvarchar] (20) NULL ,
	[PayMethod] [nvarchar] (50) NULL ,
	[GiftCertificateTotal] [nvarchar] (50) NULL ,
	[DownloadDate] [nvarchar] (50) NULL ,
	[CustomerGroup] [int] NULL ,
	[DiscountsTotal] [nvarchar] (50) NULL ,
	[TotalBilled] [nvarchar] (50) NULL ,
	[AmountRemaining] [nvarchar] (50) NULL ,
	[OrderNumber] [numeric](10, 0) NULL ,
	[TotalAppliedDiscounts] [nvarchar] (50) NULL ,
	[LocalTaxTotal] [nvarchar] (50) NULL ,
	[Units] [nvarchar] (50) NULL ,
	[GrandTotal] [nvarchar] (255) NULL ,
	[PaymentsPending] [int] NULL ,
	[BOPaymentsPending] [int] NULL ,
	[Void] [int] NULL ,
	[Referrer] [int] NULL ,
	[IntegrationExported] [bit] NULL ,
	[IntegrationComment] [text] NULL ,
	[RMSStatus] [int] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE  INDEX [IX_CUSTOMERID] ON [Orders]([CustomerID]) ON [PRIMARY]

CREATE TABLE [OrdersBackOrderBilling] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderID] [int] NULL ,
	[ItemsQty] [int] NULL ,
	[BackOrderDue] [nvarchar] (50) NULL ,
	[TotalBilled] [nvarchar] (50) NULL 
) ON [PRIMARY]

CREATE TABLE [PayPalUsers] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CustomerID] [int] NOT NULL ,
	[PayPalEMail] [nvarchar] (255) NULL 
) ON [PRIMARY]

CREATE TABLE [PaymentMethods] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Type] [nvarchar] (30) NULL ,
	[Name] [nvarchar] (50) NULL ,
	[isActive] [tinyint] NULL 
) ON [PRIMARY]

CREATE TABLE [PaymentProcessors] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar] (100) NULL ,
	[MerchantID] [nvarchar] (500) NULL ,
	[UserID] [nvarchar] (500) NULL ,
	[Pass] [nvarchar] (500) NULL ,
	[DeclineMode] [int] NULL ,
	[AVSIsActive] [int] NULL ,
	[LiveServerPath] [nvarchar] (255) NULL ,
	[AuthMode] [int] NULL ,
	[IsEncrypt] [int] NULL ,
	[AVSFlags] [nvarchar] (100) NULL ,
	[TestMode] [int] NULL DEFAULT (0),
	[PayerAuthSel] [int] NULL ,
	[PayerAuthURL] [varchar] (255) NULL ,
	[service] [varchar] (50) NULL ,
	[ccpaclientid] [varchar] (50) NULL ,
	[ProcessorID] [varchar] (50) NOT NULL DEFAULT (''),
	[RecurringBillingIsActive] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [ProcessorResponse] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderID] [numeric](10, 0) NULL ,
	[CustTransNo] [nvarchar] (255) NULL ,
	[MerchantTransNo] [nvarchar] (255) NULL ,
	[AVSResult] [nvarchar] (255) NULL ,
	[ActionCode] [nvarchar] (255) NULL ,
	[RetrievalCode] [nvarchar] (255) NULL ,
	[AuthorizationNo] [nvarchar] (255) NULL ,
	[Success] [int] NULL ,
	[CVVResult] [nvarchar] (255) NULL ,
	[ErrorLocation] [nvarchar] (255) NULL ,
	[AuxMessage] [ntext] NULL ,
	[ErrorMessage] [ntext] NULL ,
	[FullResponse] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [ProductCategory] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NULL ,
	[CategoryID] [int] NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_PRODUCTID] ON [ProductCategory]([ProductID]) ON [PRIMARY]
CREATE  INDEX [IX_CATEGORYID] ON [ProductCategory]([CategoryID]) ON [PRIMARY]

CREATE TABLE [ProductDetail] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Type] [int] NULL ,
	[DisplayImage] [int] NULL ,
	[DisplayProductCode] [int] NULL ,
	[DisplayProductName] [int] NULL ,
	[DisplayCategory] [int] NULL ,
	[DisplayPriceSalePrice] [int] NULL ,
	[DisplayShortDescription] [int] NULL ,
	[DisplayLongDescription] [int] NULL ,
	[DisplayVendor] [int] NULL ,
	[DisplayManufacturer] [int] NULL ,
	[DisplayVolumePricing] [int] NULL ,
	[DisplayStockInfo] [int] NULL ,
	[DisplaySavedCartWishList] [int] NULL ,
	[DisplayEMailFriend] [int] NULL ,
	[DisplayLabels] [int] NULL ,
	[DisplayQty] [int] NULL ,
	[DisplayRecommendedProducts] [int] NULL ,
	[DisplayRecommendedImage] [int] NULL ,
	[DisplayRecommendedName] [int] NULL ,
	[DisplayRecommendedCode] [int] NULL ,
	[DisplayRecommendedShortDescription] [int] NULL ,
	[DisplayRecommendedPrice] [int] NULL ,
	[DefaultQty] [int] NULL ,
	[LinkImage] [int] NULL ,
	[LinkProductName] [int] NULL ,
	[LinkProductCode] [int] NULL ,
	[RecommendedTitle] [nvarchar] (255) NULL ,
	[ImageSize] [int] NULL ,
	[IsActive] [int] NULL ,
	[DisplaySavings] [int] NULL ,
	[AttributeDisplay] [tinyint] NULL ,
	[DisplayAddToCart] [int] NOT NULL DEFAULT (1),
	[RecommendedItemsPerRow] [int] NOT NULL DEFAULT (3)
) ON [PRIMARY]

CREATE TABLE [Products] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Code] [nvarchar] (50) NULL ,
	[ManufacturerId] [int] NULL ,
	[VendorId] [int] NULL ,
	[IsActive] [int] NULL ,
	[Name] [nvarchar] (255) NULL ,
	[NamePlural] [nvarchar] (255) NULL ,
	[ShortDescription] [nvarchar] (255) NULL ,
	[Description] [ntext] NULL ,
	[UpSellMessage] [ntext] NULL ,
	[ImageSmallPath] [nvarchar] (255) NULL ,
	[ImageLargePath] [nvarchar] (255) NULL ,
	[FileName] [nvarchar] (255) NULL ,
	[Cost] [nvarchar] (20) NULL ,
	[Price] [nvarchar] (20) NULL ,
	[IsOnSale] [int] NULL ,
	[SalePrice] [nvarchar] (20) NULL ,
	[IsShipable] [int] NULL ,
	[ShipPrice] [nvarchar] (20) NULL ,
	[Weight] [nvarchar] (20) NULL ,
	[Length] [nvarchar] (20) NULL ,
	[Width] [nvarchar] (20) NULL ,
	[Height] [nvarchar] (20) NULL ,
	[HasCountryTax] [int] NULL ,
	[HasStateTax] [int] NULL ,
	[HasLocalTax] [int] NULL ,
	[DateAdded] [nvarchar] (50) NULL ,
	[DateModified] [nvarchar] (50) NULL ,
	[Keywords] [ntext] NULL ,
	[DetailLink] [nvarchar] (255) NULL ,
	[Inventory_Tracked] [int] NULL ,
	[DropShip] [int] NULL ,
	[DownloadOneTime] [int] NULL ,
	[DownloadExpire] [nvarchar] (50) NULL ,
	[DealTimeIsActive] [int] NULL ,
	[MMIsActive] [int] NULL ,
	[ProductType] [int] NOT NULL DEFAULT (0),
	[RecurringSubscriptionPrice] [nvarchar] (20) NOT NULL DEFAULT (0),
	[PaymentPeriod] [int] NOT NULL DEFAULT (-1),
	[Term] [int] NOT NULL DEFAULT (0),
	[BillingDelay] [int] NOT NULL DEFAULT (0),
	[IsOnClearance] [int] NULL DEFAULT (0),
	[SaleType] [int] NULL DEFAULT (0),
	[BundleGroupID] [int] NULL DEFAULT (0),
	[BundleDisplayName] [nvarchar] (50) NULL ,
	[ComputePrice] [int] NULL DEFAULT (0),
	[PriceUp] [int] NULL DEFAULT (0),
	[PriceChangedAmount] [nvarchar] (20) NULL DEFAULT (0),
	[PriceChangedType] [int] NULL ,
	[SwatchesPerRow] [int] NULL ,
	[RelatedImage] [nvarchar] (50) NULL ,
	[CloseUpImage] [nvarchar] (255) NULL ,
	[CloseUpLinkText] [nvarchar] (255) NULL ,
	[ChangeOnClick] [int] NULL ,
	[ChangeOnMouseover] [int] NULL ,
	[ShowCloseUpLink] [int] NULL ,
	[LinkBigImage] [int] NULL ,
	[SwatchAllignment] [int] NULL ,
	[DescriptionAllignment] [int] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE  INDEX [IX_MANUFACTURERID] ON [Products]([ManufacturerId]) ON [PRIMARY]
CREATE  INDEX [IX_VENDORID] ON [Products]([VendorId]) ON [PRIMARY]
CREATE  INDEX [IX_CODE] ON [Products]([Code]) ON [PRIMARY]
CREATE  INDEX [IX_ISACTIVE] ON [Products]([IsActive]) ON [PRIMARY]
CREATE  INDEX [IX_NAME] ON [Products]([Name]) ON [PRIMARY]
CREATE  INDEX [IX_NAMEPLURAL] ON [Products]([NamePlural]) ON [PRIMARY]
CREATE  INDEX [IX_SHORTDESCRIPTION] ON [Products]([ShortDescription]) ON [PRIMARY]

CREATE TABLE [ProductsBundle] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NOT NULL ,
	[BundleID] [int] NOT NULL ,
	[Qty] [int] NOT NULL DEFAULT (1),
	[DisplayOrder] [int] NOT NULL DEFAULT (1)
) ON [PRIMARY]

CREATE TABLE [QBAccounts] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[AccountName] [varchar] (50) NULL ,
	[Description] [varchar] (50) NULL ,
	[Item] [varchar] (50) NULL 
) ON [PRIMARY]

CREATE TABLE [QBConversion] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[PageName] [varchar] (50) NULL ,
	[Sequence] [varchar] (10) NULL ,
	[WebID] [varchar] (50) NULL ,
	[QuickBooksID] [varchar] (50) NULL ,
	[QBAccount] [varchar] (50) NULL 
) ON [PRIMARY]

CREATE TABLE [QBSettings] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ToPrint] [int] NULL ,
	[OrderType] [int] NULL ,
	[Prefix] [varchar] (50) NULL ,
	[LastNumber] [varchar] (50) NULL ,
	[QBMemo] [text] NULL ,
	[ExtractedDate] [varchar] (20) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [QBTaxGroups] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Sequence] [varchar] (10) NULL ,
	[State] [varchar] (20) NULL ,
	[Taxable] [int] NULL ,
	[IsGroupTax] [int] NULL ,
	[GroupName] [varchar] (50) NULL ,
	[Rate] [varchar] (10) NULL 
) ON [PRIMARY]

CREATE TABLE [QBTaxRates] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[TaxName] [varchar] (50) NULL ,
	[TaxAgency] [varchar] (50) NULL ,
	[TaxRate] [varchar] (10) NULL ,
	[TaxAccount] [varchar] (50) NULL ,
	[TaxGroupID] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [QBTemplates] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[PageName] [varchar] (50) NULL ,
	[Sequence] [varchar] (50) NULL ,
	[Description] [varchar] (50) NULL ,
	[Active] [int] NULL ,
	[Skip] [int] NULL ,
	[ColumnVisible] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [RecurringTransactionResponse] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[OrderItemID] [int] NOT NULL ,
	[ProfileID] [nvarchar] (50) NOT NULL ,
	[RPRef] [nvarchar] (20) NOT NULL ,
	[Result] [int] NOT NULL ,
	[Message] [text] NOT NULL ,
	[TransactionDate] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [RelatedProducts] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ParentID] [int] NULL ,
	[RelatedID] [int] NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_PARENTID] ON [RelatedProducts]([ParentID]) ON [PRIMARY]
CREATE  INDEX [IX_RELATEDID] ON [RelatedProducts]([RelatedID]) ON [PRIMARY]

CREATE TABLE [RoleTasks] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[RoleId] [int] NOT NULL ,
	[TaskID] [int] NOT NULL ,
	[TaskName] [varchar] (100) NOT NULL 
) ON [PRIMARY]

CREATE TABLE [SavedCartAttributes] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[SavedItemID] [int] NULL ,
	[AttributeID] [int] NULL ,
	[AttributeDetailID] [int] NULL ,
	[CustonAttribute] [nvarchar] (100) NULL 
) ON [PRIMARY]

CREATE TABLE [SavedCartItems] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ParentId] [int] NULL ,
	[ProductID] [int] NULL ,
	[Quantity] [int] NULL ,
	[IsItemEbay] [bit] NULL 
) ON [PRIMARY]

CREATE TABLE [SavedCarts] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CustomerID] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [SearchFilters] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[AttributeName] [nvarchar] (200) NOT NULL ,
	[GlobalSelectorName] [nvarchar] (200) NOT NULL 
) ON [PRIMARY]

CREATE TABLE [SearchOptions] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[KeyWord] [tinyint] NULL ,
	[Category] [tinyint] NULL ,
	[Manufacturer] [tinyint] NULL ,
	[Vendor] [tinyint] NULL ,
	[PriceBetween] [tinyint] NULL ,
	[AddedOn] [tinyint] NULL ,
	[SaleOnly] [tinyint] NULL 
) ON [PRIMARY]

CREATE TABLE [SearchResults] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Type] [int] NULL ,
	[DisplayImage] [int] NULL ,
	[DisplayProductCode] [int] NULL ,
	[DisplayProductName] [int] NULL ,
	[DisplayPriceSalePrice] [int] NULL ,
	[DisplayShortDescription] [int] NULL ,
	[DisplayVendor] [int] NULL ,
	[DisplayManufacturer] [int] NULL ,
	[DisplayVolumePricing] [int] NULL ,
	[DisplayStockInfo] [int] NULL ,
	[DisplayAddToCart] [int] NULL ,
	[DisplaySavedCartWishList] [int] NULL ,
	[DisplayEMailFriend] [int] NULL ,
	[DisplayMoreInfo] [int] NULL ,
	[DisplayLabels] [int] NULL ,
	[ProductsPerRow] [int] NULL ,
	[NumOfRows] [int] NULL ,
	[Alignment] [int] NULL ,
	[LinkImage] [int] NULL ,
	[LinkProductCode] [int] NULL ,
	[LinkProductName] [int] NULL ,
	[DefaultQty] [int] NULL ,
	[IsActive] [int] NULL ,
	[DisplayQty] [int] NULL ,
	[AttributeDisplay] [smallint] NULL 
) ON [PRIMARY]

CREATE TABLE [SecurityLog] (
	[uid] [int] IDENTITY (0, 1) NOT NULL PRIMARY KEY,
	[UserName] [varchar] (100) NULL ,
	[AccessTime] [datetime] NULL 
) ON [PRIMARY]

CREATE TABLE [SelectCountry] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Abbreviation] [nvarchar] (3) NULL ,
	[Name] [nvarchar] (100) NULL ,
	[IsActive] [tinyint] NULL ,
	[UPSCountry] [tinyint] NULL 
) ON [PRIMARY]

CREATE TABLE [SelectISOCurrency] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ISOCurrency] [nvarchar] (50) NULL ,
	[LCID] [nvarchar] (50) NULL ,
	[Name] [nvarchar] (100) NULL ,
	[Code] [varchar] (4) NULL ,
	[LanguageCode] [varchar] (4) NULL 
) ON [PRIMARY]

CREATE TABLE [SelectLocal] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar] (100) NULL ,
	[IsActive] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [SelectState] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Abbreviation] [nvarchar] (3) NULL ,
	[Name] [nvarchar] (50) NULL ,
	[IsActive] [tinyint] NULL 
) ON [PRIMARY]

CREATE TABLE [Shipping] (
	[ID] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Method] [nvarchar] (50) NULL ,
	[Code] [nvarchar] (50) NULL ,
	[IsActive] [int] NULL ,
	[Edit] [int] NULL ,
	[Rates] [nvarchar] (10) NULL ,
	[CarrierID] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [ShippingCarriers] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar] (50) NULL ,
	[Code] [nvarchar] (10) NULL ,
	[Active] [int] NULL ,
	[UserName] [ntext] NULL ,
	[Pass] [ntext] NULL ,
	[AccessCode] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [ShoppingCart] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[SessionID] [nvarchar] (75) NULL ,
	[CouponCodes] [ntext] NULL ,
	[CreatedDate] [nvarchar] (50) NULL ,
	[GiftCertificates] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [ShoppingCartAttributes] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ShoppingCartItemID] [int] NULL ,
	[AttributeID] [int] NULL ,
	[AttributeDetailID] [int] NULL ,
	[CustomAttribute] [nvarchar] (100) NULL 
) ON [PRIMARY]

CREATE TABLE [ShoppingCartGiftWrap] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ShoppingItemID] [int] NULL ,
	[MessageTo] [nvarchar] (50) NULL ,
	[MessageFrom] [nvarchar] (50) NULL ,
	[Message] [nvarchar] (255) NULL 
) ON [PRIMARY]

CREATE TABLE [ShoppingCartItems] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CartID] [int] NULL ,
	[ProductID] [int] NULL ,
	[Quantity] [int] NULL ,
	[ReferalAddress] [nvarchar] (255) NULL ,
	[GiftWrapQty] [int] NULL ,
	[IsItemEbay] [bit] NULL ,
	[EbayPrice] [decimal](18, 0) NULL ,
	[BLOB] [image] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [Swatch] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NULL ,
	[Name] [nvarchar] (1000) NULL ,
	[LittleImage] [nvarchar] (255) NULL ,
	[LargeImage] [nvarchar] (255) NULL ,
	[ThumbnailImage] [nvarchar] (255) NULL ,
	[CloseUpImage] [nvarchar] (255) NULL ,
	[ShowDescription] [int] NULL ,
	[ShowSelectedDescription] [int] NULL ,
	[ShowImage] [int] NULL ,
	[SortOrder] [int] NULL ,
	[Description] [nvarchar] (255) NULL ,
	[AttributeNames] [nvarchar] (1000) NULL 
) ON [PRIMARY]

CREATE TABLE [TaxRates] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Type] [int] NULL ,
	[DestinationID] [int] NULL ,
	[Rate] [nvarchar] (50) NULL ,
	[IsActive] [int] NULL 
) ON [PRIMARY]

CREATE TABLE [TaxTypes] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[TaxType] [nvarchar] (25) NOT NULL ,
	[SFTaxType] [nvarchar] (25) NOT NULL 
) ON [PRIMARY]

CREATE TABLE [UserRoles] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name] [varchar] (100) NOT NULL ,
	[IsSuper] [bit] NOT NULL DEFAULT (0)
) ON [PRIMARY]

CREATE TABLE [ValueShipping] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[MinTotal] [nvarchar] (10) NULL ,
	[MaxTotal] [nvarchar] (10) NULL ,
	[Amount] [nvarchar] (20) NULL 
) ON [PRIMARY]

CREATE TABLE [Vendors] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[AddressID] [int] NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_ADDRESSID] ON [Vendors]([AddressID]) ON [PRIMARY]

CREATE TABLE [VolumePricing] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ProductID] [int] NULL ,
	[DollarOrPercent] [int] NULL ,
	[BreakLevel] [int] NULL ,
	[Amount] [nvarchar] (20) NULL 
) ON [PRIMARY]
CREATE  INDEX [IX_PRODUCTID] ON [VolumePricing]([ProductID]) ON [PRIMARY]

CREATE TABLE [Walkin] (
	[uid] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[SessionID] [nvarchar] (75) NULL ,
	[CustomerID] [int] NULL ,
	[IsSignedIn] [int] NULL ,
	[CreatedDate] [nvarchar] (50) NULL ,
	[OandaRate] [nvarchar] (50) NULL ,
	[OandaISO] [nvarchar] (50) NULL ,
	[AnonymousSignIn] [bit] NULL 
) ON [PRIMARY]
GO
/* ==================================================================================================== */
/*
END 	TABLES CREATION
BEGIN	FUNCTION REMOVAL
*/
/* ==================================================================================================== */
if exists (select * from dbo.sysobjects where id = object_id(N'[ufn_GetOrderItemSKU]') and xtype in (N'FN', N'IF', N'TF'))
drop function [ufn_GetOrderItemSKU]
GO
/* ==================================================================================================== */
/*
END	FUNCTION REMOVAL
BEGIN	FUNCTION CREATION
*/
/* ==================================================================================================== */
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE FUNCTION [ufn_GetOrderItemSKU](@OrderItemId int)
	RETURNS varchar(50)
AS
BEGIN
	DECLARE
		@attributeId int,
		@AttributeIds varchar(1000),
		@ItemCode varchar(50)
		
	SET @ItemCode=''
	SET @AttributeIds= ''
	DECLARE crs_Attributes CURSOR READ_ONLY FOR 
		SELECT AttributeDetailId FROM orderitemsattributes WHERE OrderItemId = @OrderItemID 
		order by AttributeDetailId asc
	
	OPEN crs_Attributes
	
	FETCH NEXT FROM crs_Attributes INTO @attributeId
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @AttributeIds = @AttributeIds + CONVERT(varchar,@attributeId) + ','
		FETCH NEXT FROM crs_Attributes INTO @attributeId
	END
	
	CLOSE crs_Attributes
	DEALLOCATE crs_Attributes
	
	IF LEN(@AttributeIds) > 0 
		SET @AttributeIds = SUBSTRING(@AttributeIds,0,LEN(@AttributeIds))
	
	SELECT @ItemCode  = SKU FROM Inventory WHERE AttributeDetailId =  @AttributeIds
	
	IF(@ItemCode IS NULL OR LEN(@ItemCode) <=0)
	BEGIN
		SELECT @ItemCode = ProductCode FROM OrderItems WHERE uid = @OrderItemId
	END
	RETURN(@ItemCode)
END	
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
/* ==================================================================================================== */
/*
END	FUNCTION CREATION
BEGIN	DATA POPULATION
*/
/* ==================================================================================================== */
INSERT INTO [AddProductStyle] ([DisplayProductCode], [DisplayProductName], [DisplayQuantity], [DisplayUpSellMessage]) VALUES (1, 1, 1, 1)

SET IDENTITY_INSERT [Addresses] ON
INSERT INTO [Addresses] ([uid], [Type], [Nickname], [FirstName], [Company], [City], [State], [Zip], [Country]) VALUES (1, 0, 'No Manufacturer', 'No Manufacturer', 'No Manufacturer', 'Lawrence', 'KS', '66044', 'US')
INSERT INTO [Addresses] ([uid], [Type], [Nickname], [FirstName], [Company], [City], [State], [Zip], [Country]) VALUES (2, 1, 'No Vendor', 'No Vendor', 'No Vendor', 'Lawrence', 'KS', '66044', 'US')
INSERT INTO [Addresses] ([uid], [Type], [Nickname], [Country]) VALUES (3, 7, 'Admin', 'US')
SET IDENTITY_INSERT [Addresses] OFF

INSERT INTO [Admin] ([PrimaryEmail], [SecondaryEmail], [EMailMethod], [EMailServer], [SSLPath], [TransMethod], [StoreName], [AddressID], [ShipType], [ShipType2], [PrmShipIsActive], [HandlingIsActive], [DeletePolicy], [DeleteSchedule], [ISOCurrency], [LCID], [ShipMin], [CODAmount], [SpcShipAmt], [OandaID], [ActivateOanda], [LivePersonActive], [SFID], [PhysPath], [BackOrderBilling], [TopMenuBarNav], [FooterNav], [LeftNav], [RightNav], [AllowMultipleCoupons], [AllowCouponWithDiscount], [TaxShipping], [TaxHandling], [AdditionalAddressHandling], [AllOrderHandling], [HandlingAmount], [AddProductStyle], [SiteURL], [MetaTag], [MetaDescription], [NextOrderNumber], [DatabaseType], [CCOnline], [ECheckOnline], [CVVIsActive], [ProductSort], [PremShipLabel], [AcceptCC], [MMPassword], [MMUserName], [SiteKeywords], [SiteDescription], [sfLogin], [sfPassword], [AllowMultiShip],[WebLicense],[SearchOnFields], [PromoMailServer])
VALUES ('merchant@store.com', 'merchant2@store.com', 'CDONTS', 'smtp.mailserver.com', 'http://localhost/storefront/ssl/', '21', 'StoreFront 7.0', 3, 1, 1, 1, 1, '2', 30, 'USD', '1033', '2', '10', '10', '', 0, 0, 'StoreFrontID', 'c:\inetpub\wwwroot\storefront\', 1, 1, 1, 1, 1, 0, 0, 0, 0, '0', 1, '20', 2, 'http://localhost/storefront/', NULL, NULL, 1001, 2, 0, 0, 0, 'Name', 'Premium Shipping', 1, NULL, NULL, NULL, NULL, NULL, NULL, 1,'69 159 139 247 59 253 137 38 201 207 84 123 242 56 225 142 9 212 178 27 214 8 202 233 236 227 68 44 7 104 142 36 142 235 253 86 137 247 168 1','Code,Name,NamePlural,ShortDescription,keywords', 'smtp.mailserver.com')

INSERT INTO [AffiliateSettings] ([Active], [terms], [Payout], [MinPayout], [PayOutRule], [CommissionType], [FlatFee]) VALUES (1, 'These are the Commission Terms.', '0.1', '3', 1, 0, '2')

INSERT INTO [Categories] ([IsActive], [ParentLevel], [ParentID], [Name]) VALUES (1, 0, 0, '')

INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (0, 'Look What I Found At [StoreName]', 'Dear [RecipientName], &vbcr; [SenderName] thought you might be interested in learning about the following item at this link: [ProductLink] &vbcr; Product: [ProductName] &vbcr; Description: [ProductDescription] &vbcr; Image Link: [ProductImage] &vbcr; &vbcr; [PersonalMessage] &vbcr; Visit [StoreURL] to view the full selection of products available at [StoreName]!', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (0, 'Look What I Found At [StoreName]', 'Dear [RecipientName]<BR>[SenderName] thought you might be interested in learning about the following item:<BR>[ProductLink]<BR>[ProductName]<BR>[ProductDescription]<BR>[ProductImage]<BR><BR>[PersonalMessage]<BR><BR><BR><HR>Visit [StoreURL] to view the full selection of products available at [StoreName] </FONT>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (1, 'Password Reminder From [StoreName]', 'Hello [RecipientFirstName] [RecipientLastName]:&vbcr; &vbcr; Here is the password you requested.  Use this with your email address, [RecipientEmailAddress], to log in at [StoreURL] for quick access to your order history and other stored information. &vbcr; &vbcr; Password: [Password]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (1, 'Password Reminder From [StoreName]', 'Hello [RecipientFirstName] [RecipientLastName]:<BR>Here is the password you requested. Use this with your email address, [RecipientEmailAddress], to log in at [StoreURL] for quick access to your order history and other stored information. </FONT><BR><BR></FONT><FONT face="Arial Black"><B>Password:</B> [Password] </FONT></FONT></P>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (2, '[SenderName]''s Wish List at [StoreName]', 'Dear [RecipientName], &vbcr; [SenderName] has chosen to send you a copy of the Wish List they''ve compiled at [StoreName]: &vbcr; & vbCr[WishList] &vbcr; &vbcr; [PersonalMessage] &vbcr; &vbcr; Visit [StoreURL] to view the full selection of products available at [StoreName]!', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (2, '[SenderName]''s Wish List at [StoreName]', 'Dear [RecipientName], <BR>[SenderName] has chosen to send you a copy of the Wish List they''ve compiled at [StoreName]: <P><U>[WishList] </U><BR><EM> [PersonalMessage] </EM><HR>Visit [StoreURL] to view the full selection of products available at [StoreName]! </FONT>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (3, '[WishlistComponents]', 'Product ID: [ProductID] &vbcr; Product: [ProductName] &vbcr; Description: [ProductDescription] &vbcr; Attributes: [ProductAttributes] &vbcr; Price: [Price] &vbcr; Sale Price: [SalePrice] &vbcr; Link: [ProductLink] &vbcr; ---------------------------------------------------------- ', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (3, '[WishlistComponents]', 'Product ID: [ProductID] <br>Product: [ProductName]<hr><br>Description: [ProductDescription]<br>Attributes: [ProductAttributes]<br>Price: [Price]<br> Sale Price: [SalePrice]<br>Link to product: [ProductLink]<br>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (4, '[StoreName] Order Confirmation', 'Dear [CustomerFirstName], &vbcr; &vbcr; Thanks for your order from [StoreName]. &vbcr; A brief summary of your order is below. &vbcr; Please feel free to contact us if you have any questions. &vbcr; ============ &vbcr; Order Summary &vbcr; ============ &vbcr; Order ID: [OrderID] &vbcr; &vbcr; Order Total: &vbcr; ---------------- &vbcr; [OrderTotal] &vbcr; &vbcr; Billing Information: &vbcr; ------------------------ &vbcr; [BillingInfo] &vbcr; &vbcr; Products and Shipping Information: &vbcr; ---------------------------------------------- &vbcr; [ProductsShippingInfo] &vbcr; &vbcr; Click below to track your order: &vbcr; [OrderDetailsLink] &vbcr; &vbcr; Thanks again, &vbcr; [StoreName]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (4, '[StoreName] Order Confirmation', 'Dear [CustomerFirstName], <P>Thanks for your order from [StoreName]. <BR>A brief summary of your order is below. <BR>Please feel free to contact us if you have any questions. <P><STRONG>Order Summary: </STRONG><BR>Order ID: [OrderID] </P><P><STRONG>Order Total: </STRONG><BR>[OrderTotal]</P><P><B>Billing Information: </B><BR>[BillingInfo] <P><B>Shipping Information:</B> <BR>[ProductsShippingInfo] <P>[OrderDetailsLink]Track your order. <P>Thanks again, <BR><B>[StoreName]</B> </FONT></P>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (5, '[StoreName] Vendor Order Notification', 'This e-mail has been sent to notify you that a customer purchased one of your products at [StoreName].  Details of the order follow: &vbcr; &vbcr; OrderID: [OrderID] &vbcr; Order Total: [OrderTotal] &vbcr; &vbcr; Billing Information: &vbcr; [BillingInfo] &vbcr; Detailed Shipping Information: &vbcr; [ProductsShippingInfo]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (5, '[StoreName] Vendor Order Notification', '<html><body bgcolor=#FFFFFF><font face=arial size=2>This e-mail has been sent to notify you that a customer purchased one of your products at <a href="[StoreURL]"> [StoreName]</a>.  Details of the order follow:<p><b>OrderID:</b> [OrderID]<br><b>Order Total:</b> [OrderTotal]<p><b>Billing Information: </b><br>[BillingInfo]<p><b>Detailed Shipping Information: </b><br>[ProductsShippingInfo]</font></body></html>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (6, '[StoreName] Merchant Order Notification', 'This e-mail has been sent to notify you that an order has been placed at [StoreName].  The order details are below.  &vbcr; &vbcr; Order Summary: &vbcr; Order ID: [OrderID] &vbcr; Order Total: [OrderTotal] &vbcr; &vbcr; Billing Information:  &vbcr; [BillingInfo] &vbcr; &vbcr; Shipping Information: &vbcr; &vbcr; [ProductsShippingInfo]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (6, '[StoreName] Merchant Order Notification', '<html><body bgcolor=#FFFFFF><font face=arial size=2>This e-mail has been sent to notify you that an order has been placed at <a href="[StoreURL]"> [StoreName]</a>.  The order details are below. <p>Order Summary:<br><b>Order ID:</b> [OrderID]<br><b>Order Total:</b> [OrderTotal]<p><b>Billing Information:</b><br>[BillingInfo]<p><b>Shipping Information:</b><br>[ProductsShippingInfo]</font></body></html>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (7, '[ConfirmProducts]', 'Product ID: [ProductID] &vbcr; Product Name: [ProductName] &vbcr; Attributes (if any): [ProductAttributes] &vbcr; Price: [ProductPrice] &vbcr; Quantity: [ProductQuantity]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (7, '[ConfirmProducts]', '<FONT face=Arial>Product ID: [ProductID] <BR>Product Name: [ProductName] <BR>Attributes (if any): [ProductAttributes] <BR>Price: [ProductPrice] <BR>Quantity: [ProductQuantity] </FONT>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (8, '[ConfirmBillingInfo]', 'Name: [BillingName] &vbcr; Company: [BillingCompany] &vbcr; Address1: [BillingAddress1] &vbcr; Address2: [BillingAddress2] &vbcr; City: [BillingCity] State: [BillingState] Zip: [BillingZip] &vbcr; Country: [BillingCountry] &vbcr; Phone: [BillingPhone] &vbcr; Fax: [BillingFax] &vbcr; E-Mail Address: [BillingEMail] &vbcr; Payment Method: [BillingPaymentMethod]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (8, '[ConfirmBillingInfo]', '<FONT face=Arial>Name: [BillingName] <BR>Company: [BillingCompany] <BR>Address1: [BillingAddress1] <BR>Address2: [BillingAddress2] <BR>City: [BillingCity] State: [BillingState] Zip: [BillingZip] <BR>Country: [BillingCountry] <BR>Phone: [BillingPhone] <BR>Fax: [BillingFax] <BR>E-Mail Address: [BillingEMail] <BR>Payment Method: [BillingPaymentMethod] </FONT>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (9, '[ConfirmShippingInfo]', 'Name: [ShippingName] &vbcr; Company: [ShippingCompany] &vbcr; Address1: [ShippingAddress1] &vbcr; Address2: [ShippingAddress2] &vbcr; City: [ShippingCity] State: [ShippingState] Zip: [ShippingZip] &vbcr; Country: [ShippingCountry] &vbcr; Phone: [ShippingPhone] &vbcr; Fax: [ShippingFax] &vbcr; E-Mail Address: [ShippingEMail] &vbcr; Shipping Method: [ShippingMethod] &vbcr; &vbcr; These products will be shipped to the above address: &vbcr; [ShippingProducts]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (9, '[ConfirmShippingInfo]', '<FONT face=Arial>Name: [ShippingName]<br>Company: [ShippingCompany]<br>Address1: [ShippingAddress1]<br>Address2: [ShippingAddress2]<br>City: [ShippingCity] State: [ShippingState] Zip: [ShippingZip]<br>Country: [ShippingCountry]<br>Phone: [ShippingPhone]<br>Fax: [ShippingFax]<br>E-Mail Address: [ShippingEMail]<br> Shipping Method: [ShippingMethod]<p>These products will be shipped to the above address:<br>[ShippingProducts]<hr></FONT>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (10, '[ConfirmOrderTotal]', 'Merchandise Total: [OrderMerchandiseTotal] &vbcr; Discounts: [OrderDiscounts] &vbcr; Sub-Total: [OrderSubtotal] &vbcr; Local Tax: [OrderLocalTax] &vbcr; State/Province Tax: [OrderStateProvinceTax] &vbcr; Country Tax: [OrderCountryTax] &vbcr; Shipping: [OrderShipping] &vbcr; Handling: [OrderHandling] &vbcr; Order Total: [OrderTotal] &vbcr; Gift Certificate: [OrderGiftCertificate] &vbcr; Grand Total: [OrderGrandTotal]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (10, '[ConfirmOrderTotal]', '<FONT face=Arial>Merchandise Total: [OrderMerchandiseTotal] <BR>Discounts: [OrderDiscounts] <BR>Sub-Total: [OrderSubtotal] </FONT><P><FONT face=Arial>Local Tax: [OrderLocalTax] <BR>State/Province Tax: [OrderStateProvinceTax] <BR>Country Tax: [OrderCountryTax] <BR>Shipping: [OrderShipping] <BR>Handling: [OrderHandling] </FONT><P><FONT face=Arial>Order Total: [OrderTotal] </FONT><P><FONT face=Arial>Gift Certificate: [OrderGiftCertificate] <BR>Grand Total: [OrderGrandTotal] </FONT></P>', 'HTML', 1)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (11, 'Low stock ([ProductInventoryCount]) of [ProductName] notice', 'This serves as notification that the product: [ProductID] [ProductName] is reaching low inventory at quantity [ProductInventoryCount] at [StoreName] : [StoreURL]. &vbcr; &vbcr; [StoreURL]', 'TEXT', 0)
INSERT INTO [EMailContent] ([Type], [Subject], [Body], [Format], [IsActive]) VALUES (11, 'Low stock ([ProductInventoryCount])  of [ProductName]', '<html><body><font face="arial" size="2">This serves as notification that the product: [ProductID] [ProductName] is reaching low inventory at quantity [ProductInventoryCount] at <a href="[StoreURL]">[StoreName]</a>.<br><br><B>[StoreName]</B></font></body></html>', 'HTML', 1)

SET IDENTITY_INSERT [HomePage] ON
INSERT INTO [HomePage] ([uid], [IsActive]) VALUES (1, 1)
INSERT INTO [HomePage] ([uid], [IsActive]) VALUES (2, 0)
INSERT INTO [HomePage] ([uid], [IsActive]) VALUES (3, 0)
SET IDENTITY_INSERT [HomePage] OFF

INSERT INTO [Integration] (LastExport, LastOrderIDExported) VALUES ('1/1/2000', 0)

SET IDENTITY_INSERT [Instructions] ON
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (1, 'CustProfileMain.aspx', 'Use the options below to view and edit your account information.  To change your registered password or email address, click Edit My Profile.  To view a list of previous orders placed under your account and to check on the status of pending orders, click View Order Status and History.  To view and modify the items in your Saved Order/Wish List, click Access My Saved Order/Wish List.  To view, delete, or add new addresses to your Address Book, click Manage Address Book.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (2, 'CustSignIn.aspx', 'Enter your email address and password below to gain access to your account information.  If you don''t remember your password, click Forgot your Password? to have the password emailed to you.  If this is your first time shopping here, you can create a new account by supplying the information requested under Create a New Account, then clicking Continue.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (3, 'CustEdit.aspx', 'Your profile consists of the name assigned to your account and the email address and password you use to gain access to the account.  To change this information, simply modify it in the form below.  When you are finished making changes, click the Save button to update your profile.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (4, 'OrderHistory.aspx', 'The order history for your account is displayed below.  To view more information about an order, click View.  Click Track to view the shipment''s current status.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (5, 'OrderDetail.aspx', 'The details of your order are shown below.  You can check the status of your order by clicking Track, or you can reorder the same items by clicking the Re-order button.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (6, 'ShoppingCart.aspx', 'Your current order is displayed below. To remove an item from the order, click Remove. To move an item to your Saved Cart to be purchased at a later time, click Save Cart. To change the quantity of an item, change the number displayed in the Quantity field and then click Update. If you have any Coupons you wish to use, enter them in the Coupon Code field and click Apply.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (7, 'SavedCart.aspx(WL)', 'Your personal Wish List is displayed below.  To remove an item from your Wish List, click Remove.  To move an item from the Wish List to your current order for immediate purchase, click Buy Now.  To change the quantity of an item in the Wish List, change the number displayed in the Quantity field and then click Update. You can also email your Wish List to friends and family by clicking E-Mail List.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (8, 'SavedCart.aspx', 'Your personal saved cart is displayed below.  To remove an item from your saved cart, click Remove.  To move an item from the saved cart to your current order for immediate purchase, click Buy Now.  To change the quantity of an item in the saved cart, change the number displayed in the Quantity field and then click Update.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (9, 'EMailWishList.aspx', 'To send your Wish List to a friend or family member, first enter the name of the recipient in the Recipient''s Name field, then enter the email address where the Wish List should be sent in the Recipient''s Email Address field.  Compose a suitable message in the Personal Message area and click Send.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (10, 'CustAddressBook.aspx', 'For faster checkout, use your personal Address Book to save addresses to which your orders may be shipped or billed.  To create a new Address Book entry, enter the information requested by the form below, then click Save.  To edit an entry, click the Edit button, make any changes to the entry, then click Save.  To delete an entry, click Delete.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (11, 'ShipSummary.aspx', 'Your current order is shown below.  Please review the contents of the order.  If changes to your order are necessary, click on the Checkout link and make any corrections.  When you are satisfied with the contents of your order, click Continue to proceed to our secure location and begin the checkout process.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (12, 'GiftWrap.aspx', 'We''ll wrap some or all of the items in your order and include a personalized message. For each item you wish to gift wrap, check the Gift Wrap option, then enter your name in the From field and the name of the recipient in the To field.  Enter a special greeting in the Message field.  When you are finished, click Continue.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (13, 'CustSignInCheckout.aspx', 'If you''ve placed an order here before, simply enter your e-mail address and password and Sign In. If this is your first time shopping with us, please supply the required information and click Continue. <br> For PayPal Users: This is different than your PayPal Account.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (14, 'Shipping.aspx', 'In the form below, enter the address that your order will be shipped to or select an address from your Address Book.  New addresses will be saved to your Address Book and you''ll be able to retrieve and reuse this information the next time you order.  When you''ve finished, click Continue to proceed to the next step of the checkout process.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (15, 'MultiShip.aspx', 'Select a shipping destination from your Address Book for each item in your order.  If no suitable entry exists in the Address Book, or if you need to make changes to an Address Book entry, click Manage Addresses.  If you have no entries in your Address Book, click Add Address to add an entry.  When you have chosen a shipping destination for each item, click Continue to proceed to the next step of the checkout process.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (16, 'Billing.aspx', 'This step of the checkout process and the next collect the information that will be used to bill your order.  In the form below, enter your billing address or select a suitable billing address from your Address Book, then click Continue. This address must match that associated with your payment method.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (17, 'Payment.aspx', 'This is the final step of the checkout process.  Review the charges shown below and, if you are satisfied, enter your payment information in the form provided.  When you are finished, click Complete Order.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (18, 'Confirm.aspx', 'Thank you for your order! Your order has been received and will be processed shortly.  Please use the View button to view your completed order summary.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (19, 'Search.aspx', 'You can use this page to search our inventory of products for items matching a certain description.  To initiate a search, enter your criteria below and then click Go.  You can also use our <a href=search.aspx?advanced=1>Advanced Search</a> for more detailed searches.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (20, 'Search.aspx?Advanced=1', 'You can use this page to perform a detailed search of our product inventory using a variety of criteria.  To initiate a search, enter your criteria in the form below and click Search.')
INSERT INTO [Instructions] ([uid], [PageName], [Instruction]) VALUES (21, 'Confirm.aspx(PF)', 'Thank you for your order! Print the order summary below and submit it by phone, mail or fax it to complete the order process.')
SET IDENTITY_INSERT [Instructions] OFF
UPDATE [Instructions] SET [Instruction] = 'Need HELP with this page?  <a href="help.aspx#' + CAST([uid] AS VARCHAR(2)) + '">Click here</a>' WHERE uid <> 18 AND uid <> 21

INSERT INTO [Labels] ([ProductCode], [ProductName], [Description], [Price], [VolumePrice], [Stock], [Category], [CategoryPlural], [Manufacturer], [ManufacturerPlural], [Vendor], [VendorPlural], [MoreInfo], [SalePrice], [NoPriceGroup], [ClearancePrice]) VALUES ('Product ID', 'Product Name', 'Description', 'Price', 'Volume Price', 'Status', 'Category', 'Categories', 'Manufacturer', 'Manufacturers', 'Vendor', 'Vendors', 'More Info', 'Sale Price', 'No Price Group', 'Clearance Price')

INSERT INTO [Manufacturers] ([AddressID], [Active]) VALUES (1, 1)

SET IDENTITY_INSERT [MerchantToolsMenuHeaders] ON
INSERT INTO [MerchantToolsMenuHeaders] ([uid], [DisplayName], [DisplayOrder]) VALUES (1, 'Store Management', 0)
INSERT INTO [MerchantToolsMenuHeaders] ([uid], [DisplayName], [DisplayOrder]) VALUES (2, 'Marketing & Promotions', 1)
INSERT INTO [MerchantToolsMenuHeaders] ([uid], [DisplayName], [DisplayOrder]) VALUES (3, 'Store Inventory', 2)
INSERT INTO [MerchantToolsMenuHeaders] ([uid], [DisplayName], [DisplayOrder]) VALUES (4, 'Store Settings', 3)
INSERT INTO [MerchantToolsMenuHeaders] ([uid], [DisplayName], [DisplayOrder]) VALUES (5, 'Store Design',4)
SET IDENTITY_INSERT [MerchantToolsMenuHeaders] OFF

INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Sales Reports', 'StoreReports.aspx', 0, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Orders', 'orderfulfillment.aspx', 1, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Affiliates', 'affiliatepayments.aspx', 2, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Customers', 'customers.aspx', 3, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Price Groups', 'managepricegroups.aspx', 4, 1)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Gift Certificates', 'managegiftcertificates.aspx', 5, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Manage Roles','ManageUserRoles.aspx', 6, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Manage Administrators','ManageAdministrators.aspx', 7, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (1, 'Manage CSR Options', 'employees.aspx', 255, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (2, 'Storewide Discounts', 'storediscounts.aspx', 0, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (2, 'Coupons', 'storecoupons.aspx', 1, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (2, 'Promotional Mail', 'PromotionalMail.aspx', 2, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (2, 'Search Engines', 'SearchEngineSubmission.aspx', 3, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (2, 'Marketplaces', 'marketplaces.aspx', 4, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Import Products', 'productimport.aspx', 0, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Products', 'manageproducts.aspx', 1, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Attributes', 'attributestemplate.aspx', 2, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Categories', 'managecategories.aspx', 3, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Manufacturers', 'managemanufacturers.aspx', 4, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Vendors', 'managevendors.aspx', 5, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Merchant Bundles', 'ManageProducts.aspx?ProdType=2', 6, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Customer Defined Bundles', 'ManageProducts.aspx?ProdType=4', 7, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (3, 'Search Result Filters', 'SearchFilters.aspx', 8, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'General', 'general.aspx', 0, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'Online Chat', 'onlinechat.aspx', 1, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'E-Mail', 'ManageEmail.aspx', 2, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'Shipping', 'shippinghandling.aspx', 3, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'Payments', 'paymentmethods.aspx',4 , 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'Localization', 'geography.aspx', 5, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'Tax', 'tax.aspx', 6, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (4, 'StoreFront Connector','wsmanagement.aspx',7, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (5, 'Layout Templates', 'CatalogGeneral.aspx', 2, 0)
INSERT INTO [MerchantToolsMenuDetails] ([HeaderID], [DisplayName], [Link], [DisplayOrder], [Version]) VALUES (5, 'Themes', 'ManageThemes.aspx', 3, 0)

INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'TooManyAddresses', 'This address cannot be saved. Your address book can only store four addresses.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'NickNameInUse', 'An address has already been stored with this name. Please select another name to save this address as.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'BlankNickName', 'Please enter a name to save this address as.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'BlankFirstName', 'Please enter a first name for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'BlankAddress1', 'Please enter an address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'BlankCity', 'Please enter a city for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'BlankZip', 'Please enter a postal code for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'BlankState', 'Please enter a state/province for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'NickNameInUse', 'An address has already been stored with this name. Please select another name to save this address as.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'BlankNickName', 'Please enter a name to save this address as.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'BlankFirstName', 'Please enter a first name for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'BlankAddress1', 'Please enter an address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'BlankCity', 'Please enter a city for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'BlankZip', 'Please enter a postal code for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'BlankState', 'Please enter a state/province for the address you are trying to save.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'SigningIn', 'BlankEMailAddress', 'Please enter an e-mail address to login to your account.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'SigningIn', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'SigningIn', 'IncorrectSignIn', 'Sorry! Incorrect login. Please try again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'CreateAccount', 'BlankFirstName', 'Please enter a first name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'CreateAccount', 'BlankLastName', 'Please enter a last name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'CreateAccount', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'CreateAccount', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'CreateAccount', 'BlankConfirmPassword', 'Please confirm your password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'CreateAccount', 'PasswordConfirmEqual', 'Your password and the one you confirmed do not match. Please reenter and confirm your password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'CreateAccount', 'DuplicateCustomer', 'An account matching your e-mail address already exists. Please enter your password to login to this account. To have your password mailed to you, please select the Forgot Password link.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustForgotPassword.aspx', 'EMailPassword', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustForgotPassword.aspx', 'EMailPassword', 'EMailNotFound', 'Sorry! No account matching this e-mail address was found. Please try again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'CancelUpdate', 'CancelUpdate', 'Address update has been cancelled.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'UpdateAddress', 'Success', 'Address has been successfully updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'BlankSite', 'Please enter a Web Site.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcustomer.aspx', 'Error', 'DuplicateCustomer', 'There is already a customer with this e-mail address and password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcustomer.aspx', 'AddCustomer', 'Success', 'The customer has been successfully added.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcustomer.aspx', 'EditCustomer', 'Success', 'The customer information has been successfully updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addgiftcertificates', 'Error', 'BlankAmount', 'The amount cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addgiftcertificates.ascx', 'Error', 'Duplicate', 'This Gift Certificate Code is already in use')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addgiftcertificates', 'Error', 'BlankCode', 'The Gift Certificate Code cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editgiftcertificates', 'Error', 'BlankAmount', 'The amount cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editgiftcertificates', 'Error', 'BlankCode', 'The Gift Certificate Code cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editdiscount', 'Error', 'BlankDiscription', 'Description cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editdiscount', 'Error', 'BlankAmount', 'Amount cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editdiscount', 'Error', 'BlankApplyToID', 'Must select an ID')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('adddiscount', 'Error', 'BlankDiscription', 'Description cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('adddiscount', 'Error', 'BlankAmount', 'Amount cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('adddiscount', 'Error', 'BlankApplyToID', 'Must select an ID')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('freeshipping', 'Error', 'BlankOrderAmount', 'Amount cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editdiscount', 'Error', 'BlanKDate', 'Date cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('adddiscount', 'Error', 'BlanKDate', 'Date cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addgiftcertificates', 'Error', 'BlanKDate', 'Date cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editgiftcertificates', 'Error', 'BlanKDate', 'Date cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('freeshipping', 'Error', 'BlanKDate', 'Date cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('storediscounts.aspx', 'PageTitle', 'Title', 'Store-Wide Discounts')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('emailafriend.aspx', 'PageTitle', 'Title', 'E-Mail A Friend')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('selectlistpage.aspx', 'PageTitle', 'Title', 'Select List')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlankDiscription', 'Description cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlankAmount', 'Amount cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlankApplyToID', 'Must select an ID')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlanKDate', 'Date cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcoupon', 'Error', 'BlankDiscription', 'Description cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcoupon', 'Error', 'BlankAmount', 'Amount cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcoupon', 'Error', 'BlankApplyToID', 'Must select an ID')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcoupon', 'Error', 'BlanKDate', 'Date cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editmanufacturer.aspx', 'edit', 'UpdateError', 'Unable to update manufacturer data.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editmanufacturer.aspx', 'edit', 'Success', 'Manufacturer information has been saved.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editmanufacturer.aspx', 'edit', 'BlankName', 'Name cannot be blank')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addmanufacturer.aspx', 'add', 'Success', 'Manufacturer has been added.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editvendor.aspx', 'edit', 'BlankName', 'Name cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editvendor.aspx', 'edit', 'BlankCity', 'City cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editvendor.aspx', 'edit', 'BlankEmail', 'E-mail cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editvendor.aspx', 'edit', 'Success', 'Vendor information has been updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editvendor.aspx', 'edit', 'UpdateError', 'Unable to update vendor information.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addvendor.aspx', 'add', 'BlankName', 'Name cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addvendor.aspx', 'add', 'BlankCity', 'City cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addvendor.aspx', 'add', 'BlankEmail', 'E-mail cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addvendor.aspx', 'add', 'Success', 'Vendor information has been updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affiliateaccount.aspx', 'PageTitle', 'Title', 'Affiliates Pages')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'Error', 'NegativeQty', 'Order quantity must be a positive number. Please reneter the number of items you want to order.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'SubHeading', 'Title', 'My Wish List')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'PageTitle', 'Title', 'My Wish List')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'NoItems', 'NoItems', 'There are no items currently in your wish list.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShoppingCart.aspx', 'NoItems', 'NoItems', 'There are no items currently in your cart.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShoppingCart.aspx', 'Coupons', 'Remove', 'The promotional code has been removed.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShoppingCart.aspx', 'Error', 'NegativeQty', 'Order quantity must be a positive number. Please reneter the number of items you want to order.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'PageTitle', 'Title', 'Billing Information')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Confirm.aspx', 'PageTitle', 'Title', 'Confirm Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignInCheckout.aspx', 'PageTitle', 'Title', 'Customer Sign In Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('MultiShip.aspx', 'PageTitle', 'Title', 'Ship To Information')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'PageTitle', 'Title', 'Payment Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'PageTitle', 'Title', 'Ship To Information')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShipSummary.aspx', 'PageTitle', 'Title', 'Shipment Summary')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'PageTitle', 'Title', 'Customer Address Book')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'PageTitle', 'Title', 'Customer Edit Profile Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustForgotPassword.aspx', 'PageTitle', 'Title', 'Forgot Password Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustProfileMain.aspx', 'PageTitle', 'Title', 'My Profile Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignIn.aspx', 'PageTitle', 'Title', 'Customer Sign In Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('default.aspx', 'PageTitle', 'Title', 'Home Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Detail.aspx', 'PageTitle', 'Title', 'Detail Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('GiftWrap.aspx', 'PageTitle', 'Title', 'GiftWrap Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('OrderDetail.aspx', 'PageTitle', 'Title', 'Order Detail Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('OrderHistory.aspx', 'PageTitle', 'Title', 'Order History Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('OrderTracking.aspx', 'PageTitle', 'Title', 'Order Tracking Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Search.aspx', 'PageTitle', 'Title', 'Search Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SearchResult.aspx', 'PageTitle', 'Title', 'Search Result Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShoppingCart.aspx', 'PageTitle', 'Title', 'Checkout Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShoppingCart.aspx', 'MultiShip', 'CheckLabel', 'Ship To Multiple Addresses (Add [AdditionalHandling] Handling Per Additional Address)')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'ErrorMessage', 'ProductDeleted', 'Your cart has been updated. Some items may have been removed or changed based on updates to the [StoreName] product inventory.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'Message', 'Update', 'Your wish list has been updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'Message', 'BuyNow', 'The item you selected has been moved to your shopping cart.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'Message', 'Remove', 'The item you selected has been removed from your wish list')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx(WL)', 'Message', 'Add', 'The item you selected has been moved to your wish list.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AddProduct', 'AddToCart', 'Add', '<b>Thanks!<b><br>[Quantity] [ProductName] [has] been added to your order.<br><br>[UpSellMessage]')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustForgotPassword.aspx', 'PasswordSent', 'Sent', 'Your password has been sent to [RecipientEmailAddress].')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailAFriend.aspx', 'ErrorMessage', 'NoRecipientName', 'Please enter a recipient name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailAFriend.aspx', 'ErrorMessage', 'NoRecipientEmail', 'Please enter a recipient e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailAFriend.aspx', 'ErrorMessage', 'NoSenderName', 'Please enter a sender name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailAFriend.aspx', 'ErrorMessage', 'NoSenderEmail', 'Please enter a sender e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBoolk.aspx', 'DeleteAddress', 'DeleteAddress', 'The address you selected has been deleted.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Download', 'DownloadLabel', 'Ready', 'Download Now')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Download', 'DownloadLabel', 'Done', 'Download Retrieved')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Download', 'DownloadLabel', 'NotReady', 'Not Available')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EMailWishList.aspx', 'ErrorMessage', 'NoRecipientName', 'Enter a recipient name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EMailWishList.aspx', 'ErrorMessage', 'NoRecipientEmail', 'Enter a recipient e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EMailWishList.aspx', 'ErrorMessage', 'NoSenderName', 'Please enter a sender name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EMailWishList.aspx', 'ErrorMessage', 'NoSenderEmail', 'Please enter a sender e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailWishList.aspx', 'PageTitle', 'Title', 'E-Mail Wish List')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailWishList.aspx', 'ErrorMessage', 'NoItemInWishList', 'There are no items in the wish list to send. There must be at least one item in your wish list to e-mail it.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailWishList.aspx', 'WishlistSent', 'Sent', 'Your wish list has been sent to [RecipientName] at [RecipientEmailAddress].')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustChangeEmail.aspx', 'PageTitle', 'Title', 'Change Email Settings')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustChangeEmail.aspx', 'ErrorMessage', 'NoEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustChangeEmail.aspx', 'ErrorMessage', 'NoSuchEMailAddressOnFile', 'The e-mail address you entered is not associated with an account at [StoreName].')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustUnsubscribe.aspx', 'PageTitle', 'Title', 'Your mail preferences have been updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustUnsubscribe.aspx', 'ChangeEmailAction', 'Remove', '[EmailAddress] has been removed from the [StoreName] mailing list.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustUnsubscribe.aspx', 'ChangeEmailAction', 'NoAction', 'You will continue to receive notifications from [StoreName].')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustUnsubscribe.aspx', 'ChangeEmailAction', 'ChangeEmail', 'Your e-mail address has been changed. Future notifications from [StoreName] will be sent to [EmailAddress].')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShipSummary.aspx', 'Error', 'InvalidCarrier', 'Please select a shipping carrier.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'BlankTo', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'BlankFrom', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'BlankSubject', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'BlankBody', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'BlankMethod', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'BlankServer', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'InvalidMethod', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CEMailBase', 'Error', 'InvalidFormat', 'Unable to send e-mail.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmailAFriend.aspx', 'EmailAFriend', 'Sent', 'You have sent [RecipientEmailAddress] a message about [ProductName].')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('BackOrderMsg', 'Notify', 'none', '<b>Backordered Items will be Billed When Shipped</b> <br> Only *amtbilled* will be billed now. The remaining <br> *amtdue* will be billed when the backordered item(s) <br> ship.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('DrillDownMsg', 'msg', 'none', 'Refine Results')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('EmptyRefineMsg', 'msg', 'none', 'No Products Found in *CatName*.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Managementdefault.aspx', 'PageTitle', 'Title', '- Merchant Tools')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Oanda.aspx', 'PageTitle', 'Title', 'Currency Conversion')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('GiftCertificates', 'Error', 'AlreadyUsed', 'The Gift Certificate you entered has already been applied.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('GiftCertificates', 'Error', 'NotFound', 'The Gift Certificate you entered is not valid.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('GiftCertificates', 'Error', 'Expire', 'The Gift Certificate you entered has expired.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('GiftCertificates', 'Error', 'Used', 'The Gift Certificate you entered has already been used.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SalesDiscount', 'Error', 'AlreadyUsed', 'The promotional code you entered has already been used.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SalesDiscount', 'Error', 'NotFound', 'The promotional code you entered is not valid.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SalesDiscount', 'Error', 'Expire', 'The promotional code you entered has expired.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SalesDiscount', 'Error', 'NotApply', 'The promotional code you entered does not apply to items in your cart.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SalesDiscount', 'Error', 'MoreThanOne', 'Only one promotional code can be used per order.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SalesDiscount', 'Error', 'StoreSale', 'This promotional code is not valid at this time.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Search.aspx', 'Error', 'LargeQty', 'The items you ordered were not added to your cart. The quantity you entered exceeds the maximum quantity allowed.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'InvalidCard', 'The Credit Card Number is not valid.  Please check your information and try again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'CardExpired', 'The Credit Card is expired.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'NoAuth', 'The transaction could not be authorized.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'MissingField', 'Please fill in all fields.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'System', 'There was a system error.  Please re-submit your information.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'CheckRefused', 'Your check was refused by the payment processor.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'CardRefused', 'Your Credit Card was refused by the payment processor.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'Call', 'Call.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'General', 'There was an error processing your order.  Please try your order again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('managegiftcertificates.aspx', 'PageTitle', 'Title', 'Gift Certificates')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('OrderTracking.aspx', 'Notify', 'DropShip', 'Notice: Some items are being sent directly from vendor, and there is no tracking information on them.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustAddressBook.aspx', 'AddAddress', 'Success', 'Address has been successfully added.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'EditProfile', 'BlankFirstName', 'Please enter a first name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'EditProfile', 'BlankLastName', 'Please enter a last name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'EditProfile', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'EditProfile', 'OldPasswordNotMatch', 'The old password you entered does not match the one on file for this account. Please try again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'EditProfile', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'EditProfile', 'PasswordsNotMatch', 'Your password and the one you confirmed do not match. Please reenter and confirm your password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustEdit.aspx', 'EditProfile', 'Success', 'Your account has been updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankSaveAs', 'Please enter a name to save this address as.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankFirstName', 'Please enter a first name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankAddress1', 'Please enter an address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankCity', 'Please enter a city.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankZip', 'Please enter a postal code.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankPhone', 'Please enter a phone number.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Billing.aspx', 'EditBilling', 'PasswordsNotMatch', 'Your password and the one you confirmed do not match. Please reenter and confirm your password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignInCheckout.aspx', 'SigningIn', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignInCheckout.aspx', 'SigningIn', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CustSignInCheckout.aspx', 'SigningIn', 'IncorrectSignIn', 'Sorry! Incorrect login. Please try again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('MultiShip.aspx', 'Continue', 'NotAllShipped', 'Not all of the products in your order have been assigned to a shipping address. Please select an address for each item.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankSaveAs', 'Please enter a name to save this address as.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankFirstName', 'Please enter a first name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankAddress1', 'Please enter an address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankCity', 'Please enter a city.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankZip', 'Please enter a postal code.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankPhone', 'Please enter a phone number.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addvendor.aspx', 'add', 'SaveError', 'Unable to save vendor information.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'Error', 'NoProcessor', 'Processor not found. Please contact the merchant.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'SigningIn', 'BlankEMailAddress', 'Please enter an e-mail address to login to your account.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'SigningIn', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'SigningIn', 'IncorrectSignIn', 'Sorry! Incorrect login. Please try again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'BlankFirstName', 'Please enter a first name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'BlankLastName', 'Please enter a last name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'BlankConfirmPassword', 'Please confirm your password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'PasswordConfirmEqual', 'Your password and the one you confirmed do not match. Please reenter and confirm your password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'CreateAccount', 'DuplicateCustomer', 'An account matching your e-mail address already exists. Please enter your password to login to this account. To have your password mailed to you, please select the Forgot Password link.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('affsignIn.aspx', 'PageTitle', 'Title', 'Customer Sign In Page')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankSaveAs', 'Please enter a name to save this address as.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankFirstName', 'Please enter a first name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankAddress1', 'Please enter an address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankCity', 'Please enter a city.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankZip', 'Please enter a postal code.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankPhone', 'Please enter a phone number.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankEMailAddress', 'Please enter an e-mail address.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'BlankPassword', 'Please enter a password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'EditAff', 'PasswordsNotMatch', 'Your password and the one you confirmed do not match. Please reenter and confirm your password.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AffRegister.aspx', 'CreateAccount', 'BlankSite', 'Please enter a Web Site.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ProductSelect.aspx', 'ViewProductSales', 'NoSelection', 'Please make a selection.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShipSummary.aspx', 'PremiumShipping', 'ApplyPremiumShipping', '[PremiumShippingName] (add [Amount])')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcoupon', 'Error', 'EarlyDate', 'Please enter a date occuring after today''s date')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcoupon', 'Error', 'InvalidDate', 'Please enter a valid date.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Shipping.aspx', 'EditShipping', 'BlankLastName', 'Please enter a last name.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'Message', 'Remove', 'The item you selected has been removed from your saved cart.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'Error', 'NegativeQty', 'Order quantity must be a positive number. Please reneter the number of items you want to order.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'Message', 'BuyNow', 'The item you selected has been moved to your shopping cart.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'Message', 'Add', 'The item you selected has been moved to your saved cart.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'SubHeading', 'Title', 'My Saved Cart')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'NoItems', 'NoItems', 'There are no items currently in your saved cart.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'ErrorMessage', 'ProductDeleted', 'Your cart has been updated. Some items may have been removed or changed based on updates to the [StoreName] product inventory.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'Message', 'Update', 'Your saved cart has been updated.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SavedCart.aspx', 'PageTitle', 'Title', 'My Saved Cart')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('LTL', 'Error', 'EmailBlank', 'LTL email address cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('LTL', 'Error', 'PasswordBlank', 'LTL password cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('LTL', 'Error', 'WrongCurrency', 'Currency must be USD.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CP', 'Error', 'CPIDBlank', 'CanadaPost ID cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CP', 'Error', 'WrongCurrency', 'Currency must be CAD.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('CP', 'Error', 'HTTPFailed', 'Http: Connection Failed')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('UPS', 'Error', 'UserNameBlank', 'UPS UserName cannot be blank.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('UPS', 'Error', 'WrongCurrency', 'Store currency must match currency of origin country.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('UPS', 'Error', 'OverSize', 'Product too large: Weight must not exceed 150lbs.  Girth must not exceed 130in.  Length, Width, and Height must not exceed 108in.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('UPS', 'Error', 'HTTPFailed', 'Http: Connection Failed')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('UPS', 'Error', 'NoneAvailableForAll', 'There were no shipping methods available for all products in shipment.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('USPS', 'Error', 'WrongCurrency', 'Currency must be USD.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('USPS', 'Error', 'WrongOrigin', 'Origin country must be US.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('USPS', 'Error', 'OverSize', 'Product too large.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('USPS', 'Error', 'HTTPFailed', 'Http: Connection Failed')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('FEDEX', 'Error', 'HTTPFailed', 'Http: Connection Failed')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Tracking', 'Notify', 'NoTrackingInfo', 'No tracking information available for this shipment.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('addcategory', 'Error', 'Duplicate', 'A root category with this name already exists.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlankProductID', 'Must select a Product.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlankCategoryID', 'Must select a Category.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlankVendorID', 'Must select a Vendor.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('editcoupon', 'Error', 'BlankManufacturerID', 'Must select a Manufacturer.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '2', 'A required field is missing.  Please enter data in all required fields and submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '3', 'A required field was given an invalid value in the request.  Please re-enter a valid value in the required fields and try to submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '4', 'The transaction you attempted is not allowed.  Please contact the site administrator to report the issue.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '5', 'The transaction server could not process the transaction.  Please submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '6', 'The transaction you attempted could not be performed.  Please contact the site administrator and report the issue.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '7', 'The software version is out of date.  Please contact the site administrator and report the issue.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '8', 'The required response files from the payment handler is invalid.  Please submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '9', 'A required response file from the payment handler is invalid.  Please submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '10', 'The transaction you attempted could not be performed.  Please contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '100', 'There is a problem authorizing the credit card provided.  Please ensure that a valid credit card has been provided and submit the order again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '12', 'The acquirer gateway could not process the transaction.  Please submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '101', 'The acquirer gateway could not process the transaction.  Please submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '13', 'The payment engine could not process the transaction.  Please submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ParaData', 'Error', '102', 'The payment engine could not process the transaction.  Please submit the order again.  If the problem persists, contact the site administrator.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Help.aspx', 'PageTitle', 'Title', 'Help')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShoppingCart.aspx', 'OrderError', 'OrderError', 'Not all items could be ordered at this time, please review your order and try again.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AddToCart', 'InventoryCheck', 'InventoryCheck', 'The item cannot be ordered at this time, please try again later')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('Payment.aspx', 'InventoryCheck', 'InventoryCheck', 'The item(s) cannot be ordered at this time, please try again later')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ShoppingCart.aspx', 'Error', 'PayPalError', 'An Error occurred while processing your order with PayPal. Please use the default site CheckOut process to complete your order or you may contact the store administrator for assistance.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('ClearCommerce','Error','TransactionFailed', 'The transaction failed. Please try again or contact store merchant for more information.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('SearchResult.aspx','Suggestion','NoResult', 'We are sorry, but your search did not yield any results. <a href=search.aspx>Click here</a> to search again. If you think your search may have been too broad, you may wish to try using more descriptive criteria (if you did not specify keywords, try doing so). If you think your search may have been too narrow, try using looser criteria (for example, try performing an "All Words" or "Any Words" search)')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AttributeMessage','LastAttributeValue','InvalidOrOutOfStock', 'The selected value for [AttributeName] is unavailable with the other selected values.')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AttributeMessage','Combination','Invalid', '(Unavailable)')
INSERT INTO [Messages] ([Page], [Action], [Condition], [Message]) VALUES ('AttributeMessage','StockStatus','OutOfStock', '(Out of Stock)')

INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('eCheck', NULL, 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('COD', NULL, 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('PO', NULL, 0)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('PhoneFax', 'Recorded', 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('PhoneFax', 'Non-Recorded', 0)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('PayPal', 'PayPal', 0)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('Credit Card', 'American Express', 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('Credit Card', 'Visa', 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('Credit Card', 'MasterCard', 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('Credit Card', 'Discover', 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('Credit Card', 'Diners Club', 1)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('Credit Card', 'Switch', 0)
INSERT INTO [PaymentMethods] ([Type], [Name], [isActive]) VALUES ('Credit Card', 'Solo', 0)

SET IDENTITY_INSERT [PaymentProcessors] ON
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (1, 'CyberSource', NULL, NULL, NULL, 1, 1, '0', 1, 'A, N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (2, 'BankOfAmerica', NULL, NULL, NULL, 1, 0, 'https://cart.bamart.com/soap/listener.mart', 1, NULL, 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (3, 'IONGate', NULL, NULL, NULL, 1, 1, 'https://secure.iongate.com/iongate.asp', 1, 'A E G N R S U W X Y Z', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (4, 'PayPal', NULL, NULL, NULL, 0, 0, 'https://www.paypal.com/cgi-bin/webscr', 1, NULL, 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (5, 'WorldPay', NULL, NULL, NULL, 0, 0, 'https://select.worldpay.com/wcc/purchase', 1, NULL, 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (6, 'Barclay', NULL, NULL, NULL, 0, 0, 'https://secure2.epdq.co.uk/cgi-bin/CcxBarclaysEpdqEncTool.e|https://secure2.epdq.co.uk/cgi-bin/CcxBarclaysEpdq.e', 1, NULL, 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (7, 'LinkPoint', NULL, NULL, NULL, 0, 0, 'secure.linkpt.net', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (8, 'PsiGate', NULL, NULL, NULL, 0, 0, 'secure.psigate.com', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (9, 'SecurePay', NULL, NULL, NULL, 0, 0, 'https://www.securepay.com/secure1/index.asp', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (10, 'Terra Payments', NULL, NULL, NULL, 0, 0, NULL, 0, 'N', 0,0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (11, 'VeriSign', NULL, NULL, NULL, 1, 0, 'payflow.verisign.com', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (12, 'AuthorizeNet', NULL, NULL, NULL, 0, 0, 'https://secure.authorize.net/gateway/transact.dll', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (13, 'SecureSource', NULL, NULL, NULL, 0, 0, 'https://secure.authorize.net/gateway/transact.dll', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (14, 'PlanetPayment', NULL, NULL, NULL, 0, 0, 'https://secure.authorize.net/gateway/transact.dll', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (15, 'QuickCommerce', NULL, NULL, NULL, 0, 0, 'https://secure.authorize.net/gateway/transact.dll', 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (16, 'Paradata - SF Payments', NULL, NULL, NULL, 0, 0, NULL, 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (17, 'Paradata', NULL, NULL, NULL, 0, 0, NULL, 0, 'N', 0, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (18, 'Orbital', NULL, NULL, NULL, 0, 0, 'https://epayhip.paymentech.net/authorize', 0, 'N', 1, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (19, 'Clear Commerce', NULL, NULL, NULL, 0, 0, 'https://secure4x.clearcommerce.com', 0, 'N', 1, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (20, 'PayFuse', NULL, NULL, NULL, 0, 0, 'https://xmlic.payfuse.com', 0, 'N', 1, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (21, 'StoreFront Payments Gateway', NULL, NULL, NULL, 0, 0, 'https://secure4x.clearcommerce.com', 0, 'N', 1, 0)
INSERT INTO [PaymentProcessors] ([uid], [Name], [MerchantID], [UserID], [Pass], [DeclineMode], [AVSIsActive], [LiveServerPath], [AuthMode], [AVSFlags], [IsEncrypt], [TestMode]) VALUES (22, 'VeriSignPayFlowLink', NULL, NULL, NULL, 0, 0, 'https://payments.verisign.com/payflowlink', 0, 'N', 1, 0)
SET IDENTITY_INSERT [PaymentProcessors] OFF

INSERT INTO [ProductDetail] ([Type], [DisplayImage], [DisplayProductCode], [DisplayProductName], [DisplayCategory], [DisplayPriceSalePrice], [DisplayShortDescription], [DisplayLongDescription], [DisplayVendor], [DisplayManufacturer], [DisplayVolumePricing], [DisplayStockInfo], [DisplaySavedCartWishList], [DisplayEMailFriend], [DisplayLabels], [DisplayQty], [DisplayRecommendedProducts], [DisplayRecommendedImage], [DisplayRecommendedName], [DisplayRecommendedCode], [DisplayRecommendedShortDescription], [DisplayRecommendedPrice], [DefaultQty], [LinkImage], [LinkProductName], [LinkProductCode], [RecommendedTitle], [ImageSize], [IsActive], [DisplaySavings], [AttributeDisplay]) VALUES (1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 'Recommended Items:', 1, 1, 1, 1)
INSERT INTO [ProductDetail] ([Type], [DisplayImage], [DisplayProductCode], [DisplayProductName], [DisplayCategory], [DisplayPriceSalePrice], [DisplayShortDescription], [DisplayLongDescription], [DisplayVendor], [DisplayManufacturer], [DisplayVolumePricing], [DisplayStockInfo], [DisplaySavedCartWishList], [DisplayEMailFriend], [DisplayLabels], [DisplayQty], [DisplayRecommendedProducts], [DisplayRecommendedImage], [DisplayRecommendedName], [DisplayRecommendedCode], [DisplayRecommendedShortDescription], [DisplayRecommendedPrice], [DefaultQty], [LinkImage], [LinkProductName], [LinkProductCode], [RecommendedTitle], [ImageSize], [IsActive], [DisplaySavings], [AttributeDisplay]) VALUES (2, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 'Recommended Items:', 1, 0, 1, 1)

INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Accounts Receivable', NULL, '')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Sales', NULL, '')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Freight Income', 'Shipping & Handling', 'Shipping')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Freight Income', 'Shipping & Handling', 'Handling')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Sales Discounts', 'Order Discount', 'Discount')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Undeposited Funds', NULL, '')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('My Companies Checking', NULL, '')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Sales Tax Payable', NULL, '')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Gift Wrap', 'Product Gift Wrap', 'Gift Wrap')
INSERT INTO [QBAccounts] ([AccountName], [Description], [Item]) VALUES ('Order Discounts', 'Gift Certificates', 'GiftCertificate')

INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Product', '001', 'ProductIds', 'QuickBooks Products', 'Quickbooks Account')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Product', '002', '1001', 'web-1001', 'Products')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Product', '001', 'ProductIds', 'QuickBooks Products', 'Quickbooks Account')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Product', '002', '1001', 'web-1001', 'Products')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '000', 'UPS Next Day Air Early A.M.®', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '022', 'UPS Next Day Air®', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '023', 'UPS Next Day Air Saver®', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '024', 'UPS 2nd Day Air A.M.®', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '025', 'UPS 2nd Day Air®', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '026', 'UPS 3 Day Select®', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '027', 'UPS Ground', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '034', 'UPS Standard to Canada', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '045', 'UPS Worldwide Express (SM)', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '001', 'UPS Worldwide Express Plus (SM)', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '012', 'UPS Worldwide Expedited (SM)', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '028', 'UPS Express Saver (SM)', 'UPS', 'UPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '029', 'Canada Post - Domestic - Regular', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '030', 'Canada Post - Domestic - Expedited', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '031', 'Canada Post - Domestic - Xpresspost', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '032', 'Canada Post - Domestic - Priority Courier', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '033', 'Canada Post - Domestic - Expedited Evening', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '035', 'Canada Post - Domestic - Xpresspost Evening', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '036', 'Canada Post - Domestic - Expedited Saturday', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '037', 'Canada Post - Domestic - Xpresspost Saturday', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '038', 'Canada Post - USA - Surface', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '039', 'Canada Post - USA - Air', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '040', 'Canada Post - USA - Xpresspost', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '041', 'Canada Post - USA - Purolator', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '042', 'Canada Post - USA - Puropak', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '043', 'Canada Post - International - Surface', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '044', 'Canada Post - International - Air', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '046', 'Canada Post - International - Purolator', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '047', 'Canada Post - International - Puropak', 'Canada Post', 'Canada Post')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '048', 'FreightQuote', 'FreightQuote', 'FreightQuote')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '051', 'USPS Express Mail', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '052', 'USPS First Class Mail', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '053', 'USPS Priority Mail', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '054', 'USPS Parcel Post', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '055', 'USPS Bound Printed Material', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '002', 'USPS Library Mail', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '003', 'USPS Media Mail', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '004', 'USPS International', 'USPS', 'USPS')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '005', 'FedEx Priority', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '006', 'FedEx 2day', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '007', 'FedEx Standard Overnight', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '008', 'FedEx First Overnight', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '009', 'FedEx Express Saver', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '010', 'FedEx Overnight Freight', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '011', 'FedEx 2day Freight', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '013', 'FedEx Express Saver Freight', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '014', 'FedEx International Priority', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '015', 'FedEx International Economy', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '016', 'FedEx International First', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '017', 'FedEx International Priority Freight', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '018', 'FedEx International Economy Freight', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '019', 'FedEx Home Delivery', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '020', 'FedEx Ground Service', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', '021', 'FedEx Ground International Service', 'FedEx', 'FedEx')
INSERT INTO [QBConversion] ([PageName], [Sequence], [WebID], [QuickBooksID], [QBAccount]) VALUES ('Shipping', NULL, 'DEFAULT', NULL, NULL)

INSERT INTO [QBSettings] ([ToPrint], [OrderType], [Prefix], [LastNumber], [QBMemo], [ExtractedDate]) VALUES (1, 0, 'WEB-', '0', '', '10/21/2002')

INSERT INTO [QBTaxGroups] ([Sequence], [State], [Taxable], [IsGroupTax], [GroupName], [Rate]) VALUES ('001', 'Out of State', 0, 0, 'none', '0')
INSERT INTO [QBTaxGroups] ([Sequence], [State], [Taxable], [IsGroupTax], [GroupName], [Rate]) VALUES ('002', 'MI', 1, 1, 'Group Tax', '8.25')
INSERT INTO [QBTaxGroups] ([Sequence], [State], [Taxable], [IsGroupTax], [GroupName], [Rate]) VALUES ('003', 'CA', 1, 0, 'California Tax', '4.78')

INSERT INTO [QBTaxRates] ([TaxName], [TaxAgency], [TaxRate], [TaxAccount], [TaxGroupID]) VALUES ('tax1', 'Tax Agency 1', '6.25', 'Sales Tax Payable', 2)
INSERT INTO [QBTaxRates] ([TaxName], [TaxAgency], [TaxRate], [TaxAccount], [TaxGroupID]) VALUES ('tax2', 'Tax Agency 2', '2.00', 'Sales Tax Payable', 2)

INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '001', 'Transaction', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '002', 'Transaction Detail', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '003', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '004', 'Discount', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '005', 'Gift&nbsp;Wrap', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '006', 'Shipping', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '007', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '008', 'Handling', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '009', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '010', 'Credit Card Masked', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '011', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '012', 'Credit Card', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '013', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '014', 'Credit Card Expires', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '015', 'Credit Card Type', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '016', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '017', 'Email Address', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '018', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '019', 'Phone Number', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '020', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '021', 'Fax Number', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '022', 'Gift Certificates', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '023', 'Customer Comments', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '024', 'Sales Tax', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '025', 'End of Transaction', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '001', 'Transaction', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '002', 'Transaction Detail', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '003', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '004', 'Discount', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '006', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '007', 'Shipping', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '008', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '009', 'Handling', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '010', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '011', 'Credit Card Masked', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '012', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '013', 'Credit Card', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '014', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '015', 'Credit Card Expires', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '016', 'Credit Card Type', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '017', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '018', 'Email Address', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '019', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '020', 'Phone Number', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '021', 'Blank Line', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '022', 'Fax Number', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '023', 'Gift Certificates', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '024', 'Customer Comments', 1, 0, 1)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '025', 'Sales Tax', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '026', 'End of Transaction', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '026', 'Payment Transaction-Payments', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '027', 'Payment&nbsp;Transactions-AR', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Invoice', '028', 'End&nbsp;Payment&nbsp;Transaction', 1, 0, 0)
INSERT INTO [QBTemplates] ([PageName], [Sequence], [Description], [Active], [Skip], [ColumnVisible]) VALUES ('Receipt', '005', 'Gift&nbsp;Wrap', 1, 0, 1)

INSERT INTO [SearchOptions] ([KeyWord], [Category], [Manufacturer], [Vendor], [PriceBetween], [AddedOn], [SaleOnly]) VALUES (1, 1, 1, 1, 1, 1, 1)

INSERT INTO [SearchResults] ([Type], [DisplayImage], [DisplayProductCode], [DisplayProductName], [DisplayPriceSalePrice], [DisplayShortDescription], [DisplayVendor], [DisplayManufacturer], [DisplayVolumePricing], [DisplayStockInfo], [DisplayAddToCart], [DisplaySavedCartWishList], [DisplayEMailFriend], [DisplayMoreInfo], [DisplayLabels], [ProductsPerRow], [NumOfRows], [Alignment], [LinkImage], [LinkProductCode], [LinkProductName], [DefaultQty], [IsActive], [DisplayQty], [AttributeDisplay]) VALUES (1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 4, 2, 3, 1, 0, 1, 1, 0, 1, 1)
INSERT INTO [SearchResults] ([Type], [DisplayImage], [DisplayProductCode], [DisplayProductName], [DisplayPriceSalePrice], [DisplayShortDescription], [DisplayVendor], [DisplayManufacturer], [DisplayVolumePricing], [DisplayStockInfo], [DisplayAddToCart], [DisplaySavedCartWishList], [DisplayEMailFriend], [DisplayMoreInfo], [DisplayLabels], [ProductsPerRow], [NumOfRows], [Alignment], [LinkImage], [LinkProductCode], [LinkProductName], [DefaultQty], [IsActive], [DisplayQty], [AttributeDisplay]) VALUES (2, 0, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 5, 0, 0, 0, 1, 1, 1, 1, 1)
INSERT INTO [SearchResults] ([Type], [DisplayImage], [DisplayProductCode], [DisplayProductName], [DisplayPriceSalePrice], [DisplayShortDescription], [DisplayVendor], [DisplayManufacturer], [DisplayVolumePricing], [DisplayStockInfo], [DisplayAddToCart], [DisplaySavedCartWishList], [DisplayEMailFriend], [DisplayMoreInfo], [DisplayLabels], [ProductsPerRow], [NumOfRows], [Alignment], [LinkImage], [LinkProductCode], [LinkProductName], [DefaultQty], [IsActive], [DisplayQty], [AttributeDisplay]) VALUES (3, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 5, 3, 1, 0, 0, 1, 0, 1, 1)

INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AD', 'Andorra', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AE', 'United Arab Emirates', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AF', 'Afghanistan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AG', 'Antigua and Barbuda', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AI', 'Anguilla', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AL', 'Albania', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AM', 'Armenia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AN', 'Netherlands Antilles', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AO', 'Angola', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AR', 'Argentina', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AT', 'Austria', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AU', 'Australia', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AW', 'Aruba', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('AZ', 'Azerbaijan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BA', 'Bosnia-Herzegovina', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BB', 'Barbados', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BD', 'Bangladesh', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BE', 'Belgium', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BF', 'Burkina Faso', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BG', 'Bulgaria', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BH', 'Bahrain', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BI', 'Burundi', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BJ', 'Benin', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BM', 'Bermuda', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BN', 'Brunei Darussalam', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BO', 'Bolivia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BR', 'Brazil', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BS', 'Bahamas', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BT', 'Bhutan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BW', 'Botswana', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BY', 'Belarus', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('BZ', 'British Honduras (Belize)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CA', 'Canada', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CC', 'Cocos Island (Australia)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CD', 'Congo, Democratic Republic of the', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CF', 'Central African Republic', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CG', 'Congo (Brazzaville),Republic of the', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CH', 'Switzerland', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CI', 'Cote d''Ivoire (Ivory Coast)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CK', 'Cook Islands (New Zealand)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CL', 'Chile', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CM', 'Cameroon', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CN', 'China', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CO', 'Colombia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CR', 'Costa Rica', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CU', 'Cuba', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CV', 'Cape Verde', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CY', 'Cyprus', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CZ', 'Czech Republic', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('DE', 'Germany', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('DJ', 'Djibouti', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('DK', 'Denmark', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('DM', 'Dominica', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('DO', 'Dominican Republic', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('DZ', 'Algeria', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('EC', 'Ecuador', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('EE', 'Estonia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('EG', 'Egypt', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ER', 'Eritrea', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ES', 'Spain', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ET', 'Ethiopia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('FI', 'Finland', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('FJ', 'Fiji', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('FK', 'Falkland Islands', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('FO', 'Faroe Islands', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('FR', 'France', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GA', 'Gabon', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GB', 'Great Britain and Northern Ireland', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GD', 'Grenada', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GE', 'Georgia, Republic of', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GF', 'French Guiana', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GH', 'Ghana', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GI', 'Gibraltar', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GL', 'Greenland', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GM', 'Gambia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GN', 'Guinea', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GP', 'Guadeloupe', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GQ', 'Equatorial Guinea', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GR', 'Greece', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GS', 'South Georgia (Falkland Islands)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GT', 'Guatemala', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GW', 'Guinea-Bissau', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('GY', 'Guyana', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('HK', 'Hong Kong', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('HN', 'Honduras', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('HR', 'Croatia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('HT', 'Haiti', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('HU', 'Hungary', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ID', 'Indonesia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IE', 'Ireland', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IL', 'Israel', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IN', 'India', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IQ', 'Iraq', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IR', 'Iran', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IR', 'Persia (Iran)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IS', 'Iceland', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('IT', 'Italy', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('JM', 'Jamaica', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('JO', 'Jordan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('JP', 'Japan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KE', 'Kenya', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KG', 'Kyrgyzstan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KH', 'Cambodia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KI', 'Kiribati', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KM', 'Comoros', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KW', 'Kuwait', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KY', 'Cayman Islands', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KZ', 'Kazakhstan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LA', 'Laos', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LB', 'Lebanon', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LI', 'Liechtenstein', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LK', 'Sri Lanka', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LR', 'Liberia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LS', 'Lesotho', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LT', 'Lithuania', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LU', 'Luxembourg', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LV', 'Latvia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('LY', 'Libya', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MA', 'Morocco', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MC', 'Monaco (France)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MD', 'Moldova', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MG', 'Madagascar', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MK', 'Macedonia, Republic of', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ML', 'Mali', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MM', 'Burma', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MO', 'Macao', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MQ', 'Martinique', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MR', 'Mauritania', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MS', 'Montserrat', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MT', 'Malta', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MU', 'Mauritius', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MV', 'Maldives', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MW', 'Malawi', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MX', 'Mexico', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MY', 'Malaysia', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('MZ', 'Mozambique', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NA', 'Namibia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NC', 'New Caledonia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NE', 'Niger', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NF', 'Norfolk Island (Australia)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NG', 'Nigeria', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NI', 'Nicaragua', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NL', 'Netherlands', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NO', 'Norway', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NP', 'Nepal', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NR', 'Nauru', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NU', 'Niue (New Zealand)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('NZ', 'New Zealand', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('OM', 'Oman', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PA', 'Panama', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PE', 'Peru', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PF', 'French Polynesia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PG', 'Papua New Guinea', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PH', 'Philippines', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PK', 'Pakistan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PL', 'Poland', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PM', 'Saint Pierre and Miquelon', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PN', 'Pitcairn Island', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PT', 'Azores', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('PY', 'Paraguay', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('QA', 'Qatar', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('RE', 'Reunion', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('RO', 'Romania', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('RU', 'Russia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('RW', 'Rwanda', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SA', 'Saudi Arabia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SB', 'Solomon Islands', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SC', 'Seychelles', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SD', 'Sudan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SE', 'Sweden', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SG', 'Singapore', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SH', 'Saint Helena', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SI', 'Slovenia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SK', 'Slovak Republic', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SL', 'Saint Lucia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SM', 'San Marino', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SN', 'Senegal', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SO', 'Somalia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SR', 'Suriname', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ST', 'Sao Tome and Principe', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SV', 'El Salvador', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('SZ', 'Swaziland', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TC', 'Turks and Caicos Islands', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TD', 'Chad', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TF', 'French West Indies (Guadeloupe or Martinique)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TG', 'Togo', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TH', 'Thailand', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TJ', 'Tajikistan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TK', 'Tokelau (Union) Group (Western Samoa)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TL', 'East Timor (Indonesia)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TM', 'Turkmenistan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TN', 'Tunisia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TO', 'Tonga', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TR', 'Turkey', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TT', 'Trinidad and Tobago', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TV', 'Tuvalu', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TW', 'Taiwan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('TZ', 'Tanzania', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('UA', 'Ukraine', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('UG', 'Uganda', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('UY', 'Uruguay', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('UZ', 'Uzbekistan', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('VA', 'Vatican City', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('VC', 'Saint Vincent and the Grenadines', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('VE', 'Venezuela', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('VG', 'British Virgin Islands', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('VN', 'Vietnam', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('VU', 'Vanuatu', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('WF', 'Wallis and Futuna Islands', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('WS', 'Western Samoa', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('YE', 'Yemen', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('YT', 'Mayotte (France)', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('YU', 'Serbia-Montenegro', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ZA', 'South Africa', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ZM', 'Zambia', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('ZW', 'Zimbabwe', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('US', 'United States', 1, 1)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('CX', 'Christmas Island', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KN', 'St. Christopher and Nevis', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KP', 'North Korea', 1, NULL)
INSERT INTO [SelectCountry] ([Abbreviation], [Name], [IsActive], [UPSCountry]) VALUES ('KR', 'South Korea', 1, NULL)

INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('USD', '1033', 'United States - English', '840', 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ALL', '1052', 'Albania - Albanian', NULL, 'SQ')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('DZD', '5121', 'Algeria - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ARS', '11274', 'Argentina - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('AUD', '3081', 'Australia - English', '036', 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ATS', '3079', 'Austria - German', NULL, 'DE')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BHD', '15361', 'Bahrain - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BYR', '1059', 'Belarus - Belarusian', NULL, 'BE')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BEF', '2067', 'Belgium - French', NULL, 'FR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BZD', '10249', 'Belize - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BOB', '16394', 'Bolivia - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BRL', '1046', 'Brazil - Portuguese', NULL, 'PT')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BND', '2110', 'Brunei Darussalam - Malay', NULL, 'MS')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('BGL', '1026', 'Bulgaria - Bulgarian', NULL, 'BG')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CAD', '4105', 'Canada - English', '124', 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CAD', '3084', 'Canada - French', '124', 'FR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('GPB', '9225', 'Caribbean - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CLP', '13322', 'Chile - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('COP', '9226', 'Colombia - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CRC', '5130', 'Costa Rica - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('HRK', '1050', 'Croatia - Croatian', NULL, 'HR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CZK', '1029', 'Czech Republic - Czech', NULL, 'CS')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('DKK', '1030', 'Denmark - Danish', '208', 'DA')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('DOP', '7178', 'Dominican Republic - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ECS', '12298', 'Ecuador - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('EGP', '3073', 'Egypt - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('SVC', '17418', 'El Salvador - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('EEK', '1061', 'Estonia - Estonian', NULL, 'ET')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('DKK', '1080', 'Faeroe Islands - Faeroese', '208', NULL)
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('FIM', '1035', 'Finland - Finnish', NULL, 'FI')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('FRF', '1036', 'France - French', NULL, 'FR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('DEM', '1031', 'Germany - German', NULL, 'DE')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('GRD', '1032', 'Greece - Greek', NULL, 'EL')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('GTQ', '4106', 'Guatemala - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('HNL', '18442', 'Honduras - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('HKD', '3076', 'Hong Kong - Chinese', '344', 'ZH')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('HUF', '1038', 'Hungary - Hungarian', NULL, 'HU')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ISK', '1039', 'Iceland - Icelandic', NULL, 'IS')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('INR', '1081', 'India - Hindi', NULL, 'HI')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('IDR', '1057', 'Indonesia - Indonesian', NULL, 'IN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('IRR', '1065', 'Iran - Farsi', NULL, NULL)
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('IQD', '2049', 'Iraq - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('IEP', '6153', 'Ireland - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ILS', '1037', 'Israel - Hebrew', NULL, 'IW')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ITL', '1040', 'Italy - Italian', NULL, 'IT')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('JMD', '8201', 'Jamaica - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('JPY', '1041', 'Japan - Japanese', '392', 'JA')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('JOD', '11265', 'Jordan - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('KES', '1089', 'Kenya - Swahili', NULL, 'SW')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('KPW', '1042', 'Korea - Korean (Ext. Wansung)', NULL, 'KO')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('KWD', '13313', 'Kuwait - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('LVL', '1062', 'Latvia - Latvian', NULL, 'LV')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('LBP', '13313', 'Lebanon - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('LYD', '12289', 'Libya - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CHF', '5127', 'Liechtenstein - German', '756', 'DE')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('LTL', '1063', 'Lithuania - Lithuanian', NULL, 'LT')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('LUF', '5132', 'Luxembourg - French', NULL, 'FR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('LUF', '4103', 'Luxembourg - German', NULL, 'DE')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('MOP', '5124', 'Macau - Chinese', NULL, 'ZH')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('MKD', '1071', 'Macedonia - Macedonian', NULL, 'MK')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('MYR', '1086', 'Malaysia - Malay', NULL, 'MS')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('MXP', '2058', 'Mexico - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('FRF', '6156', 'Monaco - French', NULL, 'FR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('MAD', '6145', 'Morocco - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('NLG', '1043', 'Netherlands - Dutch', NULL, 'NL')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('NZD', '5129', 'New Zealand - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('NIO', '19466', 'Nicaragua - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('NOK', '1044', 'Norway (Bokmal) - Norwegian', '578', 'NO')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('NOK', '2068', 'Norway (Nynorsk) - Norwegian', '578', 'NO')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('OMR', '8193', 'Oman - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('PKR', '1056', 'Pakistan - Urdu', NULL, 'UR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('PAB', '6154', 'Panama - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('PYG', '10250', 'Paraguay - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('PEN', '10250', 'Peru - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('PHP', '13321', 'Philippines - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('PLZ', '1045', 'Poland - Polish', NULL, 'PL')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('PTE', '2070', 'Portugal - Portuguese', NULL, 'PT')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CNY', '2052', 'PRC - Chinese', NULL, 'ZH')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('USD', '20490', 'Puerto Rico - Spanish', '840', 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('QAR', '16385', 'Qatar - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ROL', '1048', 'Romania - Romanian', NULL, 'RO')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('RUR', '1049', 'Russia - Russian', NULL, 'RU')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('SAR', '1025', 'Saudi Arabia - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('YUN', '3098', 'Serbia (Cyrillic) - Serbian', NULL, 'SR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('YUN', '2074', 'Serbia (Latin) - Serbian', NULL, 'SR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('SGD', '4100', 'Singapore - Chinese', NULL, 'ZH')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('SKK', '1051', 'Slovakia - Slovak', NULL, 'SK')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('SIT', '1060', 'Slovenia - Slovene', NULL, NULL)
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ZAR', '7177', 'South Africa - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ZAR', '1078', 'South Africa - Afrikaans', NULL, 'AF')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ESP', '1069', 'Spain - Basque', NULL, 'EU')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ESP', '1027', 'Spain - Catalan', NULL, 'CA')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ESP', '3082', 'Spain (Mod. Sort) - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ESP', '1034', 'Spain (Trad. Sort) - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('SEK', '1053', 'Sweden - Swedish', '752', 'SV')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CHF', '4108', 'Switzerland - French', '756', 'FR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CHF', '2055', 'Switzerland - German', '756', 'DE')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('CHF', '2064', 'Switzerland - Italian', '756', 'IT')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('SYP', '10241', 'Syria - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('TWD', '1028', 'Taiwan - Chinese', NULL, 'ZH')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('THB', '1054', 'Thailand - Thai', NULL, 'TH')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('TTD', '11273', 'Trinidad - English', NULL, 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('TND', '7169', 'Tunisia - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('TRL', '1055', 'Turkey - Turkish', NULL, 'TR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('AED', '14337', 'U.A.E. - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('UAH', '1058', 'Ukraine - Ukranian', NULL, 'UK')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('GBP', '2057', 'United Kingdom - English', '826', 'EN')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('UYU', '14346', 'Uruguay - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('VEB', '8202', 'Venezuela - Spanish', NULL, 'ES')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('VND', '1066', 'Vietnam - Vietnamese', NULL, 'VI')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('YER', '9217', 'Yemen - Arabic', NULL, 'AR')
INSERT INTO [SelectISOCurrency] ([ISOCurrency], [LCID], [Name], [Code], [LanguageCode]) VALUES ('ZWD', '12297', 'Zimbabwe - English', NULL, 'EN')

INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AA', 'Armed Forces America', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AB', 'Alberta', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AE', 'Armed Forces Other Areas', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AK', 'Alaska', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AL', 'Alabama', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AP', 'Armed Forces Pacific', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AR', 'Arkansas', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AS', 'American Samoa', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('AZ', 'Arizona', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('BC', 'British Columbia', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('CA', 'California', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('CO', 'Colorado', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('CT', 'Connecticut', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('DC', 'District of Columbia', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('DE', 'Delaware', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('FL', 'Florida', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('FM', 'Micronesia', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('GA', 'Georgia', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('GU', 'Guam', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('HI', 'Hawaii', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('IA', 'Iowa', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('ID', 'Idaho', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('IL', 'Illinois', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('IN', 'Indiana', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('KS', 'Kansas', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('KY', 'Kentucky', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('LA', 'Louisiana', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MA', 'Massachusetts', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MB', 'Manitoba', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MD', 'Maryland', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('ME', 'Maine', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MH', 'Marshall Islands', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MI', 'Michigan', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MN', 'Minnesota', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MO', 'Missouri', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MP', 'Northern Mariana Islands', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MS', 'Mississippi', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('MT', 'Montana', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NB', 'New Brunswick', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NC', 'North Carolina', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('ND', 'North Dakota', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NE', 'Nebraska', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NF', 'Newfoundland', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NH', 'New Hampshire', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NJ', 'New Jersey', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NM', 'New Mexico', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NS', 'Nova Scotia', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NT', 'Northwest Territories', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NV', 'Nevada', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NY', 'New York', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('OH', 'Ohio', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('OK', 'Oklahoma', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('ON', 'Ontario', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('OR', 'Oregon', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('PA', 'Pennsylvania', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('PE', 'Prince Edward Island', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('PR', 'Puerto Rico', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('PW', 'Palau', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('QC', 'Quebec', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('RI', 'Rhode Island', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('SC', 'South Carolina', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('SD', 'South Dakota', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('SK', 'Saskatchewan', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('TN', 'Tennessee', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('TX', 'Texas', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('UT', 'Utah', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('VA', 'Virginia', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('VI', 'Virgin Islands', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('VT', 'Vermont', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('WA', 'Washington', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('WI', 'Wisconsin', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('WV', 'West Virginia', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('WY', 'Wyoming', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('YK', 'Yukon', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('NU', 'Nunavut', 1)
INSERT INTO [SelectState] ([Abbreviation], [Name], [IsActive]) VALUES ('N/A', 'Not Applicable', 1)

INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Next Day Air Early A.M.®', '14', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Next Day Air®', '01', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Next Day Air Saver®', '13', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS 2nd Day Air A.M.®', '59', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS 2nd Day Air®', '02', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS 3 Day Select®', '12', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Ground', '03', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Standard to Canada', '11', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Worldwide Express (SM)', '07', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Worldwide Express Plus (SM)', '54', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Worldwide Expedited (SM)', '08', 1, 0, '100', 1)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Regular', '1010', 0, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Expedited', '1020', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Xpresspost', '1030', 0, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Priority Courier', '1040', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Expedited Evening', '1120', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Xpresspost Evening', '1130', 0, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Expedited Saturday', '1220', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - Domestic - Xpresspost Saturday', '1230', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - USA - Surface', '2010', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - USA - Air', '2020', 0, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - USA - Xpresspost', '2030', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - USA - Purolator', '2040', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - USA - Puropak', '2050', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - International - Surface', '3010', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - International - Air', '3020', 0, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - International - Purolator', '3040', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('Canada Post - International - Puropak', '3050', 1, 0, '100', 4)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FreightQuote', 'LTL', 1, 0, '100', 5)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS Express Mail', 'Express', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS First Class Mail', 'First Class', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS Priority Mail', 'Priority', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS Parcel Post', 'Parcel', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS Bound Printed Material', 'BPM', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS Library Mail', 'Library', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS Media Mail', 'Media', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('USPS International', 'USPSINT', 1, 0, '100', 2)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Priority', 'FDXED01', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx 2day', 'FDXED03', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Standard Overnight', 'FDXED05', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx First Overnight', 'FDXED06', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Express Saver', 'FDXED20', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Overnight Freight', 'FDXED70', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx 2day Freight', 'FDXED80', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Express Saver Freight', 'FDXED83', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx International Priority', 'FDXEI01', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx International Economy', 'FDXEI03', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx International First', 'FDXEI06', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx International Priority Freight', 'FDXEI70', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx International Economy Freight', 'FDXEI86', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Home Delivery', 'FDXGD90', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Ground Service', 'FDXGD92', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('FedEx Ground International Service', 'FDXGI92', 1, 0, '100', 3)
INSERT INTO [Shipping] ([Method], [Code], [IsActive], [Edit], [Rates], [CarrierID]) VALUES ('UPS Express Saver (SM)', '65', 0, 0, '100', 1)

INSERT INTO [ShippingCarriers] ([Name], [Code], [Active], [UserName], [Pass], [AccessCode]) VALUES ('UPS®', 'UPS', 1, NULL, NULL, NULL)
INSERT INTO [ShippingCarriers] ([Name], [Code], [Active], [UserName], [Pass], [AccessCode]) VALUES ('USPS', 'USPS', 1, NULL, NULL, NULL)
INSERT INTO [ShippingCarriers] ([Name], [Code], [Active], [UserName], [Pass], [AccessCode]) VALUES ('FEDEX', 'FEDEX', 1, NULL, NULL, NULL)
INSERT INTO [ShippingCarriers] ([Name], [Code], [Active], [UserName], [Pass], [AccessCode]) VALUES ('Canada Post', 'CP', 1, NULL, NULL, NULL)
INSERT INTO [ShippingCarriers] ([Name], [Code], [Active], [UserName], [Pass], [AccessCode]) VALUES ('FreightQuote', 'LTL', 1, NULL, NULL, NULL)
INSERT INTO [ShippingCarriers] ([Name], [Code], [Active], [UserName], [Pass], [AccessCode]) VALUES ('<Please Select a Carrier>', 'NONE', 1, NULL, NULL, NULL)

INSERT INTO [ValueShipping] ([MinTotal], [MaxTotal], [Amount]) VALUES ('0', '25', '2')
INSERT INTO [ValueShipping] ([MinTotal], [MaxTotal], [Amount]) VALUES ('25', '50', '4')
INSERT INTO [ValueShipping] ([MinTotal], [MaxTotal], [Amount]) VALUES ('50', '75', '6')
INSERT INTO [ValueShipping] ([MinTotal], [MaxTotal], [Amount]) VALUES ('75', '100', '8')
INSERT INTO [ValueShipping] ([MinTotal], [MaxTotal], [Amount]) VALUES ('100', '150', '10')
INSERT INTO [ValueShipping] ([MinTotal], [MaxTotal], [Amount]) VALUES ('150', '500', '12')

INSERT INTO [Vendors] ([AddressID]) VALUES (2)
/* ==================================================================================================== */
/*
END	DATA POPULATION
*/
/* ==================================================================================================== */

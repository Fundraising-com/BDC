/** Migration Scripts **/
/**
*
* Authors: Jason Farrel and Javier Arellano
* Created date: 2015-FEB-12
* Description: The following scripts must be run before deploying the web application to PROD for the FIRST TIME only.
* The scripts must be run in the same order as they appear in this file.
*
**/
USE EFundStore;
PRINT 'SCRIPT START';
PRINT 'STEP: Delete current Newsletter table';
DROP TABLE newsletter;
PRINT 'STEP: Create new Newsletter table';
/****** Object:  Table [dbo].[newsletter]    Script Date: 02/12/2015 15:10:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[newsletter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[url] [nvarchar](200) NOT NULL,
	[body] [nvarchar](max) NOT NULL,
	[created_on] [datetime] NOT NULL,
	[enabled] [bit] NOT NULL,
	[author] [nvarchar](128) NOT NULL,
	[updated_on] [datetime] NULL,
	[partner] [int] NULL,
	[display_order] [tinyint] NOT NULL,
 CONSTRAINT [PK_newsletter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[newsletter]  WITH CHECK ADD  CONSTRAINT [FK_newsletter_online_user] FOREIGN KEY([author])
REFERENCES [dbo].[AspNetUsers] ([id])
GO

ALTER TABLE [dbo].[newsletter] CHECK CONSTRAINT [FK_newsletter_online_user]
GO

ALTER TABLE [dbo].[newsletter] ADD  CONSTRAINT [DF_newsletter_enabled]  DEFAULT ((1)) FOR [enabled]
GO

CREATE NONCLUSTERED INDEX [i_newsletter_url] ON [dbo].[newsletter] 
(
	[url] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


CREATE TABLE [dbo].[shipping_fee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[is_default] BIT NOT NULL DEFAULT(0)
 CONSTRAINT [PK_shipping_fee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[shipping_fee_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shipping_fee_id] INT NOT NULL,
	[minimum_quantity] INT NOT NULL,
	[maximum_quantity] INT NOT NULL,
	[fee] FLOAT NOT NULL,
 CONSTRAINT [PK_shipping_fee_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[shipping_fee_detail]  WITH CHECK ADD  CONSTRAINT [FK_shipping_fee_detail] FOREIGN KEY([shipping_fee_id])
REFERENCES [dbo].[shipping_fee] ([id])
GO
PRINT 'STEP: Fill Newsletter table';
--UNDONE: Javi, add the newsletters
PRINT 'STEP: Add Order column to Package table';
use eFundstore;
ALTER TABLE package
ADD [order] int NOT NULL DEFAULT(1), [shipping_fee_id] INT NULL;
GO
ALTER TABLE [dbo].[package]  WITH CHECK ADD  CONSTRAINT [FK_package_shipping_fee] FOREIGN KEY([shipping_fee_id])
REFERENCES [dbo].[shipping_fee] ([id])
GO
PRINT 'STEP: Add Url column to Package table';
use eFundstore;
ALTER TABLE package
ADD [url] varchar(100) NULL;
PRINT 'STEP: Fill information to Package table for USA Categories';
use eFundstore;
INSERT INTO package (parent_package_id, name, profit_percentage, enabled, create_date, [order], url, shipping_fee_id) VALUES (151, 'Fundraising MVC', NULL, 1, GETDATE(), 1, '', NULL); -- ROOT
--
INSERT INTO package SELECT package_id, 'Scratchcards', NULL, 1, GETDATE(), 1, 'Scratchcards', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Chocolate', NULL, 1, GETDATE(), 2, 'Chocolate', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Pretzel Rods', NULL, 1, GETDATE(), 4, 'Pretzel-Rods', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Cookie Dough', NULL, 1, GETDATE(), 5, 'Cookie-Dough', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Lollipops', NULL, 1, GETDATE(), 6, 'Lollipops', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Beef Jerky', NULL, 1, GETDATE(), 8, 'Beef-Jerky', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Order Takers', NULL, 1, GETDATE(), 9, 'Order-Takers', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Discount Fundraisers', NULL, 1, GETDATE(), 10, 'Discount-Fundraisers', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Magazines', NULL, 1, GETDATE(), 11, 'Magazines', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Smencils', NULL, 1, GETDATE(), 12, 'Smencils', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Smens', NULL, 1, GETDATE(), 13, 'Smens', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Smanimals', NULL, 1, GETDATE(), 14, 'Smanimals', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'T-Shirts', NULL, 1, GETDATE(), 15, 'T-Shirts', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Snifty', NULL, 1, GETDATE(), 16, 'Snifty', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Custom Sports Apparel', NULL, 1, GETDATE(), 17, 'Custom-Sports-Apparel', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Heritage Candles', NULL, 1, GETDATE(), 18, 'Heritage-Candles', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Recycled Trash Bags', NULL, 1, GETDATE(), 19, 'Recycled-Trash-Bags', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'To Remember This', NULL, 1, GETDATE(), 20, 'To-Remember-This', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Popcorn', NULL, 1, GETDATE(), 21, 'Popcorn', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Tumblers', NULL, 1, GETDATE(), 22, 'Tumblers', NULL FROM package where name = 'Fundraising MVC'
INSERT INTO package SELECT package_id, 'Water Bottles', NULL, 1, GETDATE(), 23, 'Water-Bottles', NULL FROM package where name = 'Fundraising MVC'
--
INSERT INTO package SELECT P1.package_id, 'Scratchcards', NULL, 1,GETDATE(), 1, 'all-scratchcards', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Scratchcards'
INSERT INTO package SELECT P1.package_id, 'Skinny Cows', NULL, 1,GETDATE(), 1, 'Skinny-Cows', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Chocolate'
INSERT INTO package SELECT P1.package_id, 'Nestle', NULL, 1,GETDATE(), 2, 'Nestle', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Chocolate'
INSERT INTO package SELECT P1.package_id, 'Kathryn Beich', NULL, 1,GETDATE(), 3, 'Kathryn-Beich', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Chocolate'
INSERT INTO package SELECT P1.package_id, 'One Dollar Bar Chocolate', NULL, 1,GETDATE(), 4, 'One-Dollar-Bar-Chocolate', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Chocolate'
INSERT INTO package SELECT P1.package_id, 'Chocolatiers', NULL, 1,GETDATE(), 5, 'Chocolatiers', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Chocolate'
INSERT INTO package SELECT P1.package_id, 'Pretzel Rods', NULL, 1,GETDATE(), 1, 'all-pretzel-rods', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Pretzel Rods'
INSERT INTO package SELECT P1.package_id, 'Cookie Dough', NULL, 1,GETDATE(), 1, 'all-cookie-dough', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Cookie Dough'
INSERT INTO package SELECT P1.package_id, 'Lollipops', NULL, 1,GETDATE(), 1, 'all-lollipops', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Lollipops'
INSERT INTO package SELECT P1.package_id, 'Monogram Beef Jerky', NULL, 1,GETDATE(), 1, 'Monogram-Beef-Jerky', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Beef Jerky'
INSERT INTO package SELECT P1.package_id, 'Jack Links', NULL, 1,GETDATE(), 2, 'Jack-Links', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Beef Jerky'
INSERT INTO package SELECT P1.package_id, 'Order Takers', NULL, 1,GETDATE(), 1, 'all-order-takers', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Order Takers'
INSERT INTO package SELECT P1.package_id, 'Discount Fundraisers', NULL, 1,GETDATE(), 1, 'all-discount-fundraisers', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Discount Fundraisers'
INSERT INTO package SELECT P1.package_id, 'Magazines', NULL, 1,GETDATE(), 1, 'all-magazines', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Magazines'
INSERT INTO package SELECT P1.package_id, 'Smencils', NULL, 1,GETDATE(), 1, 'all-Smencils', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Smencils'
INSERT INTO package SELECT P1.package_id, 'Smanimals', NULL, 1,GETDATE(), 1, 'all-Smanimals', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Smanimals'
INSERT INTO package SELECT P1.package_id, 'T-Shirts', NULL, 1,GETDATE(), 1, 'all-T-Shirts', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'T-Shirts'
INSERT INTO package SELECT P1.package_id, 'Snifty', NULL, 1,GETDATE(), 1, 'all-Snifties', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Snifty'
INSERT INTO package SELECT P1.package_id, 'Custom Sports Apparel', NULL, 1,GETDATE(), 1, 'all-Custom-Sports-Apparels', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Custom Sports Apparel'
INSERT INTO package SELECT P1.package_id, 'Heritage Candles', NULL, 1,GETDATE(), 1, 'all-Heritage-Candles', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Heritage Candles'
INSERT INTO package SELECT P1.package_id, 'Recycled Trash Bags', NULL, 1,GETDATE(), 1, 'all-Recycled-Trash-Bags', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Recycled Trash Bags'
INSERT INTO package SELECT P1.package_id, 'To Remember This', NULL, 1,GETDATE(), 1, 'all-To-Remember-This', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'To Remember This'
INSERT INTO package SELECT P1.package_id, 'Popcorn', NULL, 1,GETDATE(), 1, 'all-Popcorn', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Popcorn'
INSERT INTO package SELECT P1.package_id, 'Tumblers', NULL, 1,GETDATE(), 1, 'all-Tumblers', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Tumblers'
INSERT INTO package SELECT P1.package_id, 'Water Bottles', NULL, 1,GETDATE(), 1, 'all-Water-Bottles', NULL FROM package P0 JOIN package P1 ON P0.package_id = P1.parent_package_id WHERE P0.name = 'Fundraising MVC' AND P1.name = 'Water Bottles'
--
DECLARE @productID INT;



PRINT 'STEP: Fill information to Package table for Canada Categories';


PRINT 'STEP: Delete current Page Route Mapper table';
use eFundstore;
DROP TABLE page_route_mapper;


PRINT 'STEP: Create new Page Route Mapper table';
use eFundstore;
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[page_route_mapper](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[source] [nvarchar](max) NOT NULL,
	[destination] [nvarchar](max) NOT NULL,
	[created] [datetime] NOT NULL,
	[enabled] [bit] NOT NULL DEFAULT(1),	
 CONSTRAINT [PK_page_route_mapper] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

PRINT 'STEP: Update product_desc table';
use eFundstore;
ALTER TABLE product_desc
ADD url NVARCHAR(200), [description] NVARCHAR(MAX), [flavors] NVARCHAR(MAX), [packaging] NVARCHAR(MAX), [extra_information] NVARCHAR(MAX), [base_price] float, [is_store_purchasable] bit NULL DEFAULT(0), [retail_price] float NULL DEFAULT(0), [minimum_quantity] INT NULL DEFAULT(1);
GO
UPDATE product_desc SET is_store_purchasable = 0, retail_price = 0, minimum_quantity = 1;
GO

PRINT 'STEP: Update product table';
use eFundstore;
ALTER TABLE product
ADD is_featured BIT NULL DEFAULT(0);
GO
UPDATE product SET is_featured = 0;
GO

PRINT 'STEP: Delete current Product Image table';
use eFundstore;
DROP TABLE product_image;


PRINT 'STEP: Create new Product Image table';
use eFundstore;
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[product_image](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[url] [nvarchar](max) NOT NULL,
	[alternative_text] [nvarchar](200) NULL,
	[created] [datetime] NOT NULL,
	[enabled] [bit] NOT NULL DEFAULT(1),	
	[sort] [int] NOT NULL DEFAULT(1),
	[is_cover] [bit] NOT NULL DEFAULT(0)
 CONSTRAINT [PK_product_image] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[product_image]  WITH CHECK ADD  CONSTRAINT [FK_product_image_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO

PRINT 'STEP: Delete current Shopping Cart Item table';
use eFundstore;
DROP TABLE shopping_cart_item;

PRINT 'STEP: Create new Shopping Cart Item table';
use eFundstore;
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[shopping_cart_item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopping_cart_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,	
	[quantity] [int] NOT NULL DEFAULT(1),
	[comments] [nvarchar](400) NULL,
	[created] [datetime] NOT NULL,	
 CONSTRAINT [PK_shopping_cart_item] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

PRINT 'STEP: Delete current Shopping Cart Code table';
use eFundstore;
DROP TABLE shopping_cart_code;


PRINT 'STEP: Delete current Shopping Cart table';
use eFundstore;
DROP TABLE shopping_cart;

PRINT 'STEP: Create new Shopping Cart table';
use eFundstore;
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[shopping_cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [nvarchar](128) NULL,
	[anonymous_id] [nvarchar](128) NULL,
	[status] [int] NOT NULL DEFAULT(1),
	[comments] [nvarchar](400) NULL,
	[created] [datetime] NOT NULL,	
 CONSTRAINT [PK_shopping_cart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



GO
ALTER TABLE [dbo].[shopping_cart_item]  WITH CHECK ADD  CONSTRAINT [FK_shopping_cart_item] FOREIGN KEY([shopping_cart_id])
REFERENCES [dbo].[shopping_cart] ([id])
GO

GO
ALTER TABLE [dbo].[shopping_cart_item]  WITH CHECK ADD  CONSTRAINT [FK_shopping_cart_item_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
USE [eFundstore]
GO

CREATE NONCLUSTERED INDEX [i_shopping_cart_anonymous_id] ON [dbo].[shopping_cart] 
(
	[anonymous_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

USE [eFundstore]
GO

CREATE NONCLUSTERED INDEX [i_shopping_cart_user_id] ON [dbo].[shopping_cart] 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

PRINT 'STEP: Create ASP.Net Membership Provider tables';

USE [EFundStore]
GO
 
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 5/15/2014 3:57:55 PM ******/
SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
SET ANSI_PADDING ON
GO
 
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
GO
 
SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
GO

SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Hometown] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
 
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO

SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
GO
 
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
 
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
GO
 
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
 
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
 
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
 
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUsers] add profile_id INT NOT NULL;

GO


CREATE TABLE [dbo].[AspNetUserProfiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[GroupName] [nvarchar](200) NULL,
	[BillingFirstName] [nvarchar](200) NULL,
	[BillingLastName] [nvarchar](200) NULL,
	[BillingPhone] [nvarchar](30) NULL,
	[BillingAddress] [nvarchar](500) NULL,
	[BillingCity] [nvarchar](200) NULL,
	[BillingState] [nvarchar](5) NULL,
	[BillingZIP] [nvarchar](10) NULL,
	[BillingAddressType] [int] NULL	
 CONSTRAINT [PK_dbo.AspNetUserProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUsers_dbo.AspNetUserProfiles_ProfileId] FOREIGN KEY([Profile_Id])
REFERENCES [dbo].[AspNetUserProfiles] ([Id])

ALTER TABLE [dbo].[shopping_cart]  ADD  CONSTRAINT [FK_dbo.shopping_cart_dbo.AspNetUser_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])

PRINT 'STEP: Delete current Session Item table';
DROP TABLE session_item;

PRINT 'STEP: Delete current Session table';
DROP TABLE [session];
PRINT 'STEP: Create new Session table';
/****** Object:  Table [dbo].[newsletter]    Script Date: 02/12/2015 15:10:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[session](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[anonymous_id] [nvarchar](128) NOT NULL UNIQUE,
	[created_on] [datetime] NOT NULL,	
 CONSTRAINT [PK_session] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
PRINT 'STEP: Create new Session Item table';
CREATE TABLE [dbo].[session_item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sessionId] INT NOT NULL,
	[created_on] [datetime] NOT NULL,
	[name] [nvarchar](128) NOT NULL,
	[value] [nvarchar](400) NOT NULL,
 CONSTRAINT [PK_session_item] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[session_item]  WITH CHECK ADD  CONSTRAINT [FK_session_item_session] FOREIGN KEY([sessionId])
REFERENCES [dbo].[session] ([id])
GO

INSERT INTO [eFundstore].[dbo].[AspNetRoles] ([Id],[Name]) VALUES (1 ,'admin');
INSERT INTO [eFundstore].[dbo].[AspNetRoles] ([Id],[Name]) VALUES (2 ,'editor');
INSERT INTO [eFundstore].[dbo].[AspNetRoles] ([Id],[Name]) VALUES (3 ,'regular');

PRINT 'STEP: Create Banners table';
CREATE TABLE [dbo].[banner](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created_on] [datetime] NOT NULL,
	[image] [nvarchar](400) NOT NULL,
	[url] [nvarchar](400) NOT NULL,
	[partner_id] INT NOT NULL,
	[type] INT NOT NULL,
	[alternative_text] [nvarchar](400) NULL,
	[is_active] [bit] NOT NULL DEFAULT (1)
 CONSTRAINT [PK_banner] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
PRINT 'STEP: Create View Port table';
CREATE TABLE [dbo].[view_port](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created_on] [datetime] NOT NULL,
	[name] [nvarchar](40) NOT NULL,
	[bootstrap_class] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_view_port] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

PRINT 'STEP: Create View Port table';
CREATE TABLE [dbo].[banner_view_port](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[banner_id] INT NOT NULL,
	[view_port_id] INT NOT NULL,
 CONSTRAINT [PK_banner_view_port] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[banner_view_port]  WITH CHECK ADD  CONSTRAINT [FK_banner_view_port_banner] FOREIGN KEY([banner_id])
REFERENCES [dbo].[banner] ([id])
GO
ALTER TABLE [dbo].[banner_view_port]  WITH CHECK ADD  CONSTRAINT [FK_banner_view_port_view_port] FOREIGN KEY([view_port_id])
REFERENCES [dbo].[view_port] ([id])
GO
PRINT 'STEP: Insert View Port table';
INSERT INTO [eFundstore].[dbo].[view_port] ([created_on],[name],[bootstrap_class]) VALUES (GETDATE(), 'Extra Small', 'visible-xs');
INSERT INTO [eFundstore].[dbo].[view_port] ([created_on],[name],[bootstrap_class]) VALUES (GETDATE(), 'Small', 'visible-sm');
INSERT INTO [eFundstore].[dbo].[view_port] ([created_on],[name],[bootstrap_class]) VALUES (GETDATE(), 'Medium', 'visible-md');
INSERT INTO [eFundstore].[dbo].[view_port] ([created_on],[name],[bootstrap_class]) VALUES (GETDATE(), 'Large', 'visible-lg');
GO
PRINT 'STEP: Insert mobile Banners for Partner 0';
INSERT INTO [eFundstore].[dbo].[banner]([created_on],[image],[url],[alternative_text],[is_active],[partner_id],[type]) VALUES (GETDATE(),'0_xs_spring2015kit01.jpg','request-a-kit','Request a Free Guide',0,0,1)
INSERT INTO [eFundstore].[dbo].[banner]([created_on],[image],[url],[alternative_text],[is_active],[partner_id],[type]) VALUES (GETDATE(),'0_xs_spring2015kit02.jpg','request-a-kit','Request a Free Guide',0,0,1)
INSERT INTO [eFundstore].[dbo].[banner]([created_on],[image],[url],[alternative_text],[is_active],[partner_id],[type]) VALUES (GETDATE(),'0_xs_spring2015kit03.jpg','request-a-kit','Request a Free Guide',0,0,1)
INSERT INTO banner_view_port (banner_id, view_port_id) SELECT Id, 1 FROM banner

PRINT 'STEP: Create Package Exlclusion table';
CREATE TABLE [dbo].[package_exclusion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[package_id] INT NOT NULL,
	[partner_id] INT NOT NULL,
 CONSTRAINT [PK_package_exclusion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[package_exclusion]  WITH CHECK ADD  CONSTRAINT [FK_package_exclusion_package] FOREIGN KEY([package_id])
REFERENCES [dbo].[package] ([package_id])
GO
insert into package_exclusion select 474, partner_id from partner
GO
PRINT 'STEP: Create Product Profit table';
CREATE TABLE [dbo].[product_profit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] INT NOT NULL,
	[price] FLOAT NOT NULL,
	[min_qty] INT NOT NULL,
	[max_qty] INT NOT NULL,
 CONSTRAINT [PK_product_profit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[product_profit]  WITH CHECK ADD  CONSTRAINT [FK_product_profit_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
PRINT 'STEP: Create Representative Fundraisers tables';
use fastfundraising;
CREATE TABLE [dbo].[fundraiser_category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[image] [nvarchar](100) NOT NULL,
	[display_order] [int] NOT NULL DEFAULT(1),
 CONSTRAINT [PK_fundraiser_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[fundraiser_product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[image] [nvarchar](100) NOT NULL,
	[sell_sheet_path] [nvarchar](100) NULL,
	[store_url] [nvarchar](100) NULL,
	[nutrition_information_sheet_path] [nvarchar](100) NULL,
	[has_nutrition_information_sheet] [bit] NOT NULL,
	[has_sell_sheet] [bit] NOT NULL,
	[is_purchasable] [bit] NOT NULL,
	[fundraiser_category_id] [int] NOT NULL,
 CONSTRAINT [PK_fundraiser_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[fundraiser_product]  WITH CHECK ADD  CONSTRAINT [FK_fundraiser_product_category] FOREIGN KEY([fundraiser_category_id])
REFERENCES [dbo].[fundraiser_category] ([id])
GO
use fastfundraising;
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Beef Jerky','01.jpg',1);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Custom Apparel','02.jpg',2);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Lollipops','03.jpg',3);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Chocolate','04.jpg',4);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Cookie Dough','05.jpg',5);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Gift Items','06.jpg',6);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Pretzel Rods','07.jpg',7);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('GA Saving Pass','08.jpg',8);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('To Remember This','09.jpg',9);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Scratchcards','10.jpg',10);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Snifty','11.jpg',11);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Smencils','12.jpg',12);
INSERT INTO fundraiser_category (name, image, display_order) VALUES ('Magazines','13.jpg',13);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Monogram Meat Snacks','01.jpg','01.pdf','beef-jerky','01.pdf',1,1,1,1);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Jack Links Meats Snacks','02.jpg','02.pdf','beef-jerky','02.pdf',1,1,1,1);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Tees','03.jpg','03.pdf','','',0,1,0,2);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Hoodies','04.jpg','04.pdf','','',0,1,0,2);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Sport Apparel','05.jpg','05.pdf','','',0,1,0,2);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Lollipops','06.jpg','06.pdf','lollipops','06.pdf',1,1,1,3);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Chocolate','07.jpg','','chocolate','07.pdf',1,0,1,4);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Cookie Dough','08.jpg','08.pdf','','08.pdf',1,1,0,5);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Gift Items','09.jpg','','','',0,0,0,6);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Pretzel Rods','10.jpg','10.pdf','pretzel-rods','10.pdf',1,1,1,7);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('GA Saving Pass','11.jpg','','','',0,0,0,8);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('To Remember This','12.jpg','','','',0,0,0,9);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Scratchcards','13.jpg','13.pdf','scratchcards','',0,1,1,10);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Snifty','14.jpg','14.pdf','','',0,1,0,11);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Smencils','15.jpg','15.pdf','smencils','',0,1,1,12);
INSERT INTO fundraiser_product (name, image, sell_sheet_path, store_url, nutrition_information_sheet_path, has_nutrition_information_sheet, has_sell_sheet, is_purchasable, fundraiser_category_id) VALUES ('Magazines','16.jpg','','','',0,0,0,13);

PRINT 'Insert Partners Banners at the Bottom';
declare @bannerId INT;
insert into banner (created_on, image, url, alternative_text, is_active, partner_id, type) VALUES (GETDATE(), '0_lg_asa.jpg', '', 'ASA', 1, 0, 3);
SET @bannerId = @@identity;
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 3);
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 4); 
insert into banner (created_on, image, url, alternative_text, is_active, partner_id, type) VALUES (GETDATE(), '0_lg_envision.jpg', '', 'Envision', 1, 0, 3);
SET @bannerId = @@identity;
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 3);
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 4); 
insert into banner (created_on, image, url, alternative_text, is_active, partner_id, type) VALUES (GETDATE(), '0_lg_kappa.jpg', '', 'Kappa', 1, 0, 3);
SET @bannerId = @@identity;
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 3);
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 4); 
insert into banner (created_on, image, url, alternative_text, is_active, partner_id, type) VALUES (GETDATE(), '0_lg_llu.jpg', '', 'League Line Up', 1, 0, 3);
SET @bannerId = @@identity;
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 3);
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 4); 
insert into banner (created_on, image, url, alternative_text, is_active, partner_id, type) VALUES (GETDATE(), '0_lg_scouts.jpg', '', 'Scouts', 1, 0, 3);
SET @bannerId = @@identity;
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 3);
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 4); 
insert into banner (created_on, image, url, alternative_text, is_active, partner_id, type) VALUES (GETDATE(), '0_lg_usafootball.jpg', '', 'USA Football', 1, 0, 3);
SET @bannerId = @@identity;
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 3);
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 4); 
insert into banner (created_on, image, url, alternative_text, is_active, partner_id, type) VALUES (GETDATE(), '0_lg_usafundraising.jpg', '', 'USA Fundraising', 1, 0, 3);
SET @bannerId = @@identity;
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 3);
insert into banner_view_port (banner_id, view_port_id) VALUES (@bannerId, 4); 
PRINT 'SCRIPT END';

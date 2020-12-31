USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[AP_Cheque_Remit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AP_Cheque_Remit](
	[AP_Cheque_Remit_ID] [int] IDENTITY(1,1) NOT NULL,
	[AP_Cheque_ID] [int] NULL,
	[RemitBatchID] [int] NOT NULL,
	[RemitCode] [nvarchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[FulfillmentHouseID] [int] NOT NULL,
	[PublisherId] [int] NULL,
	[ProductSortName] [varchar](55) NOT NULL,
	[NetAmount] [numeric](14, 2) NOT NULL,
	[GSTAmount] [numeric](10, 2) NULL,
	[HSTAmount] [numeric](10, 2) NULL,
	[PSTAmount] [numeric](10, 2) NULL,
	[CurrencyCode] [varchar](3) NOT NULL,
	[Address1] [varchar](50) NOT NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[Province] [varchar](2) NOT NULL,
	[PostalCode] [varchar](10) NOT NULL,
	[CountryCode] [varchar](2) NOT NULL,
	[Comment] [varchar](150) NULL,
	[PostageAmount] [numeric](10, 2) NULL,
 CONSTRAINT [PK_AP_Cheque_Remit] PRIMARY KEY CLUSTERED 
(
	[AP_Cheque_Remit_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

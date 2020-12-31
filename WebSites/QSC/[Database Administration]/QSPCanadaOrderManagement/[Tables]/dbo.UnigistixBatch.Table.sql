USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[UnigistixBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnigistixBatch](
	[BatchDate] [datetime] NOT NULL,
	[BatchID] [int] NOT NULL,
	[OrderID] [int] NULL,
	[OrderTypeCode] [int] NULL,
	[DateOrderReceived] [datetime] NULL,
	[DateOrderCreated] [datetime] NULL,
	[BillToContactFirstName] [varchar](50) NULL,
	[BillToContactLastName] [varchar](50) NULL,
	[BillToContactPhone] [varchar](50) NULL,
	[Comment] [varchar](300) NULL,
	[CampaignID] [int] NULL,
	[Lang] [varchar](2) NULL,
	[FMID] [varchar](4) NULL,
	[FmFirstName] [varchar](50) NULL,
	[FmLastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[BillToAccountID] [int] NULL,
	[BillToAccountName] [varchar](50) NULL,
	[BillToAccountAddress1] [varchar](50) NULL,
	[BillToAccountAddress2] [varchar](50) NULL,
	[BillToAccountCity] [varchar](50) NULL,
	[BillToAccountState] [varchar](2) NULL,
	[BillToAccountZip] [varchar](12) NULL,
	[BillToAccountPhone] [varchar](50) NULL,
	[DateSentToUnigistix] [datetime] NULL,
	[FileName] [varchar](200) NULL,
	[ShipDate] [datetime] NULL,
	[CarrierID] [int] NULL,
	[CarrierName] [varchar](50) NULL,
	[NoOfBoxes] [int] NULL,
	[TotalWeight] [numeric](10, 2) NULL,
	[ShippingComment] [varchar](200) NULL,
	[ShipmentID] [int] NULL,
	[ShipToContactFirstName] [varchar](50) NULL,
	[ShipToContactLastName] [varchar](50) NULL,
	[ShipToContactPhone] [varchar](50) NULL,
	[ShipToAccountID] [int] NULL,
	[ShipToAccountName] [varchar](50) NULL,
	[ShipToAccountAddress1] [varchar](50) NULL,
	[ShipToAccountAddress2] [varchar](50) NULL,
	[ShipToAccountCity] [varchar](50) NULL,
	[ShipToAccountState] [varchar](2) NULL,
	[ShipToAccountZip] [varchar](12) NULL,
	[ShipToAccountPhone] [varchar](50) NULL,
 CONSTRAINT [PK_UnigistixBatch] PRIMARY KEY CLUSTERED 
(
	[BatchDate] ASC,
	[BatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

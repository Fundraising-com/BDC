USE [eFundweb]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sale](
	[Sales_ID] [int] NOT NULL,
	[Carrier_ID] [int] NULL,
	[Shipping_Option_ID] [int] NULL,
	[Payment_Term_ID] [int] NOT NULL,
	[Client_Sequence_Code] [varchar](4) NOT NULL,
	[Client_ID] [int] NOT NULL,
	[Sales_Status_ID] [int] NOT NULL,
	[Payment_Method_ID] [int] NOT NULL,
	[PO_Status_ID] [int] NULL,
	[PO_Number] [varchar](50) NULL,
	[Consultant_ID] [int] NOT NULL,
	[Sales_Date] [datetime] NOT NULL,
	[Shipping_Fees] [numeric](15, 4) NOT NULL,
	[Shipping_Fees_Discount] [numeric](15, 4) NULL,
	[Payment_Due_Date] [datetime] NULL,
	[Confirmed_Date] [datetime] NULL,
	[Scheduled_Delivery_Date] [datetime] NULL,
	[Scheduled_Ship_Date] [datetime] NULL,
	[Actual_Ship_Date] [datetime] NULL,
	[Waybill_No] [varchar](20) NULL,
	[Comment] [varchar](2000) NULL,
	[Coupon_Sheet_Assigned] [bit] NULL,
	[Production_Status_ID] [int] NULL,
	[Billing_Company_ID] [int] NULL,
	[Total_Amount] [numeric](15, 4) NULL,
	[AR_Status_ID] [int] NULL,
	[Invoice_date] [datetime] NULL,
	[Cancellation_date] [datetime] NULL,
	[Is_Ordered] [bit] NOT NULL,
	[PO_Received_On] [datetime] NULL,
	[Is_Delivered] [bit] NOT NULL,
	[Local_Sponsor_Found] [bit] NOT NULL,
	[Sponsor_Consultant_ID] [int] NULL,
	[AR_Consultant_ID] [int] NULL,
	[Box_Return_Date] [datetime] NULL,
	[Reship_Date] [datetime] NULL,
	[UpFront_Payment_Required] [numeric](10, 2) NULL,
	[UpFront_Payment_Due_Date] [datetime] NULL,
	[UpFront_Payment_Method_ID] [int] NULL,
	[Sponsor_Required] [bit] NOT NULL,
	[Actual_Delivery_Date] [datetime] NULL,
	[Accounting_Comments] [varchar](2000) NULL,
	[Lead_ID] [int] NULL,
	[SSN_Number] [char](9) NULL,
	[SSN_Address] [varchar](50) NULL,
	[SSN_City] [varchar](50) NULL,
	[SSN_State_Code] [varchar](10) NULL,
	[SSN_Country_Code] [varchar](10) NULL,
	[SSN_Zip_Code] [varchar](10) NULL,
	[Is_Validated] [bit] NULL,
 CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED 
(
	[Sales_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Sales_Stat__0F624AF8]  DEFAULT (1) FOR [Sales_Status_ID]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Shipping_F__10566F31]  DEFAULT (0) FOR [Shipping_Fees]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Shipping_F__114A936A]  DEFAULT (0) FOR [Shipping_Fees_Discount]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Coupon_She__123EB7A3]  DEFAULT (0) FOR [Coupon_Sheet_Assigned]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Production__1332DBDC]  DEFAULT (1) FOR [Production_Status_ID]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Total_Amou__14270015]  DEFAULT (0) FOR [Total_Amount]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Is_Ordered__151B244E]  DEFAULT (0) FOR [Is_Ordered]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Is_Deliver__160F4887]  DEFAULT (0) FOR [Is_Delivered]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Local_Spon__17036CC0]  DEFAULT (0) FOR [Local_Sponsor_Found]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Sponsor_Re__17F790F9]  DEFAULT (1) FOR [Sponsor_Required]
GO
ALTER TABLE [dbo].[Sale] ADD  CONSTRAINT [DF__Sale__Is_Validat__18EBB532]  DEFAULT (0) FOR [Is_Validated]
GO

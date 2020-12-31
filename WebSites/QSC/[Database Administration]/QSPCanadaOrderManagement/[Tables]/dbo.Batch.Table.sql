USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Batch](
	[Date] [datetime] NOT NULL,
	[ID] [int] NOT NULL,
	[AccountID] [int] NULL,
	[EnterredCount] [int] NULL,
	[EnterredAmount] [numeric](10, 2) NULL,
	[CalculatedAmount] [numeric](10, 2) NULL,
	[StatusInstance] [int] NULL,
	[KE3FileName] [varchar](200) NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NULL,
	[TeacherCount] [int] NULL,
	[StudentCount] [int] NULL,
	[CustomerCount] [int] NULL,
	[OrderCount] [int] NULL,
	[OrderCountAccept] [int] NULL,
	[OrderDetailCount] [int] NULL,
	[OrderDetailCountError] [int] NULL,
	[StartImportTime] [datetime] NULL,
	[EndImportTime] [datetime] NULL,
	[ImportTimeSeconds] [int] NULL,
	[Clerk] [varchar](4) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateKeyed] [datetime] NULL,
	[DateBatchCompleted] [datetime] NULL,
	[OverridePctState] [bit] NULL,
	[PctState] [numeric](10, 2) NULL,
	[OriginalStatusInstance] [int] NULL,
	[OrderTypeCode] [int] NULL,
	[CampaignID] [int] NULL,
	[BillToAddressID] [int] NULL,
	[ShipToAddressID] [int] NULL,
	[ShipToAccountID] [int] NULL,
	[BillToFMID] [varchar](4) NULL,
	[ShipToFMID] [varchar](4) NULL,
	[ReportedEnvelopes] [int] NULL,
	[PaymentSend] [numeric](10, 2) NULL,
	[SalesBeforeTax] [numeric](10, 2) NULL,
	[DateSent] [datetime] NULL,
	[DateReceived] [datetime] NULL,
	[ContactFirstName] [varchar](50) NULL,
	[ContactLastName] [varchar](50) NULL,
	[ContactEmail] [varchar](50) NULL,
	[ContactPhone] [varchar](50) NULL,
	[Comment] [varchar](300) NULL,
	[IncentiveCalculationStatus] [int] NULL,
	[MagnetBookletCount] [int] NULL,
	[MagnetCardCount] [int] NULL,
	[MagnetGoodCardCount] [int] NULL,
	[MagnetCardsMailed] [int] NULL,
	[MagnetMailDate] [datetime] NULL,
	[PickDate] [datetime] NULL,
	[IsDMApproved] [bit] NULL,
	[CountryCode] [varchar](10) NULL,
	[PickLine] [int] NULL,
	[OrderQualifierID] [int] NULL,
	[CheckPayableToQSPAmount] [numeric](10, 2) NULL,
	[IsIncentive] [bit] NULL,
	[OrderDeliveryDate] [datetime] NULL,
	[RefNumber] [int] NULL,
	[PaymentBatchDate] [datetime] NULL,
	[PaymentBatchID] [int] NULL,
	[IsStaffOrder] [bit] NULL,
	[InquireUponComplete] [bit] NULL,
	[GroupProfit] [numeric](10, 2) NULL,
	[OrderID] [int] NOT NULL,
	[OrderAmntDue] [numeric](10, 2) NULL,
	[MagnetPostage] [numeric](10, 2) NULL,
	[OrderIDIncentive] [int] NULL,
	[IsInvoiced] [bit] NULL,
	[CampaignNetTotal] [numeric](10, 2) NULL,
	[DistributionCenterID] [int] NULL,
	[IsMagQueueDone] [int] NULL,
	[ProblemID] [int] NULL,
	[CheckDate] [datetime] NULL,
	[CheckNumber] [int] NULL,
	[MagnetUnitPostage] [numeric](10, 2) NULL,
	[PostageAdjustmentID] [int] NULL,
	[SentToSAP] [datetime] NULL,
 CONSTRAINT [PK_Batch] PRIMARY KEY CLUSTERED 
(
	[Date] ASC,
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Batch] ADD  CONSTRAINT [DF_Batch_OrderID]  DEFAULT (1) FOR [OrderID]
GO
ALTER TABLE [dbo].[Batch] ADD  CONSTRAINT [DF_Batch_DistributionCenterID]  DEFAULT (0) FOR [DistributionCenterID]
GO

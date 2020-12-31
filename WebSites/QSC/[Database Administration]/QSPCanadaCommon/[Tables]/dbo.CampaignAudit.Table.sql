USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CampaignAudit]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CampaignAudit](
	[AuditDate] [datetime] NOT NULL,
	[ID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Renewal] [bit] NULL,
	[Country] [varchar](50) NOT NULL,
	[FMID] [varchar](4) NOT NULL,
	[DateChanged] [varchar](50) NOT NULL,
	[Lang] [varchar](10) NOT NULL,
	[EndDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[IncentivesBillToID] [int] NULL,
	[BillToAccountID] [int] NOT NULL,
	[ShipToCampaignContactID] [int] NULL,
	[ShipToAccountID] [int] NULL,
	[EstimatedGross] [numeric](10, 2) NULL,
	[NumberOfParticipants] [int] NULL,
	[NumberOfClassroooms] [int] NULL,
	[NumberOfStaff] [int] NULL,
	[BillToCampaignContactID] [int] NULL,
	[SuppliesCampaignContactID] [int] NULL,
	[SuppliesShipToCampaignContactID] [int] NULL,
	[SuppliesDeliveryDate] [datetime] NULL,
	[SpecialInstructions] [varchar](1000) NULL,
	[IsStaffOrder] [bit] NULL,
	[StaffOrderDiscount] [numeric](10, 2) NULL,
	[IsTestCampaign] [bit] NULL,
	[DateModified] [datetime] NULL,
	[UserIDModified] [dbo].[UserID_UDDT] NULL,
	[IsPayLater] [bit] NULL,
	[IncentivesDistributionID] [int] NULL,
	[FSRequired] [bit] NULL,
	[FSExtraRequired] [bit] NULL,
	[FSOrderRecCreated] [bit] NULL,
	[ApprovedStatusDate] [datetime] NULL,
	[MagnetStatementDate] [datetime] NULL,
	[RewardsProgramCumulative] [bit] NULL,
	[RewardsProgramChart] [bit] NULL,
	[RewardsProgramDraw] [bit] NULL,
	[ContactName] [varchar](50) NULL,
	[PhoneListID] [int] NULL,
	[SuppliesAddressID] [int] NOT NULL,
	[DateSubmitted] [datetime] NULL,
	[Extra_1Ups] [int] NULL,
	[Extra_GiftForm] [int] NULL,
	[OnlineOnlyPrograms] [bit] NOT NULL,
	[ForceStatementPrint] [bit] NULL,
	[DisableStatementPrint] [bit] NULL,
	[Extra_MagBrochure] [int] NULL,
 CONSTRAINT [PK_CampaignAudit] PRIMARY KEY CLUSTERED 
(
	[AuditDate] ASC,
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CampaignAudit] ADD  CONSTRAINT [DF_CampaignAudit_OnlineOnlyPrograms]  DEFAULT (0) FOR [OnlineOnlyPrograms]
GO

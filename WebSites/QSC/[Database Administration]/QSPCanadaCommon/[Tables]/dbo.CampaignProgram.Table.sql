USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CampaignProgram]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CampaignProgram](
	[CampaignID] [int] NOT NULL,
	[ProgramID] [int] NOT NULL,
	[IsPreCollect] [varchar](1) NULL,
	[GroupProfit] [numeric](10, 2) NULL,
	[DeletedTF] [bit] NOT NULL,
	[IsPersonalize] [bit] NOT NULL,
	[BlackboardPacket] [bit] NULL,
	[FieldSupplyPacket] [bit] NULL,
	[OnlineOnly] [bit] NOT NULL,
	[AllowOnlineAccountDelivery] [bit] NOT NULL,
 CONSTRAINT [PK_CampaignProgram] PRIMARY KEY CLUSTERED 
(
	[CampaignID] ASC,
	[ProgramID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CampaignProgram] ADD  CONSTRAINT [DF_CampaignProgram_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
ALTER TABLE [dbo].[CampaignProgram] ADD  CONSTRAINT [DF__CampaignP__IsPer__73FA27A5]  DEFAULT (0) FOR [IsPersonalize]
GO
ALTER TABLE [dbo].[CampaignProgram] ADD  DEFAULT ((0)) FOR [OnlineOnly]
GO
ALTER TABLE [dbo].[CampaignProgram] ADD  DEFAULT ((0)) FOR [AllowOnlineAccountDelivery]
GO

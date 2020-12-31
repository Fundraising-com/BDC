USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CampaignCommissionSplit]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CampaignCommissionSplit](
	[CampaignCommissionSplitID] [int] IDENTITY(1,1) NOT NULL,
	[CampaignID] [int] NOT NULL,
	[FMID] [varchar](4) NOT NULL,
	[CommissionPercentage] [numeric](5, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[EffectiveToDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CampaignCommissionSplit] PRIMARY KEY CLUSTERED 
(
	[CampaignCommissionSplitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CampaignCommissionSplit]  WITH CHECK ADD  CONSTRAINT [FK_CampaignCommissionSplit_Campaign] FOREIGN KEY([CampaignID])
REFERENCES [dbo].[Campaign] ([ID])
GO
ALTER TABLE [dbo].[CampaignCommissionSplit] CHECK CONSTRAINT [FK_CampaignCommissionSplit_Campaign]
GO
ALTER TABLE [dbo].[CampaignCommissionSplit]  WITH CHECK ADD  CONSTRAINT [FK_CampaignCommissionSplit_FieldManager] FOREIGN KEY([FMID])
REFERENCES [dbo].[FieldManager] ([FMID])
GO
ALTER TABLE [dbo].[CampaignCommissionSplit] CHECK CONSTRAINT [FK_CampaignCommissionSplit_FieldManager]
GO

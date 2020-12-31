USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CampaignToContentCatalog]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CampaignToContentCatalog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignId] [int] NOT NULL,
	[Content_Catalog_Code] [varchar](50) NOT NULL,
	[ProgramId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedTF] [bit] NULL,
	[ProgramContentCatalogCodeLookupID] [int] NULL,
 CONSTRAINT [PK_CampaignToContentCatalog] PRIMARY KEY CLUSTERED 
(
	[CampaignId] ASC,
	[Content_Catalog_Code] ASC,
	[ProgramId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CampaignToContentCatalog] ADD  CONSTRAINT [DF_CampaignToContentCatalog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[CampaignToContentCatalog] ADD  CONSTRAINT [DF_CampaignToContentCatalog_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[CampaignToContentCatalog] ADD  CONSTRAINT [DF_CampaignToContentCatalog_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO

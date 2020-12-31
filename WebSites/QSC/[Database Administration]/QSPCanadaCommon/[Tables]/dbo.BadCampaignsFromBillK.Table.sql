USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[BadCampaignsFromBillK]    Script Date: 06/07/2017 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BadCampaignsFromBillK](
	[IntOrderID] [int] NULL,
	[OrgId] [int] NULL,
	[BadCampId] [int] NULL,
	[CorrectCampId] [int] NULL,
	[PLorderID] [int] NULL,
	[fileno] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BadCampaignsFromBillK] ADD  CONSTRAINT [DF_BadCampaignsFromBillK_file]  DEFAULT (3) FOR [fileno]
GO

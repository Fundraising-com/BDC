USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[Brochure]    Script Date: 06/07/2017 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Brochure](
	[ProgramMasterCode] [varchar](20) NOT NULL,
	[ProgramSectionID] [int] NOT NULL,
	[CampaignID] [int] NOT NULL,
	[ProgramID] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedTF] [bit] NULL,
 CONSTRAINT [PK_Brochure] PRIMARY KEY CLUSTERED 
(
	[ProgramMasterCode] ASC,
	[ProgramSectionID] ASC,
	[CampaignID] ASC,
	[ProgramID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Brochure', @level2type=N'COLUMN',@level2name=N'ModifyDate'
GO
ALTER TABLE [dbo].[Brochure] ADD  CONSTRAINT [DF_Brochure_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO

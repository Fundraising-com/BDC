USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[FieldSupplySection]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FieldSupplySection](
	[ProgramMasterCode] [varchar](20) NOT NULL,
	[ProgramSectionID] [int] NOT NULL,
	[ContentProgramMasterCode] [varchar](20) NULL,
	[TaxRegionID] [int] NULL,
	[ApplicabilityID] [int] NULL,
	[DistributionLevelID] [int] NULL,
	[ProductCode] [varchar](20) NULL,
	[Price] [numeric](10, 2) NULL,
	[IsBrochure] [bit] NULL,
	[ExtraLimitRate] [numeric](10, 4) NULL,
 CONSTRAINT [PK_FieldSupplySection] PRIMARY KEY CLUSTERED 
(
	[ProgramMasterCode] ASC,
	[ProgramSectionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

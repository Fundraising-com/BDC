USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[EZTRAK_contract_export_TABLE]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EZTRAK_contract_export_TABLE](
	[Id] [int] NULL,
	[Name] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](2) NULL,
	[Zip] [varchar](6) NULL,
	[Zip4] [varchar](4) NULL,
	[Sponsor] [varchar](50) NULL,
	[FMID] [varchar](4) NULL,
	[ProgramStartDate] [datetime] NULL,
	[ProgramEndDate] [datetime] NULL,
	[FiscalYR] [int] NULL,
	[CampaignID] [int] NULL,
	[Country] [varchar](4) NULL,
	[Corp_Division] [tinyint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

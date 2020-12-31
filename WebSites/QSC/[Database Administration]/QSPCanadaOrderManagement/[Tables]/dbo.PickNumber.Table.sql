USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[PickNumber]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PickNumber](
	[PickNumber] [varchar](20) NULL,
	[PickLineNumber] [int] NULL,
	[DistributionCenterID] [int] NULL,
	[CatalogProductCode] [varchar](20) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

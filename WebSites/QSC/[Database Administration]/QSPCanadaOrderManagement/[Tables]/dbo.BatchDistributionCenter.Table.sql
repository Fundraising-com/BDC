USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[BatchDistributionCenter]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchDistributionCenter](
	[BatchDate] [datetime] NOT NULL,
	[BatchID] [int] NOT NULL,
	[DistributionCenterID] [int] NOT NULL,
	[StatusInstance] [int] NULL,
	[QSPProductLine] [int] NOT NULL,
 CONSTRAINT [PK_BatchDistributionCenter] PRIMARY KEY CLUSTERED 
(
	[BatchDate] ASC,
	[BatchID] ASC,
	[DistributionCenterID] ASC,
	[QSPProductLine] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

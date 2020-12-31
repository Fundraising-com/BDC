USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[PruneLog]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PruneLog](
	[Date] [datetime] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Instance] [int] NULL,
	[Reason] [varchar](50) NULL,
	[BatchDate] [datetime] NULL,
	[BatchID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

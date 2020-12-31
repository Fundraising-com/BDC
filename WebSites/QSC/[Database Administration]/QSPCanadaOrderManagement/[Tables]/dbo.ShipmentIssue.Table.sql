USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentIssue]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShipmentIssue](
	[d] [datetime] NULL,
	[productline] [varchar](512) NULL,
	[orderids] [varchar](512) NULL,
	[distcenter] [int] NULL,
	[sessionid] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

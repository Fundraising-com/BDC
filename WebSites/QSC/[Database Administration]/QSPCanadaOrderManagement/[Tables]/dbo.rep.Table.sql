USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[rep]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rep](
	[productcode] [varchar](20) NULL,
	[recipient] [varchar](81) NULL,
	[customerorderheaderinstance] [int] NOT NULL,
	[transid] [int] NOT NULL,
	[id] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

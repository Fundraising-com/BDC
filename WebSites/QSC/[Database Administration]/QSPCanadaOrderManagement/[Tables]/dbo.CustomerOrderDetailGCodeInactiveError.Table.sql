USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerOrderDetailGCodeInactiveError]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrderDetailGCodeInactiveError](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL
) ON [PRIMARY]
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ActionConstraints]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionConstraints](
	[Instance] [int] NOT NULL,
	[ActionInstanceToBePerformed] [int] NOT NULL,
	[CurrentStatus] [int] NOT NULL
) ON [PRIMARY]
GO

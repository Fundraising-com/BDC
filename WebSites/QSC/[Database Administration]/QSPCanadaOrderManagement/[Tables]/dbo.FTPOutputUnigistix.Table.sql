USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[FTPOutputUnigistix]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FTPOutputUnigistix](
	[ID] [int] NULL,
	[RunID] [int] NULL,
	[Date] [datetime] NULL,
	[Line] [nvarchar](4000) NULL
) ON [PRIMARY]
GO

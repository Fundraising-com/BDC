USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[SwitchLetterBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SwitchLetterBatch](
	[Instance] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [varchar](20) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserID] [varchar](4) NOT NULL,
	[Reason] [int] NOT NULL,
	[IsPrinted] [smallint] NULL,
	[DatePrinted] [datetime] NULL,
	[IsLocked] [smallint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

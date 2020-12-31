USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[SystemErrorLog]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemErrorLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorDate] [datetime] NULL,
	[OrderID] [int] NULL,
	[CampaignID] [int] NULL,
	[ProcName] [varchar](200) NULL,
	[Desc1] [varchar](500) NULL,
	[Desc2] [varchar](500) NULL,
	[IsReviewed] [int] NULL,
	[IsFixed] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

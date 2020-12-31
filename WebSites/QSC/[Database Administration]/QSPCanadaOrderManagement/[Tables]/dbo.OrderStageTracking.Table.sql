USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[OrderStageTracking]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderStageTracking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StageDate] [datetime] NULL,
	[CampaignID] [int] NULL,
	[OrderID] [int] NULL,
	[FMID] [varchar](4) NULL,
	[Stage] [int] NULL,
	[Scancount] [int] NULL,
	[TransmissionID] [int] NULL,
	[PDFFilename] [varchar](200) NULL,
	[BatchFilename] [varchar](200) NULL,
	[CampaignFilename] [varchar](200) NULL,
	[PDFAckTPL] [bit] NULL,
	[BatchAckTPL] [bit] NULL,
	[CampaignAckTPL] [bit] NULL,
	[GroupID] [int] NULL,
	[GroupName] [varchar](200) NULL,
	[FMName] [varchar](200) NULL,
	[ReceiptDate] [datetime] NULL,
	[ImageDate] [datetime] NULL,
	[DataCaptureDate] [datetime] NULL,
	[VerificationDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[TransmitDate] [datetime] NULL,
	[TransmissionSequence] [int] NULL,
	[TotalReceived] [int] NULL,
	[ResolveFileName] [varchar](100) NULL,
	[ResolveFileInError] [varchar](1) NULL,
	[PDFAckFileSize] [int] NULL,
	[BatchAckFileSize] [int] NULL,
	[CampaignAckFileSize] [int] NULL,
	[DateShipped] [datetime] NULL,
	[DateInvoiced] [datetime] NULL,
	[Units] [int] NULL,
	[ToteID] [int] NULL,
 CONSTRAINT [PK_OrderStageTracker] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

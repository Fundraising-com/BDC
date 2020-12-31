USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TrackingRegularInvoiceDate]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrackingRegularInvoiceDate](
	[ID] [int] NOT NULL,
	[AccountID] [int] NULL,
	[JulianBatchID] [int] NULL,
	[BatchDate] [datetime] NULL,
	[BatchID] [int] NULL,
	[InvoiceDate] [datetime] NULL,
	[InvoiceNumber] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateProcessed] [datetime] NULL,
 CONSTRAINT [PK_TrackingRegularInvoiceDate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[RemitBatchSummary]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RemitBatchSummary](
	[RemitDate] [datetime] NOT NULL,
	[NumberOfSubs] [int] NULL,
	[NumberOfTitleCodes] [int] NULL,
	[NumberOfFulfillmentHouses] [int] NULL,
	[GrossAmount] [numeric](10, 2) NULL,
	[Currency] [varchar](3) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](50) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](50) NULL,
 CONSTRAINT [PK_RemitBatchSummary] PRIMARY KEY CLUSTERED 
(
	[RemitDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

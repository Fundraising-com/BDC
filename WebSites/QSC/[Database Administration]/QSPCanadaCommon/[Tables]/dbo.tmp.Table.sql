USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[tmp]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tmp](
	[AccountId] [int] NOT NULL,
	[AccountName] [varchar](50) NULL,
	[InvoiceDate] [datetime] NULL,
	[InvoiceNumber] [int] NULL,
	[StudentInstance] [int] NOT NULL,
	[SF] [varchar](50) NULL,
	[SL] [varchar](50) NULL,
	[CustomerBillToInstance] [int] NOT NULL,
	[CF] [varchar](50) NULL,
	[CL] [varchar](50) NULL,
	[ProductCode] [varchar](4) NULL,
	[AlphaProductCode] [varchar](4) NULL,
	[ProductName] [varchar](30) NULL,
	[FY] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

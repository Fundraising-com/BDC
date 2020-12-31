USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[MarketingCodes]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MarketingCodes](
	[Instance] [int] NOT NULL,
	[MarketingCode] [varchar](3) NULL,
	[Description] [varchar](100) NULL,
	[FiscalYear] [int] NULL,
	[Season] [char](1) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
	[QuantityLimit] [int] NULL,
	[QuantityToDate] [int] NULL,
	[StartToteCollectionDate] [datetime] NULL,
	[EndToteCollectionDate] [datetime] NULL,
	[MinAccountAmount] [int] NULL,
	[MaxAccountAmount] [int] NULL,
	[MaxMailDate] [datetime] NULL,
	[SchoolType] [char](2) NULL,
	[OrderWithinMailDate] [int] NULL,
	[SchoolType2] [varchar](50) NULL,
 CONSTRAINT [PK_MarketingCodes] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

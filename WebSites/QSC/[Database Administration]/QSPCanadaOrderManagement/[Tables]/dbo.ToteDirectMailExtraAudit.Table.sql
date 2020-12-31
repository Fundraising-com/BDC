USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteDirectMailExtraAudit]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ToteDirectMailExtraAudit](
	[ToteInstance] [int] NOT NULL,
	[Booklets] [int] NOT NULL,
	[FamilyEnvelopes] [int] NOT NULL,
	[DateReported] [datetime] NULL,
	[MarketingCodesInstance] [int] NULL,
	[UserID] [char](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[CouponEstimate] [decimal](10, 2) NULL,
 CONSTRAINT [PK_ToteDirectMailExtraAudit] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC,
	[DateChanged] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

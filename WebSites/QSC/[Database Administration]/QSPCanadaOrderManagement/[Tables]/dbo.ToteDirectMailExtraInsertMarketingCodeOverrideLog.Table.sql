USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteDirectMailExtraInsertMarketingCodeOverrideLog]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ToteDirectMailExtraInsertMarketingCodeOverrideLog](
	[ToteInstance] [int] NOT NULL,
	[OriginalMarketingCodeInstance] [int] NULL,
	[DateCreated] [datetime] NULL,
	[UserID] [varchar](4) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

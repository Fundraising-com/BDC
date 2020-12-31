USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CodeDetailTranslation]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CodeDetailTranslation](
	[CodeDetailInstance] [int] NOT NULL,
	[Lang] [varchar](10) NULL,
	[Description] [varchar](64) NULL,
 CONSTRAINT [PK_CodeDetailTranslation] PRIMARY KEY CLUSTERED 
(
	[CodeDetailInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

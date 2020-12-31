USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CodeDetail]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CodeDetail](
	[Instance] [int] NOT NULL,
	[CodeHeaderInstance] [int] NOT NULL,
	[Description] [varchar](64) NULL,
	[Gross] [bit] NOT NULL,
	[ADPCode] [varchar](6) NULL,
 CONSTRAINT [PK_CodeDetail] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

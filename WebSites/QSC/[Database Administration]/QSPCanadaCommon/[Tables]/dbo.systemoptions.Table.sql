USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[systemoptions]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[systemoptions](
	[KeyValue] [varchar](50) NOT NULL,
	[DescValue] [varchar](50) NULL,
	[TextValue] [varchar](200) NULL,
	[DoubleValue] [float] NOT NULL,
	[Long1Value] [int] NOT NULL,
	[Long2Value] [int] NOT NULL,
	[TextPrompt] [varchar](50) NULL,
	[DoublePrompt] [varchar](50) NULL,
	[Long1Prompt] [varchar](50) NULL,
	[Long2Prompt] [varchar](50) NULL,
	[CodeHeaderInstance] [int] NOT NULL,
 CONSTRAINT [PK_systemoptions] PRIMARY KEY CLUSTERED 
(
	[KeyValue] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

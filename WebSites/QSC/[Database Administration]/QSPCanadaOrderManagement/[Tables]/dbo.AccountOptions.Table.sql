USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[AccountOptions]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountOptions](
	[AccountID] [int] NOT NULL,
	[KeyValue] [varchar](50) NOT NULL,
	[TextValue] [varchar](200) NULL,
	[DoubleValue] [float] NOT NULL,
	[Long1Value] [int] NOT NULL,
	[Long2Value] [int] NOT NULL,
 CONSTRAINT [PK_AccountOptions] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC,
	[KeyValue] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

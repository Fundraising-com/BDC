USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[test_table]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[test_table](
	[test_int] [int] NOT NULL,
	[test_varchar] [varchar](50) NULL,
 CONSTRAINT [PK_test_table] PRIMARY KEY CLUSTERED 
(
	[test_int] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

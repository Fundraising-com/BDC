USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[mke_replication_test]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mke_replication_test](
	[test] [int] NOT NULL,
	[test_value] [varchar](50) NOT NULL,
 CONSTRAINT [PK_mke_replication_test] PRIMARY KEY CLUSTERED 
(
	[test] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

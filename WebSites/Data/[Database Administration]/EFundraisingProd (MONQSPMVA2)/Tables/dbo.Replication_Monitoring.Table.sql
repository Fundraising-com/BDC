USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Replication_Monitoring]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Replication_Monitoring](
	[Replication_ID] [int] NOT NULL,
	[Msg] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Replication_Monitoring] PRIMARY KEY NONCLUSTERED 
(
	[Replication_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[rs_lastcommit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rs_lastcommit](
	[origin] [int] NOT NULL,
	[origin_qid] [binary](36) NULL,
	[secondary_qid] [binary](36) NULL,
	[origin_time] [datetime] NULL,
	[commit_time] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

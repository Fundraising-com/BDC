USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[waitstats]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[waitstats](
	[wait type] [varchar](80) NULL,
	[requests] [numeric](20, 1) NULL,
	[wait time] [numeric](20, 1) NULL,
	[signal wait time] [numeric](20, 1) NULL,
	[now] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[waitstats] ADD  DEFAULT (getdate()) FOR [now]
GO

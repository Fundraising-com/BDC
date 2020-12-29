USE [fastfundraising]
GO
/****** Object:  Table [dbo].[tbl_samplerequest]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_samplerequest](
	[orgname] [varchar](100) NULL,
	[contactname] [varchar](50) NULL,
	[address] [varchar](100) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[zip] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
	[emailaddr] [varchar](255) NULL,
	[grouptype] [varchar](50) NULL,
	[numberofsellers] [varchar](50) NULL,
	[desiredprofit] [varchar](50) NULL,
	[desiredprofit2] [varchar](50) NULL,
	[notes] [varchar](8000) NULL,
	[processedby] [varchar](50) NULL,
	[processeddatetime] [datetime] NULL,
	[requestdatetime] [datetime] NULL,
	[requeststring] [varchar](50) NULL,
	[cookieid] [int] NULL,
	[besttimetocall] [varchar](50) NULL,
	[batchid] [int] NULL,
	[hearaboutus] [varchar](100) NULL,
	[callyesno] [int] NULL,
	[srid] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

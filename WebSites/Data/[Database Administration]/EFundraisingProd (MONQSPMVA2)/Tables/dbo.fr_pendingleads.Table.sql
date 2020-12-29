USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[fr_pendingleads]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fr_pendingleads](
	[id] [int] NOT NULL,
	[custname] [nvarchar](255) NULL,
	[cntcprsn] [nvarchar](255) NULL,
	[Address1] [nvarchar](255) NULL,
	[city] [nvarchar](255) NULL,
	[state] [nvarchar](255) NULL,
	[zip] [float] NULL,
	[phone1] [nvarchar](255) NULL,
	[emailaddress1] [nvarchar](255) NULL,
	[besttimetocall] [nvarchar](255) NULL,
	[GroupType] [nvarchar](255) NULL,
	[TargetProfit] [nvarchar](255) NULL,
	[NumberOfSellers] [float] NULL,
	[Comments] [nvarchar](255) NULL,
	[Requestdate] [smalldatetime] NULL,
	[SampleRequestDetails] [nvarchar](255) NULL,
	[SourceCode] [nvarchar](255) NULL,
	[createdate] [smalldatetime] NULL,
	[processeddate] [nvarchar](255) NULL,
	[processedby] [nvarchar](255) NULL,
	[rowid] [float] NULL,
	[adoptedby] [nvarchar](255) NULL,
	[adoptedDate] [nvarchar](255) NULL,
	[category] [nvarchar](255) NULL,
	[salesid] [float] NULL,
	[dnotes] [nvarchar](255) NULL,
	[callbackdatetime] [nvarchar](255) NULL,
	[salescategory] [nvarchar](255) NULL,
	[hearaboutus] [nvarchar](255) NULL,
	[callyesno] [float] NULL,
	[done] [bit] NOT NULL,
	[remove] [bit] NOT NULL
) ON [PRIMARY]
GO

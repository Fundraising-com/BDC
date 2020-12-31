USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[BatchSummary]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BatchSummary](
	[Date] [datetime] NOT NULL,
	[AccountID] [int] NOT NULL,
	[Actual] [int] NULL,
	[Estimated] [int] NULL,
	[Highest] [int] NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
	[Lowest] [int] NOT NULL,
 CONSTRAINT [PK_BatchSummary] PRIMARY KEY CLUSTERED 
(
	[Date] ASC,
	[AccountID] ASC,
	[Highest] ASC,
	[Lowest] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

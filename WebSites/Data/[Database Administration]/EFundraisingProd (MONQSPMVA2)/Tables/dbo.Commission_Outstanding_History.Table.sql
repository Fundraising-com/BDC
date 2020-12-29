USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Commission_Outstanding_History]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Commission_Outstanding_History](
	[Sales_ID] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Consultant_ID] [int] NULL,
	[Sales_Date] [datetime] NULL,
	[Shipped_Date] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[Payment_Term] [varchar](50) NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Organization] [varchar](100) NULL,
	[Day_Phone] [varchar](20) NULL,
	[Outstanding_Amount] [varchar](125) NULL,
	[Currency_Code] [varchar](10) NULL,
	[Outstanding_Commission] [varchar](125) NULL,
 CONSTRAINT [PK_Commission_Outstanding_History] PRIMARY KEY NONCLUSTERED 
(
	[Sales_ID] ASC,
	[Month] ASC,
	[Year] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

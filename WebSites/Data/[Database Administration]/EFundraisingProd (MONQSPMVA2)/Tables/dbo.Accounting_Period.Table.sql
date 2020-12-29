USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Accounting_Period]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounting_Period](
	[Closing_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Accounting_Period] PRIMARY KEY CLUSTERED 
(
	[Closing_Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

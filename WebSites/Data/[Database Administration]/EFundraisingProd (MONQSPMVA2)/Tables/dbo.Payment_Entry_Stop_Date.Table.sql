USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Payment_Entry_Stop_Date]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment_Entry_Stop_Date](
	[Payment_Entry_Stop_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment_Entry_Stop_Date] PRIMARY KEY CLUSTERED 
(
	[Payment_Entry_Stop_Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

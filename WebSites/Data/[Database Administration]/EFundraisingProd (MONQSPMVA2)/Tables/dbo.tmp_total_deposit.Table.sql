USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[tmp_total_deposit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tmp_total_deposit](
	[Sales_ID] [int] NOT NULL,
	[Total_Deposit] [decimal](38, 4) NULL
) ON [PRIMARY]
GO

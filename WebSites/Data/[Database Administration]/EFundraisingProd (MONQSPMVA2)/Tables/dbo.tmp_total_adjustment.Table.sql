USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[tmp_total_adjustment]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tmp_total_adjustment](
	[Sales_ID] [int] NOT NULL,
	[Adjustment_Amount] [numeric](15, 4) NULL
) ON [PRIMARY]
GO

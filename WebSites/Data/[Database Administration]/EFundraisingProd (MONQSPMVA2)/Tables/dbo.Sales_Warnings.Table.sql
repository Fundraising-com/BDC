USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Sales_Warnings]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_Warnings](
	[Sales_ID] [int] NULL,
	[Sales_Item_No] [int] NULL,
	[Sales_Constraint_Id] [int] NULL
) ON [PRIMARY]
GO

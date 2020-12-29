USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Inventory_Adjustment_Type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Inventory_Adjustment_Type](
	[Inventory_Adjustment_Type_ID] [int] IDENTITY(4,1) NOT FOR REPLICATION NOT NULL,
	[Inventory_Adjustment_Type_Desc] [varchar](255) NULL,
 CONSTRAINT [PK_Inventory_Adjustment_Type] PRIMARY KEY NONCLUSTERED 
(
	[Inventory_Adjustment_Type_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Local_Sponsor_Sales_Item]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Local_Sponsor_Sales_Item](
	[Brand_ID] [int] NOT NULL,
	[Local_Sponsor_ID] [int] NOT NULL,
	[Sales_ID] [int] NOT NULL,
	[Sales_Item_No] [smallint] NOT NULL,
 CONSTRAINT [PK_Local_Sponsor_Sales_Item] PRIMARY KEY NONCLUSTERED 
(
	[Brand_ID] ASC,
	[Local_Sponsor_ID] ASC,
	[Sales_ID] ASC,
	[Sales_Item_No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

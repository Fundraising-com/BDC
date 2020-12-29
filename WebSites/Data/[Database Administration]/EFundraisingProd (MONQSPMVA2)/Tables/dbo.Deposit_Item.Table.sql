USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Deposit_Item]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deposit_Item](
	[Deposit_ID] [int] NOT NULL,
	[Sales_ID] [int] NOT NULL,
	[Paiement_No] [int] NOT NULL,
 CONSTRAINT [PK_Deposit_Item] PRIMARY KEY NONCLUSTERED 
(
	[Deposit_ID] ASC,
	[Sales_ID] ASC,
	[Paiement_No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Deposit_Item]  WITH CHECK ADD  CONSTRAINT [FK_deposit_item_deposit] FOREIGN KEY([Deposit_ID])
REFERENCES [dbo].[deposit] ([deposit_id])
GO
ALTER TABLE [dbo].[Deposit_Item] CHECK CONSTRAINT [FK_deposit_item_deposit]
GO
ALTER TABLE [dbo].[Deposit_Item] ADD  CONSTRAINT [DF_Deposit_Item_Deposit_ID]  DEFAULT (0) FOR [Deposit_ID]
GO
ALTER TABLE [dbo].[Deposit_Item] ADD  CONSTRAINT [DF_Deposit_Item_Sales_ID]  DEFAULT (0) FOR [Sales_ID]
GO
ALTER TABLE [dbo].[Deposit_Item] ADD  CONSTRAINT [DF_Deposit_Item_Paiement_No]  DEFAULT (0) FOR [Paiement_No]
GO

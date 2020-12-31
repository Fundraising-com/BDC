USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[LetterBatchCustomerOrderDetail]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LetterBatchCustomerOrderDetail](
	[LetterBatchID] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
 CONSTRAINT [PK_LetterBatchCustomerOrderDetail] PRIMARY KEY NONCLUSTERED 
(
	[LetterBatchID] ASC,
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LetterBatchCustomerOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_LetterBatchCustomerOrderDetail_CustomerOrderDetail] FOREIGN KEY([CustomerOrderHeaderInstance], [TransID])
REFERENCES [dbo].[CustomerOrderDetail] ([CustomerOrderHeaderInstance], [TransID])
GO
ALTER TABLE [dbo].[LetterBatchCustomerOrderDetail] CHECK CONSTRAINT [FK_LetterBatchCustomerOrderDetail_CustomerOrderDetail]
GO
ALTER TABLE [dbo].[LetterBatchCustomerOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_LetterBatchCustomerOrderDetail_LetterBatch] FOREIGN KEY([LetterBatchID])
REFERENCES [dbo].[LetterBatch] ([ID])
GO
ALTER TABLE [dbo].[LetterBatchCustomerOrderDetail] CHECK CONSTRAINT [FK_LetterBatchCustomerOrderDetail_LetterBatch]
GO

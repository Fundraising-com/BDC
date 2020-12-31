USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerPaymentDetail]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPaymentDetail](
	[CustomerPaymentHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[CustomerOrderDetailTransID] [int] NOT NULL,
	[Amount] [float] NOT NULL,
 CONSTRAINT [aaaaaCustomerPaymentDetail_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerPaymentHeaderInstance] ASC,
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerPaymentDetail] ADD  CONSTRAINT [DF__CustomerP__Custo__760C8D49]  DEFAULT (0) FOR [CustomerPaymentHeaderInstance]
GO
ALTER TABLE [dbo].[CustomerPaymentDetail] ADD  CONSTRAINT [DF__CustomerP__Trans__7700B182]  DEFAULT (0) FOR [TransID]
GO
ALTER TABLE [dbo].[CustomerPaymentDetail] ADD  CONSTRAINT [DF__CustomerP__Custo__77F4D5BB]  DEFAULT (0) FOR [CustomerOrderDetailTransID]
GO
ALTER TABLE [dbo].[CustomerPaymentDetail] ADD  CONSTRAINT [DF__CustomerP__Amoun__78E8F9F4]  DEFAULT (0.0) FOR [Amount]
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerPaymentError]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPaymentError](
	[PaymentHeaderInstance] [int] NOT NULL,
	[ErrorInstance] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [aaaaaCustomerPaymentError_PK] PRIMARY KEY CLUSTERED 
(
	[PaymentHeaderInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerPaymentError] ADD  CONSTRAINT [DF__CustomerP__Payme__7CB98AD8]  DEFAULT (0) FOR [PaymentHeaderInstance]
GO
ALTER TABLE [dbo].[CustomerPaymentError] ADD  CONSTRAINT [DF__CustomerP__Error__7DADAF11]  DEFAULT (0) FOR [ErrorInstance]
GO
ALTER TABLE [dbo].[CustomerPaymentError] ADD  CONSTRAINT [DF__CustomerPa__Date__7EA1D34A]  DEFAULT ('1/1/1995') FOR [Date]
GO

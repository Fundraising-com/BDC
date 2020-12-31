USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ResolveCreditCardRefund]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResolveCreditCardRefund](
	[Price] [numeric](38, 2) NULL,
	[CreditCardNumber] [varchar](25) NULL,
	[ExpirationMonth] [varchar](2) NULL,
	[ExpirationYear] [varchar](2) NULL,
	[PaymentMethodInstance] [int] NULL,
	[FirstName] [varchar](40) NULL,
	[LastName] [varchar](40) NULL,
	[SubsCount] [int] NULL,
	[RefundStatus] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

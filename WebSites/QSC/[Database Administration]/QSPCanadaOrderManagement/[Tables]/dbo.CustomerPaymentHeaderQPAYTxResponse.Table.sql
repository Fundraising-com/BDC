USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerPaymentHeaderQPAYTxResponse]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPaymentHeaderQPAYTxResponse](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerPaymentHeaderInstance] [int] NULL,
	[BPPSTxID] [int] NULL
) ON [PRIMARY]
GO

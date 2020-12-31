USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[NonBatchCreditCardPayment]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NonBatchCreditCardPayment](
	[CustomerPaymentHeaderInstance] [int] NOT NULL,
	[CreditCardNumber] [varchar](25) NULL,
	[ExpirationDate] [varchar](10) NULL,
	[ReasonCode] [int] NOT NULL,
	[AuthorizationSource] [varchar](2) NULL,
	[AuthorizationCode] [varchar](20) NULL,
	[AuthorizationDate] [datetime] NOT NULL,
	[AVSResponseCode] [varchar](2) NULL,
	[StatusInstance] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[VeriSignID] [varchar](36) NULL,
	[IsGLDone] [int] NULL,
 CONSTRAINT [PK_NbCreditCardPayment_1] PRIMARY KEY NONCLUSTERED 
(
	[CustomerPaymentHeaderInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

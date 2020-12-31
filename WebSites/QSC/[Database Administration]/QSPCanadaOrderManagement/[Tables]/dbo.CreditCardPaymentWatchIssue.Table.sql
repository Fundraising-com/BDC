USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CreditCardPaymentWatchIssue]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CreditCardPaymentWatchIssue](
	[CustomerPaymentHeaderInstance] [int] NOT NULL,
	[CreditCardNumber] [varchar](25) NULL,
	[ExpirationDate] [varchar](10) NULL,
	[ReasonCode] [int] NOT NULL,
	[AuthorizationSource] [varchar](2) NULL,
	[AuthorizationCode] [varchar](6) NULL,
	[AuthorizationDate] [datetime] NOT NULL,
	[AVSResponseCode] [varchar](2) NULL,
	[StatusInstance] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[BatchID] [int] NOT NULL,
	[VeriSignID] [varchar](13) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

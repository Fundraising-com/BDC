USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[InvoiceGenerationLog]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InvoiceGenerationLog](
	[InvoiceGenLogId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[CamapaignId] [int] NOT NULL,
	[BatchStatus] [int] NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransId] [int] NOT NULL,
	[PricingDetailId] [int] NULL,
	[ProgramSectionId] [int] NULL,
	[TaxAmount] [numeric](10, 2) NULL,
	[PaymentMethodId] [int] NULL,
	[PaymentStatusId] [int] NULL,
	[PaymentHeaderStatusId] [int] NULL,
	[ProvinceCode] [varchar](10) NULL,
	[PaymentBatchId] [int] NULL,
	[CCPaymentBatchId] [int] NULL,
	[CPHInstance] [int] NULL,
	[CPHTotal] [numeric](10, 2) NULL,
	[TotalPaidByCC] [numeric](10, 2) NULL,
	[InvoiceGenErrorCode] [int] NOT NULL,
	[InvoiceGenErrorMessage] [varchar](200) NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[IsFixed] [int] NOT NULL,
	[DateFixed] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[InvoiceGenerationLog] ADD  CONSTRAINT [DF__invoicege__IsFix__338A9CD5]  DEFAULT (0) FOR [IsFixed]
GO

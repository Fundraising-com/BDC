USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementPayment]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatementPayment](
	[StatementPaymentID] [int] IDENTITY(1,1) NOT NULL,
	[StatementID] [int] NOT NULL,
	[PaymentID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StatementPayment] PRIMARY KEY CLUSTERED 
(
	[StatementPaymentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StatementPayment]  WITH CHECK ADD  CONSTRAINT [FK_StatementPayment_Payment] FOREIGN KEY([PaymentID])
REFERENCES [dbo].[PAYMENT] ([PAYMENT_ID])
GO
ALTER TABLE [dbo].[StatementPayment] CHECK CONSTRAINT [FK_StatementPayment_Payment]
GO
ALTER TABLE [dbo].[StatementPayment]  WITH CHECK ADD  CONSTRAINT [FK_StatementPayment_Statement] FOREIGN KEY([StatementID])
REFERENCES [dbo].[Statement] ([StatementID])
GO
ALTER TABLE [dbo].[StatementPayment] CHECK CONSTRAINT [FK_StatementPayment_Statement]
GO
ALTER TABLE [dbo].[StatementPayment] ADD  CONSTRAINT [DF_StatementPayment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

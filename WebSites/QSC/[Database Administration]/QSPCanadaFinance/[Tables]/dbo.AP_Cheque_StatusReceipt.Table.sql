USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[AP_Cheque_StatusReceipt]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_Cheque_StatusReceipt](
	[AP_Cheque_StatusReceipt_ID] [int] IDENTITY(1,1) NOT NULL,
	[AP_Cheque_StatusReceipt_Batch_ID] [int] NULL,
	[AP_Cheque_ID] [int] NOT NULL,
	[AP_Cheque_StatusReceipt_Status_ID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Amount] [numeric](12, 2) NOT NULL,
	[AP_Cheque_StatusReceipt_Type_ID] [int] NOT NULL,
	[PaidDate] [datetime] NULL,
 CONSTRAINT [PK_AP_Cheque_StatusReceipt] PRIMARY KEY CLUSTERED 
(
	[AP_Cheque_StatusReceipt_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt]  WITH CHECK ADD  CONSTRAINT [FK_AP_Cheque_StatusReceipt_AP_Cheque_StatusReceipt_Batch] FOREIGN KEY([AP_Cheque_StatusReceipt_Batch_ID])
REFERENCES [dbo].[AP_Cheque_StatusReceipt_Batch] ([AP_Cheque_StatusReceipt_Batch_ID])
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt] CHECK CONSTRAINT [FK_AP_Cheque_StatusReceipt_AP_Cheque_StatusReceipt_Batch]
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt]  WITH CHECK ADD  CONSTRAINT [FK_AP_Cheque_StatusReceipt_AP_Cheque_StatusReceipt_Status] FOREIGN KEY([AP_Cheque_StatusReceipt_Status_ID])
REFERENCES [dbo].[AP_Cheque_StatusReceipt_Status] ([AP_Cheque_StatusReceipt_Status_ID])
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt] CHECK CONSTRAINT [FK_AP_Cheque_StatusReceipt_AP_Cheque_StatusReceipt_Status]
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt]  WITH CHECK ADD  CONSTRAINT [FK_AP_Cheque_StatusReceipt_AP_Cheque_StatusReceipt_Type] FOREIGN KEY([AP_Cheque_StatusReceipt_Type_ID])
REFERENCES [dbo].[AP_Cheque_StatusReceipt_Type] ([AP_Cheque_StatusReceipt_Type_ID])
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt] CHECK CONSTRAINT [FK_AP_Cheque_StatusReceipt_AP_Cheque_StatusReceipt_Type]
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt] ADD  CONSTRAINT [DF_AP_Cheque_StatusReceipt_AP_Cheque_StatusReceipt_Status_ID]  DEFAULT ((1)) FOR [AP_Cheque_StatusReceipt_Status_ID]
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt] ADD  CONSTRAINT [DF_AP_Cheque_StatusReceipt_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

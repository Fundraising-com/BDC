USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[AP_Cheque_StatusReceipt_Batch]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_Cheque_StatusReceipt_Batch](
	[AP_Cheque_StatusReceipt_Batch_ID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_AP_Cheque_StatusReceipt_Batch] PRIMARY KEY CLUSTERED 
(
	[AP_Cheque_StatusReceipt_Batch_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AP_Cheque_StatusReceipt_Batch] ADD  CONSTRAINT [DF_AP_Cheque_StatusReceipt_Batch_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

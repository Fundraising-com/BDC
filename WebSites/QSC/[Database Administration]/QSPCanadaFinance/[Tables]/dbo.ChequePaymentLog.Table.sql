USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[ChequePaymentLog]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChequePaymentLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[DateProcess] [datetime] NOT NULL,
	[RecordCount] [int] NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[StartingPaymentId] [int] NULL,
	[ErrorMessage] [varchar](250) NULL,
	[Comment] [varchar](100) NULL,
 CONSTRAINT [PK_ChequePaymentLog] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ChequePaymentLog] ADD  CONSTRAINT [DF_ChequePaymentLog_RecordCount]  DEFAULT (0) FOR [RecordCount]
GO
ALTER TABLE [dbo].[ChequePaymentLog] ADD  CONSTRAINT [DF_ChequePaymentLog_TotalAmount]  DEFAULT (0) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[ChequePaymentLog] ADD  CONSTRAINT [DF_ChequePaymentLog_StartingPaymentId]  DEFAULT (0) FOR [StartingPaymentId]
GO

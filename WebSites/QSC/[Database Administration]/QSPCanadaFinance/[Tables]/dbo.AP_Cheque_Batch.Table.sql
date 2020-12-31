USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[AP_Cheque_Batch]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_Cheque_Batch](
	[AP_Cheque_Batch_ID] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[FileName] [nvarchar](100) NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AP_Cheque_Batch] PRIMARY KEY CLUSTERED 
(
	[AP_Cheque_Batch_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

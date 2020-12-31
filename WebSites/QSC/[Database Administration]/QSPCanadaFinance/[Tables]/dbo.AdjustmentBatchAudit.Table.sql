USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[AdjustmentBatchAudit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdjustmentBatchAudit](
	[AuditInstance] [int] IDENTITY(1,1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[ID] [int] NOT NULL,
	[AdjustmentTypeID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NOT NULL,
	[CreateUserID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ChangeUserID] [int] NULL,
	[ChangeDate] [datetime] NULL,
 CONSTRAINT [PK_AdjustmentBatchAudit] PRIMARY KEY CLUSTERED 
(
	[AuditInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

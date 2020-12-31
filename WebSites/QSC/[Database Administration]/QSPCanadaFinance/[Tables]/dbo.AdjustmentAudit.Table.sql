USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[AdjustmentAudit]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdjustmentAudit](
	[AuditInstance] [int] IDENTITY(1,1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[ADJUSTMENT_ID] [int] NOT NULL,
	[ACCOUNT_ID] [int] NOT NULL,
	[ACCOUNT_TYPE_ID] [int] NOT NULL,
	[ADJUSTMENT_TYPE_ID] [int] NOT NULL,
	[ADJUSTMENT_EFFECTIVE_DATE] [datetime] NOT NULL,
	[ADJUSTMENT_AMOUNT] [decimal](14, 6) NULL,
	[NOTE_TO_PRINT] [varchar](100) NULL,
	[DATE_CREATED] [datetime] NOT NULL,
	[DATETIME_MODIFIED] [datetime] NULL,
	[LAST_UPDATED_BY] [varchar](30) NULL,
	[COUNTRY_CODE] [varchar](10) NOT NULL,
	[INTERNAL_COMMENT] [varchar](100) NULL,
	[ORDER_ID] [int] NULL,
	[CAMPAIGN_ID] [int] NULL,
	[ADJUSTMENT_BATCH_ID] [int] NULL,
 CONSTRAINT [PK_AdjustmentAudit] PRIMARY KEY CLUSTERED 
(
	[AuditInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

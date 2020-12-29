USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[accounting_period_result]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounting_period_result](
	[accounting_period_result_id] [smallint] NOT NULL,
	[accounting_class_id] [tinyint] NULL,
	[period] [smalldatetime] NULL,
	[amount] [decimal](15, 4) NULL,
	[budgeted_amount] [decimal](15, 4) NULL,
 CONSTRAINT [PK_accounting_period_result] PRIMARY KEY CLUSTERED 
(
	[accounting_period_result_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[accounting_period_result]  WITH CHECK ADD  CONSTRAINT [FK_accounting_period_result_accounting_class] FOREIGN KEY([accounting_class_id])
REFERENCES [dbo].[accounting_class] ([accounting_class_id])
GO
ALTER TABLE [dbo].[accounting_period_result] CHECK CONSTRAINT [FK_accounting_period_result_accounting_class]
GO

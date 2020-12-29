USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[credit_check_request]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[credit_check_request](
	[credit_check_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[lead_id] [int] NOT NULL,
	[consultant_id] [int] NULL,
	[request_date] [datetime] NULL,
	[order_date] [datetime] NULL,
	[amount_requested] [money] NULL,
	[credit_status_id] [int] NULL,
	[credit_score] [int] NULL,
	[amount_approved] [money] NULL,
	[last_name] [varchar](50) NULL,
	[first_name] [varchar](50) NULL,
	[mid_init] [varchar](25) NULL,
	[address] [varchar](75) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[zip] [varchar](10) NULL,
	[ssn] [varchar](20) NULL,
	[result_date] [datetime] NULL,
	[result_confirmation_date] [datetime] NULL,
	[credit_report] [text] NULL,
 CONSTRAINT [PK_credit_check_request] PRIMARY KEY CLUSTERED 
(
	[credit_check_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'request by FC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'credit_check_request', @level2type=N'COLUMN',@level2name=N'request_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'request sent to experian' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'credit_check_request', @level2type=N'COLUMN',@level2name=N'order_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'amount requested by FC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'credit_check_request', @level2type=N'COLUMN',@level2name=N'amount_requested'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'score received from experian' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'credit_check_request', @level2type=N'COLUMN',@level2name=N'credit_score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount Confirmed by AR after results' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'credit_check_request', @level2type=N'COLUMN',@level2name=N'amount_approved'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'request result received from experian' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'credit_check_request', @level2type=N'COLUMN',@level2name=N'result_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'results updated and sent back to FC ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'credit_check_request', @level2type=N'COLUMN',@level2name=N'result_confirmation_date'
GO
ALTER TABLE [dbo].[credit_check_request] ADD  CONSTRAINT [DF_credit_check_activities_amount_requested]  DEFAULT (0) FOR [amount_requested]
GO
ALTER TABLE [dbo].[credit_check_request] ADD  CONSTRAINT [DF_credit_check_activities_credit_status_id]  DEFAULT (4) FOR [credit_status_id]
GO
ALTER TABLE [dbo].[credit_check_request] ADD  CONSTRAINT [DF_credit_check_activities_amount_confirmed]  DEFAULT (0) FOR [amount_approved]
GO

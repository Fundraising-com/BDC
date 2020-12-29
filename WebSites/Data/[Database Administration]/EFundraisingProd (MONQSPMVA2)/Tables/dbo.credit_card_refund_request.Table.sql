USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[credit_card_refund_request]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[credit_card_refund_request](
	[credit_card_refund_request_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[sale_id] [int] NOT NULL,
	[bpps_id] [int] NULL,
	[request_date] [datetime] NULL,
	[status_code] [char](3) NOT NULL,
	[refund_amount] [money] NULL,
	[processed] [bit] NULL,
	[credit_card_type_id] [tinyint] NOT NULL,
	[cancelled] [bit] NULL,
 CONSTRAINT [PK_credit_card_refund_request] PRIMARY KEY CLUSTERED 
(
	[credit_card_refund_request_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[credit_card_refund_request] ADD  CONSTRAINT [DF_dbo.credit_card_refund_request_Request_date]  DEFAULT (getdate()) FOR [request_date]
GO

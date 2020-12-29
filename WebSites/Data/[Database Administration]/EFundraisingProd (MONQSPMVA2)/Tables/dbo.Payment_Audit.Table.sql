USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Payment_Audit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Payment_Audit](
	[AUDIT_ID] [int] IDENTITY(1,1) NOT NULL,
	[AUDIT_OPERATION] [char](2) NOT NULL,
	[HOST] [varchar](50) NULL,
	[AUDIT_USERID] [varchar](50) NOT NULL,
	[AUDIT_DATETIME] [datetime] NOT NULL,
	[sales_id] [int] NULL,
	[payment_no] [int] NULL,
	[payment_method_id] [tinyint] NULL,
	[collection_status_id] [int] NULL,
	[payment_entry_date] [datetime] NULL,
	[cashable_date] [datetime] NULL,
	[credit_card_no] [varchar](16) NULL,
	[expiry_date] [varchar](7) NULL,
	[name_on_card] [varchar](50) NULL,
	[authorization_number] [varchar](10) NULL,
	[payment_amount] [decimal](15, 4) NULL,
	[commission_paid] [bit] NULL,
	[foreign_orderid] [int] NULL,
	[create_date] [datetime] NULL,
	[ext_payment_id] [int] NULL,
	[create_user_id] [int] NULL,
	[payment_status_id] [int] NULL,
 CONSTRAINT [PK_Payment_AUDIT] PRIMARY KEY CLUSTERED 
(
	[AUDIT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

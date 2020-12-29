USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[deposit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[deposit](
	[deposit_id] [int] NOT NULL,
	[payment_method_id] [tinyint] NOT NULL,
	[bank_id] [int] NOT NULL,
	[bank_account_no] [varchar](50) NOT NULL,
	[deposit_date] [datetime] NOT NULL,
 CONSTRAINT [PK_deposit] PRIMARY KEY CLUSTERED 
(
	[deposit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[deposit]  WITH CHECK ADD  CONSTRAINT [FK_deposit_bank_account] FOREIGN KEY([bank_id], [bank_account_no])
REFERENCES [dbo].[Bank_Account] ([Bank_ID], [Bank_Account_No])
GO
ALTER TABLE [dbo].[deposit] CHECK CONSTRAINT [FK_deposit_bank_account]
GO

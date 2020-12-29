USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[credit_card_types]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[credit_card_types](
	[credit_card_type_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[payment_method_id] [tinyint] NOT NULL,
	[credit_card_type_name] [varchar](25) NOT NULL,
	[credit_card_image] [varchar](25) NULL,
	[display_order] [tinyint] NOT NULL,
	[displayable] [bit] NOT NULL,
 CONSTRAINT [PK_credit_card_types] PRIMARY KEY CLUSTERED 
(
	[credit_card_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[credit_card_types]  WITH CHECK ADD  CONSTRAINT [FK_credit_card_types_payment_method] FOREIGN KEY([payment_method_id])
REFERENCES [dbo].[payment_method] ([payment_method_id])
GO
ALTER TABLE [dbo].[credit_card_types] CHECK CONSTRAINT [FK_credit_card_types_payment_method]
GO

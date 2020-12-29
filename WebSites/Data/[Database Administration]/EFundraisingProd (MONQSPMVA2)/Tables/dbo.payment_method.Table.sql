USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[payment_method]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment_method](
	[payment_method_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[is_negative] [bit] NOT NULL,
	[discount_percentage] [decimal](15, 4) NOT NULL,
	[ext_payment_type_id] [int] NULL,
 CONSTRAINT [PK_payment_method] PRIMARY KEY CLUSTERED 
(
	[payment_method_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UC_payment_method_description] UNIQUE NONCLUSTERED 
(
	[description] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment_method] ADD  CONSTRAINT [DF_payment_method_discount_percentage]  DEFAULT (0) FOR [discount_percentage]
GO

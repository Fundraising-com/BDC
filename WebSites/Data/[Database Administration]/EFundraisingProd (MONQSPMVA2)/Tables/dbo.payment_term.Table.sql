USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[payment_term]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment_term](
	[payment_term_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[discount_percent] [decimal](15, 4) NOT NULL,
	[lead_days] [smallint] NULL,
	[default_ar_status] [int] NULL,
	[hide_from_consultants] [bit] NOT NULL,
	[ext_payment_type_id] [int] NULL,
 CONSTRAINT [PK_payment_term] PRIMARY KEY CLUSTERED 
(
	[payment_term_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_payment_term_description] UNIQUE NONCLUSTERED 
(
	[description] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment_term] ADD  CONSTRAINT [DF_payment_term_payment_term_id]  DEFAULT (0) FOR [payment_term_id]
GO
ALTER TABLE [dbo].[payment_term] ADD  CONSTRAINT [DF_payment_term_discount_percent]  DEFAULT (0) FOR [discount_percent]
GO
ALTER TABLE [dbo].[payment_term] ADD  CONSTRAINT [DF_payment_term_lead_days]  DEFAULT (0) FOR [lead_days]
GO

USE [eFundstore]
GO
/****** Object:  Table [dbo].[credit_card_type]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[credit_card_type](
	[credit_card_type_id] [tinyint] IDENTITY(1,1) NOT NULL,
	[payment_method_id] [tinyint] NOT NULL,
	[credit_card_type_name] [varchar](25) NOT NULL,
	[credit_card_image] [varchar](25) NULL,
	[display_order] [tinyint] NOT NULL,
	[displayable] [bit] NOT NULL,
 CONSTRAINT [PK_credit_card_types] PRIMARY KEY NONCLUSTERED 
(
	[credit_card_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[credit_card_type] ADD  CONSTRAINT [DF_credit_card_types_displayable]  DEFAULT (1) FOR [displayable]
GO

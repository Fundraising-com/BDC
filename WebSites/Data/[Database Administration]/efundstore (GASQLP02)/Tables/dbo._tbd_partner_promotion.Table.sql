USE [eFundstore]
GO
/****** Object:  Table [dbo].[_tbd_partner_promotion]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_tbd_partner_promotion](
	[partner_promotion_id] [int] IDENTITY(1,1) NOT NULL,
	[partner_id] [int] NOT NULL,
	[promotion_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_promotion] PRIMARY KEY CLUSTERED 
(
	[partner_promotion_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[_tbd_partner_promotion] ADD  CONSTRAINT [DF_partner_promotion_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

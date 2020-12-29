USE [EFRCommon]
GO
/****** Object:  Table [dbo].[partner_promotion]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_promotion](
	[partner_promotion_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[partner_id] [int] NOT NULL,
	[promotion_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_promotion] PRIMARY KEY CLUSTERED 
(
	[partner_promotion_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[partner_promotion]  WITH CHECK ADD  CONSTRAINT [FK_partner_promotion_partner] FOREIGN KEY([partner_id])
REFERENCES [dbo].[partner] ([partner_id])
GO
ALTER TABLE [dbo].[partner_promotion] CHECK CONSTRAINT [FK_partner_promotion_partner]
GO
ALTER TABLE [dbo].[partner_promotion]  WITH CHECK ADD  CONSTRAINT [FK_partner_promotion_promotion] FOREIGN KEY([promotion_id])
REFERENCES [dbo].[promotion] ([promotion_id])
GO
ALTER TABLE [dbo].[partner_promotion] CHECK CONSTRAINT [FK_partner_promotion_promotion]
GO

USE [fastfundraising]
GO
/****** Object:  Table [dbo].[shipping_group]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shipping_group](
	[shipping_group_id] [int] NOT NULL,
	[shipping_fee_id] [int] NOT NULL,
	[default] [bit] NULL,
 CONSTRAINT [PK_shipping_group] PRIMARY KEY CLUSTERED 
(
	[shipping_group_id] ASC,
	[shipping_fee_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[shipping_group]  WITH CHECK ADD  CONSTRAINT [FK_shipping_group_shipping_fee] FOREIGN KEY([shipping_fee_id])
REFERENCES [dbo].[shipping_fee] ([shipping_fee_id])
GO
ALTER TABLE [dbo].[shipping_group] CHECK CONSTRAINT [FK_shipping_group_shipping_fee]
GO

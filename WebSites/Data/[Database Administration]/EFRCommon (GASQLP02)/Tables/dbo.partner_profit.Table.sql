USE [EFRCommon]
GO
/****** Object:  Table [dbo].[partner_profit]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_profit](
	[partner_profit_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[partner_id] [int] NOT NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NULL,
	[profit_group_id] [int] NULL,
 CONSTRAINT [PK_partner_profit] PRIMARY KEY CLUSTERED 
(
	[partner_profit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[partner_profit]  WITH CHECK ADD  CONSTRAINT [FK_partner_profit_group] FOREIGN KEY([profit_group_id])
REFERENCES [dbo].[profit_group] ([profit_group_id])
GO
ALTER TABLE [dbo].[partner_profit] CHECK CONSTRAINT [FK_partner_profit_group]
GO

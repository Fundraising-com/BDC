USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[program_partner_prize]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[program_partner_prize](
	[program_id] [int] NOT NULL,
	[partner_id] [int] NOT NULL,
	[prize_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_program_partner_prize] PRIMARY KEY CLUSTERED 
(
	[program_id] ASC,
	[partner_id] ASC,
	[prize_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[program_partner_prize]  WITH CHECK ADD  CONSTRAINT [FK_program_partner_prize_prize] FOREIGN KEY([prize_id])
REFERENCES [dbo].[prize] ([prize_id])
GO
ALTER TABLE [dbo].[program_partner_prize] CHECK CONSTRAINT [FK_program_partner_prize_prize]
GO
ALTER TABLE [dbo].[program_partner_prize]  WITH CHECK ADD  CONSTRAINT [FK_program_partner_prize_program_partner] FOREIGN KEY([program_id], [partner_id])
REFERENCES [dbo].[program_partner] ([program_id], [partner_id])
GO
ALTER TABLE [dbo].[program_partner_prize] CHECK CONSTRAINT [FK_program_partner_prize_program_partner]
GO
ALTER TABLE [dbo].[program_partner_prize] ADD  CONSTRAINT [DF_program_partner_prize_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

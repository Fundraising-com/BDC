USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[member_phone_number]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[member_phone_number](
	[member_phone_number_id] [int] IDENTITY(1000000,1) NOT FOR REPLICATION NOT NULL,
	[member_id] [int] NOT NULL,
	[phone_number_type_id] [int] NOT NULL,
	[phone_number_id] [int] NOT NULL,
	[active] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_member_phone_number] PRIMARY KEY CLUSTERED 
(
	[member_phone_number_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[member_phone_number]  WITH CHECK ADD  CONSTRAINT [FK_member_phone_number_member] FOREIGN KEY([member_id])
REFERENCES [dbo].[member] ([member_id])
GO
ALTER TABLE [dbo].[member_phone_number] CHECK CONSTRAINT [FK_member_phone_number_member]
GO
ALTER TABLE [dbo].[member_phone_number]  WITH CHECK ADD  CONSTRAINT [FK_member_phone_number_phone_number] FOREIGN KEY([phone_number_id])
REFERENCES [dbo].[phone_number] ([phone_number_id])
GO
ALTER TABLE [dbo].[member_phone_number] CHECK CONSTRAINT [FK_member_phone_number_phone_number]
GO
ALTER TABLE [dbo].[member_phone_number]  WITH CHECK ADD  CONSTRAINT [FK_member_phone_number_phone_number_type] FOREIGN KEY([phone_number_type_id])
REFERENCES [dbo].[phone_number_type] ([phone_number_type_id])
GO
ALTER TABLE [dbo].[member_phone_number] CHECK CONSTRAINT [FK_member_phone_number_phone_number_type]
GO
ALTER TABLE [dbo].[member_phone_number] ADD  CONSTRAINT [DF_member_phone_number_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

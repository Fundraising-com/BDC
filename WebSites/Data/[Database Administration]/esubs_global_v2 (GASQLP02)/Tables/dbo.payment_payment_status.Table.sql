USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_payment_status]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment_payment_status](
	[payment_id] [int] NOT NULL,
	[payment_status_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_payment_payment_status] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC,
	[payment_status_id] ASC,
	[create_date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[payment_payment_status]  WITH CHECK ADD  CONSTRAINT [FK_payment_payment_status_payment] FOREIGN KEY([payment_id])
REFERENCES [dbo].[payment] ([payment_id])
GO
ALTER TABLE [dbo].[payment_payment_status] CHECK CONSTRAINT [FK_payment_payment_status_payment]
GO
ALTER TABLE [dbo].[payment_payment_status]  WITH CHECK ADD  CONSTRAINT [FK_payment_payment_status_payment_status] FOREIGN KEY([payment_status_id])
REFERENCES [dbo].[payment_status] ([payment_status_id])
GO
ALTER TABLE [dbo].[payment_payment_status] CHECK CONSTRAINT [FK_payment_payment_status_payment_status]
GO
ALTER TABLE [dbo].[payment_payment_status] ADD  CONSTRAINT [DF_payment_payment_status_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

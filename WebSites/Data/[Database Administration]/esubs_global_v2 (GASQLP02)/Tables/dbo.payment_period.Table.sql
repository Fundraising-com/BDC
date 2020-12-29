USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_period]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment_period](
	[payment_period_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_payment_period] PRIMARY KEY CLUSTERED 
(
	[payment_period_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[payment_period] ADD  CONSTRAINT [DF_payment_period_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event_total_amount]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[event_total_amount](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[event_id] [int] NOT NULL,
	[items] [int] NOT NULL,
	[total_amount] [money] NOT NULL,
	[total_supporters] [int] NOT NULL,
	[total_donation_amount] [money] NOT NULL,
	[total_donars] [int] NOT NULL,
	[total_profit] [money] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_event_total_amount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

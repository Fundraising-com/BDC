USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[profit_range]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profit_range](
	[profit_range_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[item_nbr_min] [int] NULL,
	[item_nbr_max] [int] NULL,
	[profit_percentage] [float] NULL,
 CONSTRAINT [PK_profit_range] PRIMARY KEY CLUSTERED 
(
	[profit_range_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

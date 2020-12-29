USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[cost_range]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cost_range](
	[cost_range_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[scratch_book_id] [int] NOT NULL,
	[service_type_id] [tinyint] NULL,
	[minimum] [int] NULL,
	[maximum] [int] NULL,
	[cost] [float] NULL,
	[margin_plan] [decimal](15, 4) NULL,
 CONSTRAINT [PK_cost_range] PRIMARY KEY CLUSTERED 
(
	[cost_range_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cost_range] ADD  CONSTRAINT [DF_Cost_Range_Minimum]  DEFAULT (0) FOR [minimum]
GO
ALTER TABLE [dbo].[cost_range] ADD  CONSTRAINT [DF_Cost_Range_Maximum]  DEFAULT (100000) FOR [maximum]
GO

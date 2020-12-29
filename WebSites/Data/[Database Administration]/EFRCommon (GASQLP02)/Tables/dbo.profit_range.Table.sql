USE [EFRCommon]
GO
/****** Object:  Table [dbo].[profit_range]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[profit_range](
	[profit_range_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[profit_id] [int] NOT NULL,
	[profit_range_percentage] [float] NOT NULL,
	[min_sub] [int] NULL,
	[min_amount] [int] NULL,
	[operator] [varchar](3) NULL,
	[disclaimer] [varchar](500) NULL,
 CONSTRAINT [PK_profit_range] PRIMARY KEY CLUSTERED 
(
	[profit_range_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[profit_range]  WITH CHECK ADD  CONSTRAINT [FK_profit_range_profit] FOREIGN KEY([profit_id])
REFERENCES [dbo].[profit] ([profit_id])
GO
ALTER TABLE [dbo].[profit_range] CHECK CONSTRAINT [FK_profit_range_profit]
GO

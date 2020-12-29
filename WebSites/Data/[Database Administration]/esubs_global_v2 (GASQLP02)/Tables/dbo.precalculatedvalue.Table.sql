USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[precalculatedvalue]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[precalculatedvalue](
	[sales_amount_grand_total] [money] NULL,
	[update_date] [datetime] NOT NULL,
 CONSTRAINT [PK_precalculatedvalue] PRIMARY KEY CLUSTERED 
(
	[update_date] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[tmp_report_center_res]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tmp_report_center_res](
	[rand_id] [varchar](20) NULL,
	[partner_id] [int] NULL,
	[promotion_id] [int] NULL,
	[description] [varchar](255) NULL,
	[countofleadid] [int] NULL,
	[countofsalesid] [int] NULL,
	[brochures] [float] NULL,
	[candies] [float] NULL,
	[scratchcards] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

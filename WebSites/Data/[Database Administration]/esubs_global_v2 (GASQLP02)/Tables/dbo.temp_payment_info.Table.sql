USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_payment_info]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_payment_info](
	[payment_info_id] [int] IDENTITY(1,1) NOT NULL,
	[check_id] [int] NULL
) ON [PRIMARY]
GO

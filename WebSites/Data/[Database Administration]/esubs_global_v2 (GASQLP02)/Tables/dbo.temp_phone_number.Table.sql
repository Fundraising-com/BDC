USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_phone_number]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_phone_number](
	[phone_number_id] [int] IDENTITY(1,1) NOT NULL,
	[member_id] [int] NOT NULL,
	[phone_number_type_id] [int] NOT NULL,
	[phone_number] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

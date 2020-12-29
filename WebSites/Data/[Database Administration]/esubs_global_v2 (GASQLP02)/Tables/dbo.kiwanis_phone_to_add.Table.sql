USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[kiwanis_phone_to_add]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kiwanis_phone_to_add](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](255) NULL,
	[phone_number] [varchar](50) NULL,
	[payment_info_id] [int] NOT NULL,
	[done] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[kiwanis_phone_to_add] ADD  CONSTRAINT [DF_kiwanis_phone_to_add_done]  DEFAULT (0) FOR [done]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[phone_number]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[phone_number](
	[phone_number_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[phone_number] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_phone_number] PRIMARY KEY CLUSTERED 
(
	[phone_number_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[phone_number] ADD  CONSTRAINT [DF_phone_number_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

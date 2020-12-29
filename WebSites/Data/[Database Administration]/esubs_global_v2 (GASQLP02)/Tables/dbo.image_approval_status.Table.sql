USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[image_approval_status]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[image_approval_status](
	[image_approval_status_id] [int] IDENTITY(1,1) NOT NULL,
	[image_approval_status_description] [varchar](255) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_image_approval_status] PRIMARY KEY CLUSTERED 
(
	[image_approval_status_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[image_approval_status] ADD  CONSTRAINT [DF_image_approval_status_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[new_email_template_desc]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[new_email_template_desc](
	[ID] [float] NULL,
	[MemberType] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[New Description] [nvarchar](255) NULL
) ON [PRIMARY]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[FR_Bounce_March_09_All]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FR_Bounce_March_09_All](
	[lead_id] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[first_name] [nvarchar](50) NULL,
	[consultant] [nvarchar](50) NULL,
	[partner] [nvarchar](50) NULL
) ON [PRIMARY]
GO

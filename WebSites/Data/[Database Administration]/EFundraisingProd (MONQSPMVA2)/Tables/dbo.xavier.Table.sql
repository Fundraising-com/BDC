USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[xavier]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[xavier](
	[lead_id] [int] NULL,
	[type] [nvarchar](50) NULL,
	[done] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[xavier] ADD  CONSTRAINT [DF_xavier_done]  DEFAULT (0) FOR [done]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Lead_Status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lead_Status](
	[Lead_Status_ID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Lead_Status] PRIMARY KEY CLUSTERED 
(
	[Lead_Status_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Lead_Status] ADD  CONSTRAINT [DF_Lead_Status_Lead_Status_ID]  DEFAULT (0) FOR [Lead_Status_ID]
GO

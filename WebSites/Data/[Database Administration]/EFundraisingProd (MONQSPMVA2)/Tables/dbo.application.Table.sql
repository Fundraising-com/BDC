USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[application]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[application](
	[application_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[description] [varchar](50) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_application] PRIMARY KEY CLUSTERED 
(
	[application_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[application] ADD  CONSTRAINT [DF_application_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

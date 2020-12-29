USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[client_status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[client_status](
	[client_status_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_client_status_description] PRIMARY KEY CLUSTERED 
(
	[client_status_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

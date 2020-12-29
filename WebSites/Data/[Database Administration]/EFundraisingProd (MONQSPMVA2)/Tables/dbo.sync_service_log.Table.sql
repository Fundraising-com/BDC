USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[sync_service_log]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sync_service_log](
	[ProcessDate] [datetime] NOT NULL,
	[Success] [bit] NOT NULL,
 CONSTRAINT [PK_SyncServiceLog] PRIMARY KEY CLUSTERED 
(
	[ProcessDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

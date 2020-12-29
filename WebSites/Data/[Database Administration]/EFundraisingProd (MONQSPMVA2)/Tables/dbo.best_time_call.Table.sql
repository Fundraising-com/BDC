USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[best_time_call]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[best_time_call](
	[best_time_call_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[best_time_call_desc] [varchar](25) NOT NULL,
 CONSTRAINT [PK_best_time_call] PRIMARY KEY CLUSTERED 
(
	[best_time_call_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

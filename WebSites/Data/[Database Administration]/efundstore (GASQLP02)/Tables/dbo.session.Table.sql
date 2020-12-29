USE [eFundstore]
GO
/****** Object:  Table [dbo].[session]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[session](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[visitors_log_id] [int] NOT NULL,
	[date_created] [datetime] NOT NULL,
 CONSTRAINT [PK_session] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[session] ADD  CONSTRAINT [DF_session_date_created]  DEFAULT (getdate()) FOR [date_created]
GO

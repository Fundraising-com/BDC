USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[exception_type]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[exception_type](
	[exception_type_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_exception_type] PRIMARY KEY CLUSTERED 
(
	[exception_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

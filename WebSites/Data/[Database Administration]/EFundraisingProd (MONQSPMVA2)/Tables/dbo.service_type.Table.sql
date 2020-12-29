USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[service_type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[service_type](
	[service_type_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_service_type] PRIMARY KEY CLUSTERED 
(
	[service_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

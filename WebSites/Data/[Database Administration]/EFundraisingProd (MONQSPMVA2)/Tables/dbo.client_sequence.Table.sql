USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[client_sequence]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client_sequence](
	[client_sequence_code] [char](2) NOT NULL,
	[description] [varchar](50) NOT NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_client_sequence] PRIMARY KEY CLUSTERED 
(
	[client_sequence_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

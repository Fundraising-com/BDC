USE [eFundstore]
GO
/****** Object:  Table [dbo].[client_address_type]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client_address_type](
	[client_address_type_id] [char](2) NOT NULL,
	[description] [varchar](25) NOT NULL,
 CONSTRAINT [PK_client_address_type] PRIMARY KEY CLUSTERED 
(
	[client_address_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

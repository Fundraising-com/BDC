USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[client_address_type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client_address_type](
	[address_type] [char](2) NOT NULL,
	[address_type_desc] [varchar](25) NOT NULL,
 CONSTRAINT [PK_client_address_type] PRIMARY KEY CLUSTERED 
(
	[address_type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

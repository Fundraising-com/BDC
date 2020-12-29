USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[party_type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[party_type](
	[party_type_id] [tinyint] NOT NULL,
	[party_type_desc] [varchar](25) NOT NULL,
 CONSTRAINT [PK_party_type] PRIMARY KEY CLUSTERED 
(
	[party_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

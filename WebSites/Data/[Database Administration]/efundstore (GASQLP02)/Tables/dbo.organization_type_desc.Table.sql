USE [eFundstore]
GO
/****** Object:  Table [dbo].[organization_type_desc]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[organization_type_desc](
	[organization_type_id] [tinyint] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[description] [varchar](200) NOT NULL,
 CONSTRAINT [PK_organization_type_desc_1] PRIMARY KEY CLUSTERED 
(
	[organization_type_id] ASC,
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[package_templates]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[package_templates](
	[package_template_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[package_template_desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_package_templates] PRIMARY KEY CLUSTERED 
(
	[package_template_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

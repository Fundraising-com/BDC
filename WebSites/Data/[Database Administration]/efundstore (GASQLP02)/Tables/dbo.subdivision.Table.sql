USE [eFundstore]
GO
/****** Object:  Table [dbo].[subdivision]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subdivision](
	[subdivision_code] [nvarchar](7) NOT NULL,
	[country_code] [nvarchar](2) NOT NULL,
	[subdivision_name_1] [nvarchar](100) NOT NULL,
	[subdivision_name_2] [nvarchar](100) NULL,
	[subdivision_name_3] [nvarchar](100) NULL,
	[regional_division] [nvarchar](10) NULL,
	[subdivision_category] [nvarchar](50) NULL,
	[display] [bit] NULL,
 CONSTRAINT [PK_subdivision] PRIMARY KEY CLUSTERED 
(
	[subdivision_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[subdivision] ADD  CONSTRAINT [DF_subdivision_display]  DEFAULT (1) FOR [display]
GO

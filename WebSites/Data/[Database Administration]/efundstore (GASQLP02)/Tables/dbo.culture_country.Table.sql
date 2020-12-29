USE [eFundstore]
GO
/****** Object:  Table [dbo].[culture_country]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[culture_country](
	[culture_code] [nvarchar](5) NOT NULL,
	[country_code] [nvarchar](2) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_culture_country_name] PRIMARY KEY CLUSTERED 
(
	[culture_code] ASC,
	[country_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

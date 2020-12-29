USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[language]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[language](
	[language_code] [nvarchar](2) NOT NULL,
	[language_name] [nvarchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_language] PRIMARY KEY CLUSTERED 
(
	[language_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[language] ADD  CONSTRAINT [DF_language_create_date]  DEFAULT (getdate()) FOR [create_date]
GO

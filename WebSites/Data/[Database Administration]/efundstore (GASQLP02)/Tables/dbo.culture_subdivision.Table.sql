USE [eFundstore]
GO
/****** Object:  Table [dbo].[culture_subdivision]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[culture_subdivision](
	[culture_code] [nvarchar](5) NOT NULL,
	[subdivision_code] [nvarchar](7) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_culture_subdivision_name] PRIMARY KEY CLUSTERED 
(
	[culture_code] ASC,
	[subdivision_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[partner_culture_link]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_culture_link](
	[partner_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[linked_partner_id] [int] NOT NULL,
 CONSTRAINT [PK_partner_culture_link] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC,
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[business_calendar]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[business_calendar](
	[business_date] [datetime] NOT NULL,
	[weekend] [int] NOT NULL,
	[holiday] [int] NOT NULL,
 CONSTRAINT [PK_business_calendar] PRIMARY KEY CLUSTERED 
(
	[business_date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

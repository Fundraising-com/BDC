USE [eFundweb]
GO
/****** Object:  Table [dbo].[leadgen_lead]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leadgen_lead](
	[LeadGen_id] [int] NOT NULL,
	[lead_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL
) ON [PRIMARY]
GO

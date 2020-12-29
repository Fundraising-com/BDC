USE [eFundweb]
GO
/****** Object:  Table [dbo].[Agent_Address]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Agent_Address](
	[Address_ID] [int] IDENTITY(1,1) NOT NULL,
	[Agent_ID] [int] NULL,
	[Address] [varchar](40) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](30) NULL,
	[Country] [varchar](40) NULL,
	[Zip] [varchar](10) NULL,
 CONSTRAINT [PK_address_id] PRIMARY KEY CLUSTERED 
(
	[Address_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

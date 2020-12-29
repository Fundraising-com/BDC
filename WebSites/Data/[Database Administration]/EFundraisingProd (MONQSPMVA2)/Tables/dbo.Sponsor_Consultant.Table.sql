USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Sponsor_Consultant]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sponsor_Consultant](
	[Sponsor_Consultant_ID] [int] NOT NULL,
	[First_Name] [varchar](50) NULL,
	[Middle Initial] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[Day_Phone] [varchar](20) NULL,
	[Day_Time_Call] [varchar](20) NULL,
	[Evening_Phone] [varchar](20) NULL,
	[Evenig_Time_Call] [varchar](20) NULL,
	[Alternate_Phone] [varchar](50) NULL,
	[Fax] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Comment] [text] NULL,
	[Is_Active] [bit] NOT NULL,
	[Nt_Login] [varchar](50) NULL,
	[Commission_Rate] [float] NULL,
 CONSTRAINT [PK_Sponsor_Consultant] PRIMARY KEY NONCLUSTERED 
(
	[Sponsor_Consultant_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Sponsor_Consultant] ADD  CONSTRAINT [DF_Sponsor_Consultant_Sponsor_Consultant_ID]  DEFAULT (0) FOR [Sponsor_Consultant_ID]
GO
ALTER TABLE [dbo].[Sponsor_Consultant] ADD  CONSTRAINT [DF_Sponsor_Consultant_Is_Active]  DEFAULT (0) FOR [Is_Active]
GO

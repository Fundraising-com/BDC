USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Conversion_Rate_Table]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Conversion_Rate_Table](
	[Currency_Code] [varchar](10) NOT NULL,
	[Conversion_Rate] [decimal](15, 4) NULL,
	[Conversion_Date] [smalldatetime] NULL,
	[Conversion_Rate_Id] [int] NOT NULL,
 CONSTRAINT [PK_Conversion_Rate_Table] PRIMARY KEY NONCLUSTERED 
(
	[Conversion_Rate_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Conversion_Rate_Table] ADD  CONSTRAINT [DF_Conversion_Rate_Table_Conversion_Rate]  DEFAULT (0) FOR [Conversion_Rate]
GO

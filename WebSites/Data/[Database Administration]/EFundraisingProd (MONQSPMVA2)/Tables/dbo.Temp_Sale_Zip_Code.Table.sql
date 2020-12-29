USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Temp_Sale_Zip_Code]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temp_Sale_Zip_Code](
	[Zip_Code] [varchar](15) NOT NULL,
	[Sale_To_Add_ID] [int] NOT NULL,
 CONSTRAINT [PK_Temp_Sale_Zip_Code] PRIMARY KEY NONCLUSTERED 
(
	[Zip_Code] ASC,
	[Sale_To_Add_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

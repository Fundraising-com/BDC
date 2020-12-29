USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Commission_Table]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Commission_Table](
	[Promotion_Type_Code] [varchar](4) NOT NULL,
	[Channel_Code] [varchar](4) NOT NULL,
	[Commission_Rate] [decimal](15, 4) NOT NULL,
 CONSTRAINT [PK_Commission_Table] PRIMARY KEY NONCLUSTERED 
(
	[Promotion_Type_Code] ASC,
	[Channel_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

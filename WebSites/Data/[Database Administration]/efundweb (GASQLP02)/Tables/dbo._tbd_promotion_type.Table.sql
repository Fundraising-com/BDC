USE [eFundweb]
GO
/****** Object:  Table [dbo].[_tbd_promotion_type]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_promotion_type](
	[Promotion_Type_Code] [varchar](4) NOT NULL,
	[Description] [varchar](50) NULL,
	[Default_Commission_Rate] [numeric](15, 4) NULL,
 CONSTRAINT [PK_Promotion_Type] PRIMARY KEY CLUSTERED 
(
	[Promotion_Type_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

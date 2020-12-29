USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[_tbd_promotion_type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_promotion_type](
	[Promotion_Type_Code] [varchar](4) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Default_Commission_Rate] [decimal](15, 4) NOT NULL,
	[Channel] [int] NULL,
 CONSTRAINT [PK_Promotion_Type] PRIMARY KEY CLUSTERED 
(
	[Promotion_Type_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UX_Promotion_Type_Description] UNIQUE NONCLUSTERED 
(
	[Description] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[_tbd_promotion_type] ADD  CONSTRAINT [DF_Promotion_Type_Default_Commission_Rate]  DEFAULT (0) FOR [Default_Commission_Rate]
GO

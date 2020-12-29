USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Default_Consultant_Rate]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Default_Consultant_Rate](
	[Consultant_ID] [int] NOT NULL,
	[Promotion_Type_Code] [varchar](4) NOT NULL,
	[Default_Commission_Rate] [decimal](15, 4) NULL,
 CONSTRAINT [PK_Default_Consultant_Rate] PRIMARY KEY NONCLUSTERED 
(
	[Consultant_ID] ASC,
	[Promotion_Type_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Default_Consultant_Rate]  WITH NOCHECK ADD  CONSTRAINT [fk_DCR_Promotion_Type_Code] FOREIGN KEY([Promotion_Type_Code])
REFERENCES [dbo].[_tbd_promotion_type] ([Promotion_Type_Code])
GO
ALTER TABLE [dbo].[Default_Consultant_Rate] CHECK CONSTRAINT [fk_DCR_Promotion_Type_Code]
GO
ALTER TABLE [dbo].[Default_Consultant_Rate]  WITH NOCHECK ADD  CONSTRAINT [FK_default_consultant_rate_consultant] FOREIGN KEY([Consultant_ID])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[Default_Consultant_Rate] CHECK CONSTRAINT [FK_default_consultant_rate_consultant]
GO
ALTER TABLE [dbo].[Default_Consultant_Rate] ADD  CONSTRAINT [DF_Default_Consultant_Rate_Consultant_ID]  DEFAULT (0) FOR [Consultant_ID]
GO
ALTER TABLE [dbo].[Default_Consultant_Rate] ADD  CONSTRAINT [DF_Default_Consultant_Rate_Default_Commission_Rate]  DEFAULT (0) FOR [Default_Commission_Rate]
GO

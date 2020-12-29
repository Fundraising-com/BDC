USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Lead_Combinaisons]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lead_Combinaisons](
	[Lead_Combinaison_ID] [int] NOT NULL,
	[Condition_ID] [int] NOT NULL,
	[Lead_Qualification_Type_ID] [int] NOT NULL,
 CONSTRAINT [PK_Lead_Combinaisons] PRIMARY KEY CLUSTERED 
(
	[Lead_Combinaison_ID] ASC,
	[Condition_ID] ASC,
	[Lead_Qualification_Type_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lead_Combinaisons]  WITH NOCHECK ADD  CONSTRAINT [fk_lc_condition_id] FOREIGN KEY([Condition_ID])
REFERENCES [dbo].[Lead_Conditions] ([Condition_ID])
GO
ALTER TABLE [dbo].[Lead_Combinaisons] CHECK CONSTRAINT [fk_lc_condition_id]
GO
ALTER TABLE [dbo].[Lead_Combinaisons]  WITH NOCHECK ADD  CONSTRAINT [fk_lc_lead_qualification_type] FOREIGN KEY([Lead_Qualification_Type_ID])
REFERENCES [dbo].[Lead_Qualification_Type] ([Lead_Qualification_Type_ID])
GO
ALTER TABLE [dbo].[Lead_Combinaisons] CHECK CONSTRAINT [fk_lc_lead_qualification_type]
GO

USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Advertising_Support]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertising_Support](
	[Advertising_Support_ID] [int] NOT NULL,
	[Advertising_Support_Type_ID] [int] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Publishnig_Date] [smalldatetime] NULL,
	[Web_Site] [varchar](100) NULL,
	[Ordering_Phone_Number] [varchar](25) NULL,
	[Periodicity] [int] NULL,
	[Nb_Draw] [int] NULL,
	[Magazine_Price] [decimal](15, 4) NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Advertising_Support] PRIMARY KEY NONCLUSTERED 
(
	[Advertising_Support_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Advertising_Support]  WITH CHECK ADD  CONSTRAINT [fk_AS_Advertising_Support_Type_ID] FOREIGN KEY([Advertising_Support_Type_ID])
REFERENCES [dbo].[Advertising_Support_Type] ([Advertising_Support_Type_ID])
GO
ALTER TABLE [dbo].[Advertising_Support] CHECK CONSTRAINT [fk_AS_Advertising_Support_Type_ID]
GO

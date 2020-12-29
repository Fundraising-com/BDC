USE [eFundweb]
GO
/****** Object:  Table [dbo].[Advertising_Support]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertising_Support](
	[Advertising_Support_ID] [int] NOT NULL,
	[Advertising_Support_Type_ID] [int] NULL,
	[Title] [char](50) NULL,
	[Publishnig_Date] [datetime] NULL,
	[Web_Site] [char](100) NULL,
	[Ordering_Phone_Number] [char](25) NULL,
	[Periodicity] [int] NULL,
	[Nb_Draw] [int] NULL,
	[Magazine_Price] [numeric](15, 4) NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Advertising_Support] PRIMARY KEY CLUSTERED 
(
	[Advertising_Support_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Advertising_Support]  WITH CHECK ADD  CONSTRAINT [FK_Advertising_Support_Advertising_Support_Type] FOREIGN KEY([Advertising_Support_Type_ID])
REFERENCES [dbo].[Advertising_Support_Type] ([Advertising_Support_Type_ID])
GO
ALTER TABLE [dbo].[Advertising_Support] CHECK CONSTRAINT [FK_Advertising_Support_Advertising_Support_Type]
GO

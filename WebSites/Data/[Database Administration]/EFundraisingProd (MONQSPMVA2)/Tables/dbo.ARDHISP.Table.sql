USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[ARDHISP]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ARDHISP](
	[ARGORF] [nvarchar](1) NOT NULL,
	[ARCUST] [decimal](9, 0) NOT NULL,
	[AR#SEQ] [decimal](5, 0) NOT NULL,
	[ARCNTR] [decimal](2, 0) NOT NULL,
	[ARYRTR] [decimal](2, 0) NOT NULL,
	[ARMOTR] [decimal](2, 0) NOT NULL,
	[ARDYTR] [decimal](2, 0) NOT NULL,
	[ARAMTR] [decimal](9, 2) NOT NULL,
	[ARCDTR] [nvarchar](2) NOT NULL,
	[ARORDR] [nvarchar](6) NOT NULL,
	[AR#ORD] [decimal](7, 0) NOT NULL,
	[AR#IFM] [decimal](6, 0) NOT NULL,
	[ARAMBL] [decimal](9, 2) NOT NULL,
	[ARAMCR] [decimal](9, 2) NOT NULL,
	[ARAMCL] [decimal](9, 2) NOT NULL,
	[ARAMNC] [decimal](9, 2) NOT NULL,
	[ARCDNC] [nvarchar](1) NOT NULL,
	[AR#CHK] [decimal](7, 0) NOT NULL,
	[ARAUTO] [nvarchar](1) NOT NULL,
	[ARCNIV] [decimal](2, 0) NOT NULL,
	[ARYRIV] [decimal](2, 0) NOT NULL,
	[ARMOIV] [decimal](2, 0) NOT NULL,
	[ARDYIV] [decimal](2, 0) NOT NULL,
	[AR#ISQ] [decimal](5, 0) NOT NULL,
	[AR#BAT] [decimal](7, 0) NOT NULL,
	[AR#BSQ] [decimal](5, 0) NOT NULL,
	[ARCDCL] [nvarchar](1) NOT NULL,
	[ARNSET] [nvarchar](1) NOT NULL,
	[ARSOBA] [nvarchar](1) NOT NULL,
	[AR#SSQ] [decimal](5, 0) NOT NULL,
	[ARRFLG] [nvarchar](1) NOT NULL,
	[ARCDFC] [nvarchar](1) NOT NULL
) ON [PRIMARY]
GO

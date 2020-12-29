USE [fastfundraising]
GO
/****** Object:  Table [dbo].[ffitems]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ffitems](
	[itemid] [int] NOT NULL,
	[categoryid] [int] NULL,
	[itemname] [varchar](100) NULL,
	[itemnmbr] [varchar](50) NULL,
	[imagepath] [varchar](100) NULL,
	[descriptionpath] [varchar](100) NULL,
	[status] [int] NULL,
	[itemuom] [varchar](50) NULL,
	[peruom] [varchar](100) NULL,
	[enduserprice] [float] NULL,
	[baseprice] [float] NULL,
	[strikeprice] [float] NULL,
	[description] [text] NULL,
	[flavors] [text] NULL,
	[packaging] [text] NULL,
	[minimumqty] [int] NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_ffitems] PRIMARY KEY NONCLUSTERED 
(
	[itemid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ffitems] ADD  CONSTRAINT [DF_ffitems_minimumqty]  DEFAULT (1) FOR [minimumqty]
GO

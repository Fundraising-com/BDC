USE [QSPCanadaCommon]
GO
/****** Object:  View [dbo].[syncobj_0x3643334634424531]    Script Date: 06/07/2017 09:32:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[syncobj_0x3643334634424531]as select  [ID],[Country],[FundraisingProcedureID],[ProgramTypeID],[Name],[MajorProductLineID],[DefaultProfit],[MinProfit],[MaxProfit],[OrdefuPrintInAR],[ActiveForFiscal_TF],[Abr],[FrenchName]  from  [dbo].[Program]  where permissions(477244755) & 1 = 1
GO

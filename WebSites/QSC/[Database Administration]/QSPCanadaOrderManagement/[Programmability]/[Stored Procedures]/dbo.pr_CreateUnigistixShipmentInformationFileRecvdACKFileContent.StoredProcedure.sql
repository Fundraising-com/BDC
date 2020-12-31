USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateUnigistixShipmentInformationFileRecvdACKFileContent]    Script Date: 06/07/2017 09:19:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_CreateUnigistixShipmentInformationFileRecvdACKFileContent]
	@file varchar(200),
	@size varchar(20),
	@date varchar(20)
as


Select '<ACKNOWLEDGE>
<FILE>
<NAME>'+@file+'</NAME>
<SIZE>'+@size+'</SIZE>
<DATE>'+@date+'</DATE>
</FILE>
</ACKNOWLEDGE>
'
GO

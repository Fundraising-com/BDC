USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_PhoneList]    Script Date: 06/07/2017 09:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_get_PhoneList]
  @ListId int,
  @xml bit =  0
AS


IF( @xml = 0)
begin
	SELECT 
		ID, 
		isnull(Type, 30500) as Type, 
		PhoneListID, 
		PhoneNumber AS Number, 
		BestTimeToCall,
		isnull(PT.[description], '') as TypeDescription
	 FROM 
		Phone AS PhoneNumber
		LEFT JOIN QSPCanadaCommon.dbo.PhoneType PT
		ON PhoneNumber.Type = PT.PhoneTypeID
	WHERE
		PhoneListID = @ListId
	ORDER BY
		case Type
		when 30505 then 1 --Main
		when 30503 then 2 --Then Fax
		else 3 --Then everything else
		end ASC
end
/*
ELSE
begin
	SELECT 
		ID, 
		Type, 
		PhoneListID, 
		PhoneNumber AS Number, 
		BestTimeToCall,
		isnull(PT.[description], '') as TypeDescription
	 FROM 
		Phone AS PhoneNumber
		LEFT JOIN QSPCanadaCommon.dbo.PhoneType PT
		ON PhoneNumber.Type = PT.PhoneTypeID
	WHERE
		PhoneListID = @ListId
	ORDER BY
		case Type
		when 30505 then 1 --Main
		when 30503 then 2 --Then Fax
		else 3 --Then everything else
		end ASC
	FOR XML AUTO 
end
*/
GO

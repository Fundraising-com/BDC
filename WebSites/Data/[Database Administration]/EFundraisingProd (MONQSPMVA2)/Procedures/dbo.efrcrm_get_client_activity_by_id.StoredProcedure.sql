USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_client_activity_by_id]    Script Date: 02/14/2014 13:03:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Client_activity
CREATE PROCEDURE [dbo].[efrcrm_get_client_activity_by_id] @Client_activity_id int AS
begin

select Client_activity_id, Client_id, Client_sequence_code, Client_activity_type_id, Client_activity_date, Completed_date, Comments, Is_contacted from Client_activity where Client_activity_id=@Client_activity_id

end
GO

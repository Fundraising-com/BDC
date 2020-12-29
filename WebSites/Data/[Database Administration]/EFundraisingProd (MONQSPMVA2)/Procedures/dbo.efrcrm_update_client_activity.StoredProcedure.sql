USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_client_activity]    Script Date: 02/14/2014 13:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Client_activity
CREATE PROCEDURE [dbo].[efrcrm_update_client_activity] @Client_activity_id int, @Client_id int, @Client_sequence_code char(2), @Client_activity_type_id tinyint, @Client_activity_date datetime, @Completed_date datetime, @Comments text, @Is_contacted bit AS
begin

update Client_activity set Client_id=@Client_id, Client_sequence_code=@Client_sequence_code, Client_activity_type_id=@Client_activity_type_id, Client_activity_date=@Client_activity_date, Completed_date=@Completed_date, Comments=@Comments, Is_contacted=@Is_contacted where Client_activity_id=@Client_activity_id

end
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Javier Arellano
-- Create date: May, 2014
-- Description: Finds all the testimonials for the FC given
-- =============================================
CREATE PROCEDURE rep_get_testimonials
    @id int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    SELECT TEST.*
    FROM dbo.FC (NOLOCK) FC
    JOIN dbo.fc_testimonial (NOLOCK) TEST ON FC.id = TEST.fc_id
    WHERE FC.ID = @id
END
GO
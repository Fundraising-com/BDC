
CREATE PROCEDURE sp_send_email_spam_report (@maxEmailsPerDay INT, @maxBounceRate DECIMAL)
-- The following SP sends an email when a criteria matches a potential email Scam.
-- i.e. if the Event has sent more than 1000 emails that day.
-- IMPORTANT NOTE: The following SP doesn't disable the event. The email is meant to warn the IT. The IT needs to decide if any further action is required.
-- @maxEmailPerDay defines the maximum amount of emails allowed per event per day.
-- @maxBounceRate defines the maximum bounce rate allowed per event per day.
-- Author: Javier Arellano
-- Date: January 5, 2016.
AS
BEGIN
   --DECLARATIONS
   DECLARE @exceptionMaxEmailsPerDay VARCHAR(200) = 'EMAIL_QUOTA_REACHED';
   DECLARE @exceptionMaxBounceRate VARCHAR(200) = 'BOUNCE_RATE_REACHED';
   DECLARE @eventsTableWithWarnings TABLE (EventId INT, [Exception] VARCHAR(200), [Description] VARCHAR(500));
   DECLARE @totalExceptions INT; -- If > 0, the email will be sent
   DECLARE @message NVARCHAR(MAX) = 'The following Events meet a criteria that flags them as potential spam, please review them.'+ char(13) + char(10) + char(13) + char(10) +
   'Be advised that you will receive the following email for the day as the events keep meeting the criteria.' + char(13) + char(10) + char(13) + char(10) + 'Event Id - Exception - Description' + char(13) + char(10);
   DECLARE @eventId INT; -- For the Cursor
   DECLARE @exception VARCHAR(200); -- For the Cursor
   DECLARE @description VARCHAR(500); -- For the Cursor

   -- EMAIL QUOTA PER DAY
   INSERT INTO @eventsTableWithWarnings
      SELECT
         EVENT_PARTICIPATION.event_id AS EventId,
         @exceptionMaxEmailsPerDay,
         CAST(COUNT(*) AS VARCHAR(7)) + ' emails sent'
      FROM touch AS TOUCH (NOLOCK)
      JOIN event_participation AS EVENT_PARTICIPATION (NOLOCK)
         ON TOUCH.event_participation_id = EVENT_PARTICIPATION.event_participation_id
      WHERE TOUCH.create_date BETWEEN CAST(GETDATE() AS DATE) AND GETDATE()
      GROUP BY EVENT_PARTICIPATION.event_id
      HAVING COUNT(*) > @maxEmailsPerDay;


   --BOUNCE RATE
   INSERT INTO @eventsTableWithWarnings
      SELECT
         EVENT_PARTICIPATION.event_id AS EventId,
         @exceptionMaxBounceRate,
         CAST(CAST(CAST(SUM(CASE
            WHEN TOUCH.processed <> 0 AND
               TOUCH.processed <> 2 THEN 1
            ELSE 0
         END) AS DECIMAL) / COUNT(*) * 100 AS DECIMAL(6, 1)) AS VARCHAR(5)) + '% bounce rate'
      FROM touch AS TOUCH (NOLOCK)
      JOIN event_participation AS EVENT_PARTICIPATION (NOLOCK)
         ON TOUCH.event_participation_id = EVENT_PARTICIPATION.event_participation_id
      WHERE TOUCH.create_date BETWEEN CAST(GETDATE() AS DATE) AND GETDATE()
      GROUP BY EVENT_PARTICIPATION.event_id
      HAVING SUM(CASE
         WHEN TOUCH.processed <> 0 AND
            TOUCH.processed <> 2 THEN 1
         ELSE 0
      END) > 0
      AND CAST(SUM(CASE
         WHEN TOUCH.processed <> 0 AND
            TOUCH.processed <> 2 THEN 1
         ELSE 0
      END) AS DECIMAL) / COUNT(*) * 100 > @maxBounceRate;

   SELECT
      @totalExceptions = COUNT(*)
   FROM @eventsTableWithWarnings;

   IF @totalExceptions > 0
      BEGIN
      DECLARE cursorException CURSOR FOR
      SELECT EventId, Exception, Description FROM @eventsTableWithWarnings;
      OPEN cursorException;
      FETCH NEXT FROM cursorException INTO @eventId, @exception, @description;
      WHILE @@fetch_status = 0
         BEGIN
         SET @message = @message + CAST(@eventId as NVARCHAR(20)) + ' - ' + @exception + ' - ' + @description + char(13) + char(10);
         FETCH NEXT FROM cursorException INTO @eventId, @exception, @description;
         END

      CLOSE cursorException;
      DEALLOCATE cursorException;

      EXEC msdb.dbo.sp_send_dbmail @profile_name = NULL,
                                   @recipients = 'BDCDevelopers@fundraising.com',
                                   @subject = '[EFUNDRAISING] [WARN] Possible Email Spam',
                                   @body = @message,
                                   @from_address = 'efrreporting@fundraising.com',
                                   @reply_to = 'dpettit@gafundraising.com'

      END
END
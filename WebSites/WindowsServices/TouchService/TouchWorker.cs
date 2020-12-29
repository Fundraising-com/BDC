//
// 2005-06-29 - Stephen Lim - New class.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Transactions;

using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.Email.MassMail;
using GA.BDC.Core.ESubsGlobal.Touch;
using GA.BDC.WindowsServices.TouchService.Properties;


namespace GA.BDC.Core.TouchService {

	public delegate void Message(string message);
	/// <summary>
	/// This function will execute and control all the touch requests
	/// </summary>
	internal class TouchWorker
    {


        #region Fields
        private bool firstRun = true;
		  private Hashtable throttleObjectCounter = new Hashtable();
        private int dueDayDefault = 1;
       

		public int FromRuleID = int.MinValue;
		public int ToRuleID = int.MaxValue;
        public int DueDay = int.MinValue;

		public event Message OnMessage;
		#endregion

		#region Constructors
		public TouchWorker() 
		{

			// Load configs
			try 
			{
				GlobalSettings.Lock();
				GlobalSettings.LoadSettingsFromConfig();
			}
			finally 
			{
				GlobalSettings.Unlock();
			}
		}
		#endregion

		#region Constants
		private const byte SOURCE_TYPE_ID = 1;
		private const int SOURCE_ID = 2;
     
		#endregion

		#region Methods
		/// <summary>
		/// Start the thread.
		/// </summary>
		public void Start() 
		{
			// Load all objects that need to throttle into our counter
			foreach (string key in GlobalSettings.ThrottleObjects.Keys)
			{
				throttleObjectCounter.Add(key,  GlobalSettings.ThrottleObjects[key]);
			}

			while (true) 
			{
				try 
				{
					if (! firstRun) 
					{
						// Throttle server by sleeping periodically.
						Thread.Sleep(TimeSpan.FromMilliseconds(GlobalSettings.QueueInterval));
					}
					else 
					{
						firstRun = false;
					}

			
					if (GlobalSettings.Enabled) 
					{
						try 
						{
							// Get TouchRules
							TouchRuleCollection touchRules = TouchRuleCollection.GetRules();				

							// Cycle through each Touch rule
							foreach (TouchRule tr in touchRules)
							{

								if(!(tr.RuleId >= FromRuleID && tr.RuleId <= ToRuleID)) 
                                {
									continue;
								}

								// Throttle Touch rule
								if (throttleObjectCounter.ContainsKey(tr.RuleId.ToString()))
								{
									// Countdown object
									int counter = (int) throttleObjectCounter[tr.RuleId.ToString()];
									if (--counter > 0)
									{
										// Set counter and skip this touch request
										throttleObjectCounter[tr.RuleId.ToString()] = counter;
										continue;
									}	
									else
									{
										// Reset counter
										throttleObjectCounter[tr.RuleId.ToString()] = GlobalSettings.ThrottleObjects[tr.RuleId.ToString()];
									}
								}

								// Log email activity
								Logger.LogInfo("Handling TouchRule.\r\n" + tr.ToXmlString());

								// Get list of emails for this Touch rule.
								List<TouchEmail> touchEmails = tr.GetEmails();				

								// Handle each touch email
								for (int j = 0; j < touchEmails.Count; j++) 
								{		
									TouchEmail te = touchEmails[j];
                                    if (checkOldEmail(te))
                                    {
                                        te.Processed = TouchProcessedStatus.LateEmail;
                                        te.UpdateProcessedStatus();
                                        Logger.LogWarn("Late email was detected:".ToString() + Environment.NewLine + te.ToXmlString());
                                        continue;
                                    }

									try 
									{
										// Log email activity
										Logger.LogInfo("Handling TouchEmail.\r\n" + tr.ToXmlString() + "\r\n" + te.ToXmlString());

										// Parse email
										te.LoadParamTags();
										te.ParseEmail();

										// Log email activity
										Logger.LogInfo("Parsing TouchEmail.\r\n" + tr.ToXmlString() + "\r\n" + te.ToXmlString());
									}
									catch (Exception ex)
									{
										Logger.LogError("Error parsing email.\r\n" + tr.ToXmlString() + "\r\n" + te.ToXmlString(), ex);
										HaltOnError();
									}


									if (te.IsParamTagFree)
									{
										try 
										{
											// Insert email into massmail (email_queue)
											Email.MassMail.Email e = new Email.MassMail.Email();
											e.SourceId = te.TouchId;
											e.ProjectId = SOURCE_ID;
											e.FromName = te.FromName;
											e.FromEmail = te.FromEmail;
											e.ToName = te.ToName;
											e.ToEmail = te.ToEmail;
											e.ReplyToName = te.ReplyToName;
											e.ReplyToEmail = te.ReplyToEmail;
											e.ReturnPathEmail = te.BounceEmail;
											e.Subject = te.Subject;
											e.HtmlBody = te.HtmlBody + te.HtmlFooter;
											e.TextBody = te.TextBody + te.TextFooter;
											e.ExtraInfo.Add("event_id", te.EventID.ToString());
											e.Priority =tr.PriorityLevel;
                                 e.ExtraInfo.Add("email_template_id", te.EmailTemplateID.ToString());
											//KO: no need as this is the default value -> //e.KomunikReturnValueId = efundraising.Email.MassMail.EmailQueue.KomunikReturnValue.New; 

                                 Email.MassMail.EmailQueue eq = Email.MassMail.EmailQueue.NewQueue(GlobalSettings.MailService);
                                 e.ProjectId = Settings.Default.AmazonProjectId;
                                 e.SentStatus = EmailQueue.EmailSentStatus.Amazon;
                                 eq.ServiceType = EmailQueue.MailService.Amazon;
                             
                                 using ( TransactionScope tc = new TransactionScope())
                                 {
                                    eq.EnQueue(Settings.Default.MassMailerConnectionString,e);
                                    Logger.LogInfo("Email Queued.\r\n" + tr.ToXmlString() + "\r\n" + te.ToXmlString());


                                    // Confirm sent status.
                                    te.Processed = TouchProcessedStatus.Sent;
                                    te.UpdateProcessedStatus();
                                    tc.Complete();

                                 }
											

										}
										catch (SqlException sqlEx)
										{
											try 
											{
												// Confirm sent status.
												te.Processed = TouchProcessedStatus.CommunicationError;
												te.UpdateProcessedStatus();
											}
											catch (Exception ex2)
											{
												Logger.LogError("Error updating touch status.\r\n" + tr.ToXmlString() + 
													"\r\n" + te.ToXmlString(), ex2);
												HaltOnError();
											}

											Logger.LogError("Error inserting into massmailer.\r\n" + tr.ToXmlString() + 
												"\r\n" + te.ToXmlString(), sqlEx);
											HaltOnError();
										}
										catch (SqlDataException sqlDataEx)
										{
											try 
											{
												// Confirm sent status.
												te.Processed = TouchProcessedStatus.CommunicationError;
												te.UpdateProcessedStatus();
											}
											catch (Exception ex2)
											{
												Logger.LogError("Error updating touch status.\r\n" + tr.ToXmlString() + 
													"\r\n" + te.ToXmlString(), ex2);
												HaltOnError();
											}

											Logger.LogError("Error inserting into massmailer.\r\n" + tr.ToXmlString() + 
												"\r\n" + te.ToXmlString(), sqlDataEx);
											HaltOnError();
										}
										catch (Exception ex)
										{
											try 
											{
												// Confirm sent status.
												te.Processed = TouchProcessedStatus.MailError;
												te.UpdateProcessedStatus();
											}
											catch (Exception ex2)
											{
												Logger.LogError("Error updating touch status.\r\n" + tr.ToXmlString() + 
													"\r\n" + te.ToXmlString(), ex2);
												HaltOnError();
											}

											Logger.LogError("Error inserting into massmailer.\r\n" + tr.ToXmlString() + 
												"\r\n" + te.ToXmlString(), ex);
											HaltOnError();
										}
									}
									else 
									{
										try 
										{
											// Error in mail.
											te.Processed = TouchProcessedStatus.MailError;
											te.UpdateProcessedStatus();
										}
										catch (Exception ex)
										{
											Logger.LogError("Error updating touch status.\r\n" + tr.ToXmlString() + 
												"\r\n" + te.ToXmlString(), ex);
											HaltOnError();
										}

										// Log email activity
										Logger.LogError("Param Tags found in email.\r\n" + tr.ToXmlString() + "\r\n" + te.ToXmlString());
										HaltOnError();
									}
								}	
							}
						}
						catch (Exception ex)
						{
							Logger.LogError(ex);
							HaltOnError();
						}
					}
				}
				catch (Exception ex)
				{
					Logger.LogError(ex);
					HaltOnError();
				}
			}
		}


		private void HaltOnError()
		{
			try 
			{
				// Halt service on error
				if (GlobalSettings.HaltOnError) 
				{
					Logger.LogWarn("Halting service.");
					TouchService.Stop();
				}
			}
			catch {}
		}

        private bool checkOldEmail(TouchEmail input)
        {
            if (input.LaunchDate != DateTime.MinValue)
            {
                if (((TimeSpan)(DateTime.Now - input.LaunchDate)).Days <= GlobalSettings.DaysDue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
		#endregion
	}
}

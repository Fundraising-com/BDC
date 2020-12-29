/* Title:	Stats
 * Author:	Jean-Francois Buist
 * Summary:	This object is a mini stat holder.  It counts certains vital values and
 *			save them to the ms windows performance counter every time an operation
 *			occurs and save/send data once a day to developpers.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// This is a singleton that logs errors in the performance counter of IIS.
	/// And sends daily emails to developers for activity information.
	/// </summary>
	public class EsubsDailyStat {
		private static EsubsDailyStat instance = null;
		private static readonly object padlock = new object();
		private System.Diagnostics.PerformanceCounter pf;

        private int day = int.MinValue;
		private const string VISIT = "Total Visits";
		private const string SPONSORS = "Sponsor visits";
		private const string PARTICIPANT = "Participant visits";
		private const string SUPPORTER = "Supporter visits";
		private const string UNKNOWN_USER = "Unknown user visits";
		private const string EMAIL_SENT_TO_SPONSOR = "Email sent to Supporter";
		private const string EMAIL_SENT_TO_PARTICIPANT = "Email sent to Supporter";
		private const string EMAIL_SENT_TO_SUPPORTER = "Email sent to Supporter";
		private const string APPLICATION_ERRORS = "Application Errors";

		private int numberVisits = 0;
		private int numberOfSponsorVisit = 0;
		private int numberOfParticipantVisit = 0;
		private int numberOfSupporterVisit = 0;
		private int numberOfUnknownVisit = 0;
		private int numberOfEmailSentToSponsor = 0;
		private int numberOfEmailSentToParticipant = 0;
		private int numberOfEmailSentToSupporter = 0;
		private int numberOfApplicationErrors = 0;

		public EsubsDailyStat()	{
			pf = new System.Diagnostics.PerformanceCounter();
			pf.CategoryName = "eSubsWeb";
		}

		public static EsubsDailyStat Instance {
			get {
				lock(padlock) {
					if(instance == null) {
						instance = new EsubsDailyStat();
					}
					return instance;
				}
			}
		}

		private void Increment(string counterName) {
			pf.CounterName = counterName;
			pf.InstanceName = counterName;
			pf.Increment();
		}

		public void AddVisit() {
			numberVisits++;
			Increment(VISIT);
		}

		public void AddNumberOfSponsor() {
			numberOfSponsorVisit++;
			Increment(SPONSORS);
		}

		public void AddNumberOfParticipant() {
			numberOfParticipantVisit++;
			Increment(PARTICIPANT);
		}

		public void AddNumberOfSupporter() {
			numberOfSupporterVisit++;
			Increment(SUPPORTER);
		}

		public void AddNumberOfUnknownUser() {
			numberOfUnknownVisit++;
			Increment(UNKNOWN_USER);
		}

		public void AddEmailSentToSponsor() {
			numberOfEmailSentToSponsor++;
			Increment(EMAIL_SENT_TO_SPONSOR);
		}

		public void AddEmailSentToParticipant() {
			numberOfEmailSentToParticipant++;
			Increment(EMAIL_SENT_TO_PARTICIPANT);
		}

		public void AddEmailSentToSupporter() {
			numberOfEmailSentToSupporter++;
			Increment(EMAIL_SENT_TO_SUPPORTER);
		}

		public void AddApplicationError() {
			numberOfApplicationErrors++;
			Increment(APPLICATION_ERRORS);
		}

		private void Reset() {
			numberVisits = 0;
			numberOfSponsorVisit = 0;
			numberOfParticipantVisit = 0;
			numberOfSupporterVisit = 0;
			numberOfUnknownVisit = 0;
			numberOfEmailSentToSponsor = 0;
			numberOfEmailSentToParticipant = 0;
			numberOfEmailSentToSupporter = 0;
			numberOfApplicationErrors = 0;
		}

		public void Check() {
			int nday = DateTime.Now.Day;
			if(nday != day) {
				// construct email and send
				// insert into database for graphs
				Reset();
			}
		}
	}
}

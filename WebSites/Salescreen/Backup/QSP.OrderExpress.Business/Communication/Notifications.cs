using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace QSPForm.Business.Communication
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Notifications : List<Notification>
    {

        #region Constructors
        
        /// <summary>
        /// Base constructor
        /// </summary>
        public Notifications() : base() 
        { 
        
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Determines if a Notifications collection is successful
        /// </summary>
        /// <returns>true if no errors are present, false otherwise</returns>
        public bool IsSuccessful()
        {
            bool isSuccessful = true;

            foreach (Notification notification in this)
            {
                if (notification.NotificationType == NotificationType.Error ||
                    notification.NotificationType == NotificationType.Warning)
                {
                    isSuccessful = false;
                    break;
                }
            }

            return isSuccessful;
        }

        /// <summary>
        /// Inserts the contents of a Notifications collection
        /// </summary>
        /// <param name="source">The collection to insert</param>
        public void InsertNotifications(Notifications source)
        {
            foreach (Notification sourceNotification in source)
            {
                Notification newNotification = new Notification();

                foreach (object sourceObject in sourceNotification.DynamicValues)
                {
                    newNotification.DynamicValues.Add(sourceObject);
                }
                
                newNotification.NotificationMessage = sourceNotification.NotificationMessage;
                newNotification.NotificationType = sourceNotification.NotificationType;

                this.Add(newNotification);
            }
        }

        #endregion

    }
}

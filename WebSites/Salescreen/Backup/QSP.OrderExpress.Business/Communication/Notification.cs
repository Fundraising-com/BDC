using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace QSPForm.Business.Communication
{

    #region Enums

    /// <summary>
    /// 
    /// </summary>
    public enum NotificationType
    {
        Error,
        Warning,
        Information
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    [DataContractAttribute]
    public class Notification
    {

        #region Fields

        private object notificationMessage;
        private List<object> dynamicValues;
        private NotificationType notificationType;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Notification() 
        {
 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationMessage"></param>
        /// <param name="dynamicValues"></param>
        /// <param name="notificationType"></param>
        public Notification(object notificationMessage, List<object> dynamicValues, NotificationType notificationType)
        {
            NotificationMessage = notificationMessage;
            DynamicValues = dynamicValues;
            NotificationType = notificationType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMemberAttribute]
        public object NotificationMessage
        {
            get
            {
                return notificationMessage;
            }
            set
            {
                notificationMessage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMemberAttribute]
        public List<object> DynamicValues
        {
            get
            {
                return dynamicValues;
            }
            set
            {
                dynamicValues = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMemberAttribute]
        public NotificationType NotificationType
        {
            get
            {
                return notificationType;
            }
            set
            {
                notificationType = value;
            }
        }

        #endregion
    }
}

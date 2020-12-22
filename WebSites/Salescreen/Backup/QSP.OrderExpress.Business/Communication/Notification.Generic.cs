using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace QSPForm.Business.Communication
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContractAttribute]
    public class Notification<T> : Notification
    {

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Notification() 
        { 
        
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationMessage"></param>
        /// <param name="dynamicValues"></param>
        /// <param name="notificationType"></param>
        public Notification(T notificationMessage, List<object> dynamicValues, NotificationType notificationType)
        {
            NotificationMessage = notificationMessage;
            DynamicValues = dynamicValues;
            NotificationType = notificationType;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMemberAttribute]
        public new T NotificationMessage
        {
            get
            {
                return (T)base.NotificationMessage;
            }
            set
            {
                base.NotificationMessage = value;
            }
        }

        #endregion

    }
}

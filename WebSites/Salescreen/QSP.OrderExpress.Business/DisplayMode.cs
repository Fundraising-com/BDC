using System;

namespace QSPForm.Business {
    /// <summary>
    /// Controls what results are shown, only for current field sales manager or 
    /// for it's assigned field sales managers.
    /// </summary>
    public enum DisplayMode {
        All = 0,
        Current = 1,
        ChildrenOnly = 2,
        CurrentAndChildren = 3
    }
}
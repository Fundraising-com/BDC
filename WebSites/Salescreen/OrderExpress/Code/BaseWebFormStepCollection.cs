using System;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseWebFormStepCollection.
    /// </summary>
    public class BaseWebFormStepCollection : System.Collections.CollectionBase, System.Collections.IEnumerable {
        public BaseWebFormStepCollection() {
        }

        public BaseWebFormStep this[int Index] {
            get { return ((BaseWebFormStep)List[Index]); }
            set { List[Index] = value; }
        }

        public int Add(BaseWebFormStep Value) {
            return (List.Add(Value));
        }

        public int IndexOf(BaseWebFormStep Value) {
            return (List.IndexOf(Value));
        }

        public void Insert(int index, BaseWebFormStep value) {
            List.Insert(index, value);
        }

        public void Remove(BaseWebFormStep value) {
            List.Remove(value);
        }

        public bool Contains(BaseWebFormStep value) {
            return (List.Contains(value));
        }

        public BaseWebFormStep FindByAppItem(QSPForm.Business.AppItem appItem) {
            BaseWebFormStep step = new BaseWebFormStep();
            bool found = false;

            for (int iIndex = 0; iIndex < this.Count; iIndex++) {
                BaseWebFormStep stepLoop = this[iIndex];
                if (stepLoop.StepItem == appItem) {
                    step = stepLoop;
                    found = true;
                    break;
                }
            }
            if (found)
                return step;
            else
                return null;
        }
    }
}
using System;

namespace BudgetsWPF.Navigation
{
    public interface INavigatable<Tobject> where Tobject : Enum
    {
        public Tobject Type { get; }

        public void ClearSensitiveData();
    }
}

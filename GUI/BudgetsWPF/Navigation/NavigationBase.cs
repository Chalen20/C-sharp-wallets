using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetsWPF.Navigation
{
    public abstract class NavigationBase<TObject> : BindableBase where TObject : Enum
    {
        private List<INavigatable<TObject>> _viewModels = new();

        public INavigatable<TObject> CurrentViewModel
        {
            get;
            private set;
        }
        protected NavigationBase()
        {
        }

        public void Navigate(TObject type)
        {
            if (CurrentViewModel != null && CurrentViewModel.Type.Equals(type))
                return;
            INavigatable<TObject> viewModel = _viewModels.FirstOrDefault(authNavigatable => authNavigatable.Type.Equals(type));
            /* foreach (var authNavigatable in _viewModels)
            {
                if (authNavigatable.Type == type)
                {
                    viewModel = authNavigatable;
                    break;
                }
            }*/
            if (viewModel == null)
            {
                viewModel = CreateViewModel(type);
                _viewModels.Add(viewModel);
            }
            viewModel.ClearSensitiveData();
            CurrentViewModel = viewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        protected abstract INavigatable<TObject> CreateViewModel(TObject type);
    }
}

using System;
using System.ComponentModel;
using Stations.Model;
using Stations.Service;
using Xamarin.Forms;

namespace Stations.Viewmodel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDatasource<Station> Datasource => DependencyService.Get<IDatasource<Station>>();

		string _title = string.Empty;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		bool _isBusy = false;
		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				OnPropertyChanged(nameof(IsBusy));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}

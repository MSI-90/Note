using Note.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Note.ViewModel
{
    
    internal class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        DataOutput dataOutput;
        public ObservableCollection<string> issues { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string statusOneText { get; set; }
        public string statusTwoText { get; set; }

        public List<string> data { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        ~MainViewModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
            //освобождение управляемх ресурсов
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Raindrops.ViweModels
{
    class Base : INotifyPropertyChanged
    {
        private bool Isbusy { get; set; }
        public async Task RunIsBusyTaskAsync(Func<Task> awaitableTask)
        {
            if (Isbusy)
                return;
            Isbusy = true;
            try
            {
                await awaitableTask();
            }
            finally
            {
                Isbusy = false;
            }
        }
        protected void OnpropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

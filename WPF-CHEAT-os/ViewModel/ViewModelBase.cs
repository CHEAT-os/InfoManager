﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.ViewModel
{
    public class ViewModelBase : ObservableObject
    {
        public virtual Task LoadAsync() => Task.CompletedTask;
    }
}

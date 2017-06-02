using MyNote.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNote.Web.ViewModels
{
    public class WarningViewModel: NotifyViewModelBase<string>
    {
        public WarningViewModel()
        {
            Title = "Uyarı...";
        }
    }
}
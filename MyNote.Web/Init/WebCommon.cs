using MyNote.BussinessLayer;
using MyNote.Common;
using MyNote.Enties;
using MyNote.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNote.Web.Init
{
    public class WebCommon : ICommon
    {
        public string GetUserName()
        {
            if(CurrentSession.User !=null)
            {
                NoteUser user = CurrentSession.User;
                return user.Username;
            }
           
                return "system";
           
        }
    }
}
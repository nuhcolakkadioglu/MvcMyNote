using MyNote.BussinessLayer;
using MyNote.Common;
using MyNote.Enties;
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
            if(HttpContext.Current.Session["login"] !=null)
            {
                BussinesLayerResult<NoteUser> user = HttpContext.Current.Session["login"] as BussinesLayerResult<NoteUser>;
                return user.Result.Username;
            }
           
                return "system";
           
        }
    }
}
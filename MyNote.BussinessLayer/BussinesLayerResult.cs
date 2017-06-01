using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.BussinessLayer
{
  public  class BussinesLayerResult<T> where T:class
    {
        public List<string> Errors { get; set; }
        public T Result { get; set; }
    }
}

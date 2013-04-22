using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGEB.Model
{
    public partial class Rg
    {
        public Rg()
        {
            this.BornDate = DateTime.Now;
            this.EmittedDate = DateTime.Now;
        }
    }

    public partial class Cnh
    {
        public Cnh()
        {
            this.EmittedDate = DateTime.Now;
            this.DueDate = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edit_Log_winforms.Exceptions
{
    public class SpaceAtEndException : Exception
    {
        public SpaceAtEndException() : base("ignoring space at end")
        {
        }
    }
}

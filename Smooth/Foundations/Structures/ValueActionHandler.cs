using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smooth.Foundations.Foundations.Structures
{
   public class ValueActionHandler<T1>
    {
       public ValueOrErrorExecMatcher<T1> Do(Action<T1> action)
       {
           return null;
       }

        public ValueOrErrorExecMatcher<T1> Do(Action action)
        {
            return null;
        }
    }
}

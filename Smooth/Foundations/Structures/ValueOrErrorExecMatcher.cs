using System;
using Smooth.Delegates;
using Smooth.Foundations.PatternMatching;

namespace Smooth.Foundations.Foundations.Structures
{
    public class ValueOrErrorExecMatcher<T1>
    {
        private readonly ValueOrErrorMatchActionSelector<T1> _actionSelector;
        private readonly ValueOrError<T1> _item;

        internal ValueOrErrorExecMatcher(ValueOrError<T1> item)
        {
            _item = item;
//            _actionSelector = new MatchActionSelector<T1>(
//                x => { throw new NoMatchException($"No match action exists for value of {_item}"); });
        }



        public ValuePredicateHandler<T1> OnValue()
        {
            return null;
        }

        public ValueActionHandler<T1> OnOtherValues()
        {
            return null;
        }

        public ValueActionHandler<T1> OnError()
        {
            return null;
        }

        public void Exec()
        {
            
        }






   
//        public void Exec() => _actionSelector.InvokeMatchedActionUsingDefaultIfRequired(_item);


    }
}

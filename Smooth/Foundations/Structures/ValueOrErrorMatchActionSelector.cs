using System;
using System.Collections.Generic;
using Smooth.Algebraics;
using Smooth.Delegates;
using Smooth.Slinq;

namespace Smooth.Foundations.Foundations.Structures
{
    public class ValueOrErrorMatchActionSelector<T1>
    {
        private readonly Action _matchNotFoundAction;
        private Option<Action<string>> _onErrorAction = Option<Action<string>>.None;
        private Option<Action<T1>> _onValueDefaultAction = Option<Action<T1>>.None;

        private readonly List<Tuple<DelegateFunc<T1, bool>, Action<T1>>> _testsAndActions =
            new List<Tuple<DelegateFunc<T1, bool>, Action<T1>>>();

        public ValueOrErrorMatchActionSelector(Action matchNotFoundAction)
        {
            _matchNotFoundAction = matchNotFoundAction;
        }

        public void SetErrorAction(Action<string> action) =>
            _onErrorAction = new Option<Action<string>>(action);

        public void SetDefaultOnValueAction(Action<T1> action) =>
            _onValueDefaultAction = new Option<Action<T1>>(action);

        public void AddPredicateAndAction(DelegateFunc<T1, bool> test, Action<T1> action) =>
            _testsAndActions.Add(new Tuple<DelegateFunc<T1, bool>, Action<T1>>(test, action));

        public void InvokeMatchedActionUsingDefaultIfRequired(ValueOrError<T1> inputArgument)
        {
            if (inputArgument.IsError)
            {
                if (_onErrorAction.isSome)
                {
                    _onErrorAction.value(inputArgument.Error);
                }
                else
                {
                    _matchNotFoundAction();
                }
            }
            else
            {
                var action =
                    _testsAndActions
                        .Slinq()
                        .FirstOrNone((matcher, param) => matcher.Item1(param.Value), inputArgument);

                if (action.isSome)
                {
                    action.value.Item2(inputArgument.Value);
                }
                else if (_onValueDefaultAction.isSome)
                {
                    _onValueDefaultAction.value(inputArgument.Value);
                }
                else
                {
                    _matchNotFoundAction(); 
                }
            }
        }
    }
}
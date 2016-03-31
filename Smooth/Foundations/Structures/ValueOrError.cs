using System;

namespace Smooth.Foundations.Foundations.Structures
{
    public struct ValueOrError<T>
    {
        public T Value
        {
            get
            {
                if (IsError)
                {
                    throw new InvalidOperationException();
                }
                return _value;
            }
        }

        public bool IsError { get; }

        public string Error { get; }

        private readonly T _value;

        public static ValueOrError<T> FromValue(T value)
        {
            return new ValueOrError<T>(value, string.Empty, false);
        }

        public static ValueOrError<T> FromError(string error)
        {
            return new ValueOrError<T>(default(T), error, true);
        }

        private ValueOrError(T value, string error, bool isError)
        {
            _value = value;
            Error = error;
            IsError = isError;
        }


        public void Test()
        {
            var testValue = ValueOrError<int>.FromValue(42);
            testValue.Match()
                .OnValue().Where(i => i == 0).Do(_ => Console.WriteLine("zero"))
                .OnValue().Where(i => i == 1).Do(_ => Console.WriteLine("one"))
                .OnOtherValues().Do(i => Console.WriteLine("other int"))
                .OnError().Do(() => Console.WriteLine("some error has occured"))
                .Exec();
        }


        public ValueOrErrorExecMatcher<T> Match()
        {
            return new ValueOrErrorExecMatcher<T>(this);
        }

        public ValueOrError<T> OnError(Func<T, bool> predicate, Action<T> action)
        {
            if (!IsError && predicate(Value))
            {
                action(Value);
            }
            return this;
        }


        public ValueOrError<TResult> ContinueWith<TResult>(Func<ValueOrError<TResult>> func)
        {
            return IsError
                ? ValueOrError<TResult>.FromError(Error)
                : func();
        }
    }
}
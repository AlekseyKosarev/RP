using System;
using System.Collections.Generic;

namespace RP.Core
{
    public class RP<T>
    {
        private T _value;
        private readonly List<Action<T>> _observers = new();

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value)) return;
                _value = value;
                foreach (var observer in _observers)
                    observer?.Invoke(value);
            }
        }

        public RP(T initialValue)
        {
            _value = initialValue;
        }

        public IDisposable Subscribe(Action<T> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            _observers.Add(observer);
            observer(_value); // ← Guaranteed delivery on subscribe!
            return new Subscription<T>(this, observer);
        }

        private class Subscription<TInner> : IDisposable
        {
            private readonly RP<TInner> _property;
            private readonly Action<TInner> _observer;

            public Subscription(RP<TInner> property, Action<TInner> observer)
            {
                _property = property;
                _observer = observer;
            }

            public void Dispose()
            {
                _property?._observers.Remove(_observer);
            }
        }
    }
}
using System;

namespace RP
{
    public class RP<T>
    {
        private T _value;
        private readonly Func<T, T> _processor;
        private event Action<T> onChanged;

        public RP(T initialValue = default, Func<T, T> processor = null)
        {
            _processor = processor;
            _value = initialValue;
        }

        public event Action<T> OnChanged
        {
            add
            {
                onChanged += value;
                value?.Invoke(_value);
            }
            remove => onChanged -= value;
        }

        public void Set(T value)
        {
            if (_processor != null)
            {
                value = _processor(value);
            }

            if (Equals(_value, value))
                return;

            _value = value;
            onChanged?.Invoke(value);
        }

        private T Get() => _value;
    
        public static implicit operator T(RP<T> property) => property.Get();
    }
}
using System;

namespace MyPlaces.ViewModels
{
    class Handleable
    {
        readonly Func<bool> _get;
        readonly Action<bool> _set;

        public Handleable(Func<bool> get, Action<bool> set)
        {
            _get = get;
            _set = set;
        }

        public bool Handled
        {
            get => _get();
            set => _set(value);
        }
    }
}

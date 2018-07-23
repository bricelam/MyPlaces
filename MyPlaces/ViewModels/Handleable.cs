using System;

namespace MyPlaces.ViewModels
{
    class Handleable
    {
        readonly Action _action;

        public Handleable(Action action)
            => _action = action;

        public void Handle()
            => _action();
    }
}

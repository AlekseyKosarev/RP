using System;
using System.Collections.Generic;
using UnityEngine;

namespace RP.Core
{
    /// <summary>
    /// Extension for auto-dispose subscriptions in MonoBehaviour.
    /// Usage: subscription.AddTo(this);
    /// </summary>
    public static class IDisposableExtensions
    {
        public static T AddTo<T>(this T disposable, MonoBehaviour behaviour) where T : IDisposable
        {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));
            if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

            behaviour.gameObject.AddComponent<AutoDisposeHelper>().Add(disposable);
            return disposable;
        }

        private class AutoDisposeHelper : MonoBehaviour
        {
            private readonly List<IDisposable> _disposables = new();

            public void Add(IDisposable disposable) => _disposables.Add(disposable);

            private void OnDestroy()
            {
                foreach (var disposable in _disposables)
                    disposable?.Dispose();
                _disposables.Clear();
            }
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Util {
    public class ConsistentCoroutine {

        private readonly MonoBehaviour _attachedMonoBehaviour;
        private IEnumerator _coroutineReference = null;

        private bool IsRunning => _coroutineReference != null; 

        public ConsistentCoroutine (MonoBehaviour attachedMonoBehaviour) {
            _attachedMonoBehaviour = attachedMonoBehaviour;
        }

        public bool StartIfNotRunning (IEnumerator coroutine) {
            if (IsRunning) return false;

            _coroutineReference = coroutine;
            _attachedMonoBehaviour.StartCoroutine(_coroutineReference);
            return true;
        }

        public bool StartAndInterruptIfRunning (IEnumerator coroutine) {
            var wasRunningCoroutineInterrupted = StopIfRunning();
            StartIfNotRunning(coroutine);
            return wasRunningCoroutineInterrupted;
        }

        public bool StopIfRunning () {
            if (!IsRunning) return false;
            
            _attachedMonoBehaviour.StopCoroutine(_coroutineReference);
            _coroutineReference = null;
            return true;
        }

    }
}
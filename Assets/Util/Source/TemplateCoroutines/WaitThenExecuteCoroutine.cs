using System;
using System.Collections;
using UnityEngine;

namespace Util.TemplateCoroutines {
    public class WaitThenExecuteCoroutine {

        private readonly float _waitTime;
        private readonly Action _callback;

        public WaitThenExecuteCoroutine (float waitTime, Action callback) {
            _waitTime = waitTime;
            _callback = callback;
        }

        public IEnumerator Start () {
            yield return new WaitForSeconds(_waitTime);
            _callback.Invoke();
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorldLevelSelect
{
    public class UILevelSelectItem : MonoBehaviour {
        [SerializeField] private Text text;

        public void ApplyData (KingdomSelectItemInfo data) {
            text.text = data.KingdomName;
        }
    }
}

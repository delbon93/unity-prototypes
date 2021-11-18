using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorldLevelSelect
{
    public class UILevelSelectItem : MonoBehaviour {
        [SerializeField] private Text text;
        [SerializeField] private Image dot;
        [SerializeField] private Image padlock;
        
        [SerializeField] private bool locked;
        
        public bool Locked {
            get => locked;
            set {
                locked = value;
                dot.enabled = !locked;
                padlock.enabled = locked;
            }
        }

        public void ApplyData (KingdomSelectItemInfo data) {
            text.text = data.KingdomName;
        }
    }
}

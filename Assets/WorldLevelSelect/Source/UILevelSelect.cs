using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace WorldLevelSelect
{
    public class UILevelSelect : MonoBehaviour {
        private const float BarBaseWidth = 27;
        private const float ItemSpacing = 55;
        
        [SerializeField] private Image bar;
        [SerializeField] private Transform items;
        [SerializeField] private UILevelSelectItem itemPrefab;
        [SerializeField] private UILevelSelectItem highlightItemPrefab;
        [SerializeField] private List<KingdomSelectItemInfo> kingdomItemInfo;
        [SerializeField] private UIPreviewImage previewImage;
        [SerializeField] private WorldSphere worldSphere;

        private int _selectedIndex = 0;

        private KingdomSelectItemInfo SelectedItemInfo => kingdomItemInfo[_selectedIndex];

        private void Start () {
            worldSphere.CreateGlobeLocations(kingdomItemInfo);
            SetData();
        }

        private void SetData () {
            var barWidth = BarBaseWidth + ItemSpacing * (kingdomItemInfo.Count - 1);
            bar.rectTransform.sizeDelta = new Vector2(barWidth, bar.rectTransform.sizeDelta.y);

            foreach (Transform child in items) {
                Destroy(child.gameObject);
            }

            for (var index = 0; index < kingdomItemInfo.Count; index++) {
                var itemInfo = kingdomItemInfo[index];
                var uiItemPrefab = index == _selectedIndex ? highlightItemPrefab : itemPrefab;
                AddKingdomSelectItemInfo(index * ItemSpacing, itemInfo, uiItemPrefab);
            }
            
            SetPreviewImage();
            worldSphere.RotateToShowLocation(SelectedItemInfo);
        }

        private void SetPreviewImage () {
            previewImage.SetPreviewTexture(SelectedItemInfo.PreviewImage);
        }

        private void AddKingdomSelectItemInfo (float offset, KingdomSelectItemInfo dataPoint, UILevelSelectItem prefab) {
            var item = Instantiate(prefab, items);
            item.Locked = (dataPoint.OnSelectedCallback.GetPersistentEventCount() == 0);
            var rectTransform = item.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + offset, 0);
            item.ApplyData(dataPoint);
        }

        public void OnSelectionLeft (InputAction.CallbackContext context) {
            if (!context.performed) return;
            
            _selectedIndex = _selectedIndex == 0 ? kingdomItemInfo.Count - 1 : _selectedIndex - 1;
            SetData();
        }

        public void OnSelectionRight (InputAction.CallbackContext context) {
            if (!context.performed) return;

            _selectedIndex = (_selectedIndex + 1) % kingdomItemInfo.Count;
            SetData();
        }
        
        public void OnConfirmSelection (InputAction.CallbackContext context) {
            if (!context.performed) return;

            SelectedItemInfo.OnSelectedCallback?.Invoke();
        }
    }
}

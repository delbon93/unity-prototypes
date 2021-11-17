using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private void Start () {
            worldSphere.CreateGlobeLocations(kingdomItemInfo);
            SetData();
        }

        private void Update () {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                SelectionLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                SelectionRight();
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                ConfirmSelection();
                print("confirm");
            }
        }

        private void ConfirmSelection () {
            kingdomItemInfo[_selectedIndex].OnSelectedCallback.Invoke();
        }


        private void SetData () {
            var barWidth = BarBaseWidth + ItemSpacing * (kingdomItemInfo.Count - 1);
            bar.rectTransform.sizeDelta = new Vector2(barWidth, bar.rectTransform.sizeDelta.y);

            foreach (Transform child in items) {
                Destroy(child.gameObject);
            }

            for (var index = 0; index < kingdomItemInfo.Count; index++) {
                var dataPoint = kingdomItemInfo[index];
                AddKingdomSelectItemInfo(index * ItemSpacing, dataPoint,
                    index == _selectedIndex ? highlightItemPrefab : itemPrefab);
            }
            
            SetPreviewImage();
            worldSphere.RotateToShowLocation(_selectedIndex);
        }

        private void SetPreviewImage () {
            previewImage.SetPreviewTexture(kingdomItemInfo[_selectedIndex].PreviewImage);
        }

        private void AddKingdomSelectItemInfo (float offset, KingdomSelectItemInfo dataPoint, UILevelSelectItem prefab) {
            var item = Instantiate(prefab, items);
            var rectTransform = item.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + offset, 0);
            item.ApplyData(dataPoint);
        }

        private void SelectionLeft () {
            _selectedIndex = _selectedIndex == 0 ? kingdomItemInfo.Count - 1 : _selectedIndex - 1;
            SetData();
        }

        private void SelectionRight () {
            _selectedIndex = (_selectedIndex + 1) % kingdomItemInfo.Count;
            SetData();
        }
        
    }
}

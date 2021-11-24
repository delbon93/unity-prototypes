using UnityEditor;
using UnityEngine;

namespace DungeonCrawler {
    [CustomEditor(typeof(DungeonRoom))]
    public class DungeonRoom_Editor : Editor {
        public override void OnInspectorGUI () {
            /*
            if (GUILayout.Button("Set As Active Room")) {
                ((DungeonRoom)target)?.GetComponentInParent<DungeonRoomContainer>().
                    SetActiveRoom(((DungeonRoom)target)?.gameObject);
            }
            */
            base.OnInspectorGUI();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorldLevelSelect
{
    public class SceneLoader : MonoBehaviour {

        public void LoadScene (int buildIndex) {
            SceneManager.LoadScene(buildIndex);
        }
    }
}

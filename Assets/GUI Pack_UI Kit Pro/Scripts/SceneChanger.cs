using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUIPack_CasualGame
{
    public class SceneChanger : MonoBehaviour
    {
        public string SceneName;
        public void SceneChange()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPack_CasualGame
{
    public class Loading : MonoBehaviour
    {
        public Image loadingFill;
        public GameObject MainScreen;


        private void Start()
        {
            loadingFill.fillAmount = 0f;
            Invoke("Delay", 0.2f);

        }
        void Delay()
        {
            StartCoroutine(loading());
        }
        IEnumerator loading()
        {
            while (loadingFill.fillAmount < 1)
            {
                loadingFill.fillAmount += (0.01f / 10f);
                yield return null;
            }
            StartGame();
            yield return 0;
        }

        void StartGame()
        {
            MainScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

}


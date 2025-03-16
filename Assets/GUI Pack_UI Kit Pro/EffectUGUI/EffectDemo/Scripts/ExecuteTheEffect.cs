using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GUIPack_CasualGame
{
    public class ExecuteTheEffect : MonoBehaviour
    {
        public GameObject MainEffect;
        public GameObject Effect;
        public GameObject Effect_02;
        public GameObject SuccessEffectText;
        public TextMeshProUGUI tex;

        public GameObject FailedEffect;
         public GameObject FailedEffectText;

        public void PlayFailedEffect()
        {
            MainEffect.SetActive(true);
            FailedEffect.SetActive(false);
            Effect.SetActive(false);
            Effect_02.SetActive(false);
            SuccessEffectText.SetActive(false);
            FailedEffectText.SetActive(false);
            tex.enabled = false;
            StartCoroutine(waitForFailedEffectToPlay());
        }

        public void PlayEffect()
        {
            MainEffect.SetActive(true);
            FailedEffect.SetActive(false);
            FailedEffectText.SetActive(false);
            SuccessEffectText.SetActive(false);
            Effect.SetActive(false);
            Effect_02.SetActive(false);
            tex.enabled = false;
            StartCoroutine(waitForEffectToPlay());
        }

        IEnumerator waitForFailedEffectToPlay()
        {

            yield return new WaitForSeconds(1.5f);

            MainEffect.SetActive(false);

            if (FailedEffect != null)
            {
                FailedEffect.SetActive(true);
                FailedEffectText.SetActive(true);
            }
        }

        IEnumerator waitForEffectToPlay()
        {
           
            yield return new WaitForSeconds(1.5f);

            MainEffect.SetActive(false);

            if (Effect != null && SuccessEffectText!=null)
            {
                Effect.SetActive(true);
                SuccessEffectText.SetActive(true);
            }
            if (Effect_02 != null)
            {
                Effect_02.SetActive(true);
            }

            if(tex != null)
            {
                tex.enabled = true;
            }

        }
       
    }
}


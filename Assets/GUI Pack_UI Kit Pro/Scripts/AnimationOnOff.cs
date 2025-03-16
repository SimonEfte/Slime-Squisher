using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPack_CasualGame
{
    public class AnimationOnOff : MonoBehaviour
    {
        private Animator animationOpen;

        public GameObject obj;


        private void Awake()
        {
            animationOpen = obj.GetComponent<Animator>();
        }

        public void PlayOn()
        {
            animationOpen.SetTrigger("isOpen");
            //animationOpen.SetBool("isOpen", true);
        }

        public void PlayBoolOn()
        {
            animationOpen.SetBool("isOpen", true);
        }

        public void PlayBoolOff()
        {
            animationOpen.SetBool("isOpen", false);
        }
    }
}


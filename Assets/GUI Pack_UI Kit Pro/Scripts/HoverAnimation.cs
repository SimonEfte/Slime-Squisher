using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUIPack_CasualGame
{
    public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public AudioManager audioManager;
        public GameObject audioGameobject;

        [Header("Increase Scale")]
        public float maxScale = 1.2f;

        [Header("Initial  Scale")]
        public float defaultScale = 1f;
        [Header("Animation speed")]
        public float animationSpeed = 2;

        [Header("Enable clickAnimation")]
        public bool animateOnClick = false;

        [Header("Enable Hover Animation")]
        public bool animateOnHover = true;

        // used for click effect
        bool isRunningI = false;
        bool isRunningD = false;

        public bool isAntiSlimeUpgrade;

        private void Awake()
        {
            audioGameobject = GameObject.Find("AudioManager");
            audioManager = audioGameobject.GetComponent<AudioManager>();

            defaultScale = gameObject.transform.localScale.x;
            maxScale = defaultScale * 1.13f;

            if(isAntiSlimeUpgrade)
            {
                maxScale = defaultScale * 1.06f;
            }
        }

        private void OnEnable()
        {
            gameObject.transform.localScale = new Vector2(defaultScale, defaultScale);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!animateOnHover)
                return;

            int random = Random.Range(1,3);
            if(random == 1) { audioManager.Play("HoverUI_2"); }
            if (random == 2) { audioManager.Play("HoverUI_3"); }


            StopAllCoroutines();
            StartCoroutine(IncreaseSize());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (gameObject.name == "resolutionText")
            {
                return;
            }

            if (!animateOnHover)
                return;

            StopAllCoroutines();
            StartCoroutine(DecreaseSize());
        }

        IEnumerator IncreaseSize()
        {
            Vector3 finalScale = new Vector3(maxScale, maxScale, maxScale);
            Vector3 initialScale = new Vector3(defaultScale, defaultScale, defaultScale);

            float scaleFactor = Time.unscaledDeltaTime * animationSpeed;

            while (transform.localScale.x < maxScale)
            {
                float x = transform.localScale.x + (scaleFactor);
                float y = transform.localScale.y + (scaleFactor);
                float z = transform.localScale.z + (scaleFactor);

                x = Mathf.Clamp(x, initialScale.x, finalScale.x);
                y = Mathf.Clamp(y, initialScale.x, finalScale.y);
                z = Mathf.Clamp(z, initialScale.x, finalScale.z);

                transform.localScale = new Vector3(x, y, z);
                isRunningI = true;
                yield return null;
            }
            isRunningI = false;

            yield return 0;
        }
        IEnumerator DecreaseSize()
        {

            Vector3 finalScale = new Vector3(maxScale, maxScale, maxScale);
            Vector3 initialScale = new Vector3(defaultScale, defaultScale, defaultScale);

            float scaleFactor = 0.02f * animationSpeed;

            while (transform.localScale.x > defaultScale)
            {
                float x = transform.localScale.x - (scaleFactor);
                float y = transform.localScale.y - (scaleFactor);
                float z = transform.localScale.z - (scaleFactor);

                x = Mathf.Clamp(x, initialScale.x, finalScale.x);
                y = Mathf.Clamp(y, initialScale.x, finalScale.y);
                z = Mathf.Clamp(z, initialScale.x, finalScale.z);

                transform.localScale = new Vector3(x, y, z);
                isRunningD = true;

                yield return null;
            }
            isRunningD = false;

            yield return 0;
        }


        IEnumerator clickEffect()
        {
            StartCoroutine(IncreaseSize());
            while (isRunningI)
            {
                yield return null;
            }
            StartCoroutine(DecreaseSize());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!animateOnClick)
                return;

            if (isRunningI || isRunningD)
                return;
            StartCoroutine(clickEffect());
        }
    }
}


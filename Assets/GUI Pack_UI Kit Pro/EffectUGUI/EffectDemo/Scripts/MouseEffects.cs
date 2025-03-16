using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GUIPack_CasualGame
{
    public class MouseEffects : MonoBehaviour
    {
        public static MouseEffects Instance;

        public GameObject TouchEffectPrefab;
        public GameObject TouchEffectPrefab_02;
        float spawnsTime;
        public float defaultTime = 0.05f;


        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0) && spawnsTime >= defaultTime)
            {
                StarCreat();
                spawnsTime = 0;
            }
            spawnsTime += Time.deltaTime;
        }

        void StarCreat()
        {
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mPosition.z = 0;
            Instantiate(TouchEffectPrefab, mPosition, Quaternion.identity);
            Instantiate(TouchEffectPrefab_02, mPosition, Quaternion.identity);
        }
    }
}


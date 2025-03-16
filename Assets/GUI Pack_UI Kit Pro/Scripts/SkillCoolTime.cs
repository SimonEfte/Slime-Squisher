using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GUIPack_CasualGame
{
    public class SkillCoolTime : MonoBehaviour
    {
        public GameObject[] hideSkillButtons;
        public GameObject[] texPros;
        public TextMeshProUGUI[] hideSkillTimeTexts;
        public Image[] hideSkillImages;
        private bool[] isHideSkills = {false,false,false,false, false, false, false };
        private float[] skillTimes = { 10, 15, 20, 25,8,5,9};
        private float[] getSkillTimes = { 0, 0, 0, 0 ,0,0,0};

        // Start is called before the first frame update
        void Start()
        {
            for(int i=0; i< texPros.Length; i++)
            {
                hideSkillTimeTexts[i] = texPros[i].GetComponent<TextMeshProUGUI>();
                hideSkillButtons[i].SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            HideSkillChk();
        }

        public void HideSkillSetting(int skillNum)
        {
            hideSkillButtons[skillNum].SetActive(true);
            getSkillTimes[skillNum] = skillTimes[skillNum];
            isHideSkills[skillNum] = true;
        }

        private void HideSkillChk()
        {
            if (isHideSkills[0])
            {
                StartCoroutine(SkillTimeChk(0));
            }

            if (isHideSkills[1])
            {
                StartCoroutine(SkillTimeChk(1));
            }

            if (isHideSkills[2])
            {
                StartCoroutine(SkillTimeChk(2));
            }

            if (isHideSkills[3])
            {
                StartCoroutine(SkillTimeChk(3));
            }
            if (isHideSkills[4])
            {
                StartCoroutine(SkillTimeChk(4));
            }
            if (isHideSkills[5])
            {
                StartCoroutine(SkillTimeChk(5));
            }
            if (isHideSkills[6])
            {
                StartCoroutine(SkillTimeChk(6));
            }
        }

        IEnumerator SkillTimeChk(int skillNum)
        {
            yield return null;

            if (getSkillTimes[skillNum] > 0)
            {
                getSkillTimes[skillNum] -= Time.deltaTime;

                if (getSkillTimes[skillNum] < 0)
                {
                    getSkillTimes[skillNum] = 0;
                    isHideSkills[skillNum] = false;
                    hideSkillButtons[skillNum].SetActive(false);
                }

                hideSkillTimeTexts[skillNum].text = getSkillTimes[skillNum].ToString("00");

                float time = getSkillTimes[skillNum] / skillTimes[skillNum];
                hideSkillImages[skillNum].fillAmount = time;
            }
        }
    }

}


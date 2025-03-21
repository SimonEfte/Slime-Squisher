using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace GUIPack_CasualGame
{
    public class PanelHandler : MonoBehaviour
    {
        void Start()
        {
            DOTween.Init();
            transform.localScale = Vector3.one * 0.1f;
      
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            var seq = DOTween.Sequence();

            seq.Append(transform.DOScale(1.1f, 0.2f));
            seq.Append(transform.DOScale(1f, 0.1f));

            seq.Play();
        }

        public void Hide()
        {
            var seq = DOTween.Sequence();

           // transform.localScale = Vector3.one * 0.2f;

            seq.Append(transform.DOScale(1.1f, 0.1f));
            seq.Append(transform.DOScale(0.2f, 0.2f));

    
            seq.Play().OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

    }
}

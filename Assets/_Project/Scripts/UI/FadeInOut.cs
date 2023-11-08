using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WorldGeneration
{
    public class FadeInOut : MonoBehaviour
    {
        [SerializeField] private Image fadeInOutImage;
        [SerializeField] private GameObject loadingText;

        private void OnEnable()
        {
            EventSystem.OnPlayerSpawned += FadeOut;
        }

        private void OnDisable()
        {
            EventSystem.OnPlayerSpawned -= FadeOut;
        }

        private void FadeOut(Transform _player)
        {
            fadeInOutImage.DOFade(0f, 1f);
            loadingText.SetActive(false);
        }

        private void FadeIn()
        {
            fadeInOutImage.DOFade(1f, 1f);
            loadingText.SetActive(true);
        }
    }
}


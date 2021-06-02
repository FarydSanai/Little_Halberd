﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class BackgroundMusic : MonoBehaviour
    {
        public static BackgroundMusic Instance;

        [SerializeField] private AudioClip BossFightAudioClip;
        [SerializeField] private AudioClip EndingAudioClip;
        private AudioSource audioSource;
        private void Awake()
        {
            Instance = this;
            audioSource = this.GetComponent<AudioSource>();
        }
        public void SetBossAudioClip()
        {
            audioSource.clip = BossFightAudioClip;
            audioSource.enabled = false;
            audioSource.enabled = true;
        }
        public void SetEndingAudioClip()
        {
            audioSource.clip = EndingAudioClip;
            audioSource.enabled = false;
            audioSource.enabled = true;
        }
    }
}
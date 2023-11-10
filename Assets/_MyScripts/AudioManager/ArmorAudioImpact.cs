using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using _MyScripts.Combat;

namespace _MyScripts.AudioManager
{
    public class ArmorAudioImpact : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> impactList = new List<AudioClip>();
        [SerializeField] private List<AudioClip> dieClip = new List<AudioClip>();
        [SerializeField] private List<AudioClip> blockClips = new List<AudioClip>();
    
        private Damageable _damageable;
        [SerializeField] private AudioSource impactAudioSource;
    
        private void Start()
        {
            _damageable = GetComponent<Damageable>();
            // This check is for player, who got another audiosource for music
            // player impactSource will be set in Editor
            if (impactAudioSource == null)
            {
                impactAudioSource = GetComponent<AudioSource>();
            }
            _damageable.OnTakeDamage += ImpactAudio;
            _damageable.OnDie += DieAudio;
            _damageable.SuccesfulBlock += BlockAudio;
        }
    
        private void ImpactAudio()
        {
            int randomIndex = Random.Range(0, impactList.Count);
            impactAudioSource.Stop();
            impactAudioSource.clip = impactList[randomIndex];
            impactAudioSource.Play();
        }
        private void DieAudio()
        {
            int randomIndex = Random.Range(0, dieClip.Count);
            impactAudioSource.Stop();
            impactAudioSource.clip = dieClip[randomIndex];
            impactAudioSource.Play();
        }
    
        private void BlockAudio()
        {
            int randomIndex = Random.Range(0, blockClips.Count);
            impactAudioSource.Stop();
            impactAudioSource.clip = blockClips[randomIndex];
            impactAudioSource.Play();
        }
        private void OnDisable()
        {
            _damageable.OnTakeDamage -= ImpactAudio;
            _damageable.OnDie -= DieAudio;
            _damageable.SuccesfulBlock -= BlockAudio;
        }
    }
}


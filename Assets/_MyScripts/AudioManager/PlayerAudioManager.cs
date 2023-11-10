using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using _MyScripts.Combat;

namespace _MyScripts.AudioManager
{
    public class PlayerAudioManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> combatMusicClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> ambienceMusicClips = new List<AudioClip>();
        [SerializeField] private AudioSource _playerAudioSource;
        private Damageable _playerDamagable;
        private void Start()
        {
            _playerDamagable = GetComponent<Damageable>();
            _playerDamagable.OutOfBattle += AmbienceMusicPlay;
            _playerDamagable.InBattle += CombatMusicPlay;
            AmbienceMusicPlay();
        }
        private void CombatMusicPlay()
        {
            int randIndex = Random.Range(0, combatMusicClips.Count);
            _playerAudioSource.Stop();
            _playerAudioSource.clip = combatMusicClips[randIndex];
            _playerAudioSource.Play();
        }

        private void AmbienceMusicPlay()
        {
            int randIndex = Random.Range(0, ambienceMusicClips.Count);
            _playerAudioSource.Stop();
            _playerAudioSource.clip = ambienceMusicClips[randIndex];
            _playerAudioSource.Play();
        }

        private void OnDisable()
        {
            _playerDamagable.OutOfBattle -= AmbienceMusicPlay;
            _playerDamagable.InBattle -= CombatMusicPlay;
        }
    }
}


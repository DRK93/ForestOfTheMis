using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using _MyScripts.Combat.WeaponMechanics;

namespace _MyScripts.AudioManager
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponAudioManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> weaponSwingClips = new List<AudioClip>();
        private AudioSource _weaponAudio;
        private WeaponDamage _weapon;
        private void Start()
        {
            _weapon = GetComponent<WeaponDamage>();
            _weaponAudio = GetComponent<AudioSource>();
            _weapon.SwingWeapon += SwingAudio;
        
        }

        private void OnDestroy()
        {
            _weapon.SwingWeapon -= SwingAudio;
        }

        private void SwingAudio()
        {
            int randomIndex = Random.Range(0, weaponSwingClips.Count);
            _weaponAudio.Stop();
            _weaponAudio.clip = weaponSwingClips[randomIndex];
            _weaponAudio.Play();
        }
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Combat
{
    public class Regenerating : MonoBehaviour
    {
        private Damageable _damageable;
        [SerializeField] public int healthRegen; // 5
        [SerializeField] public int manaRegen; //1
        [SerializeField] public int powerRegen; // 3

        public float regenTimerIdle = 5f;
        public float regenTimerFight = 20f;
        private float _timer = 0f;
        private float _currentRegenTimer;
        private void Start()
        {
            _damageable = GetComponent<Damageable>();
            _currentRegenTimer = regenTimerIdle;
            _damageable.InBattle += RegenInFight;
            _damageable.OutOfBattle += RegenInIdle;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _currentRegenTimer)
            {
                _damageable.RegenerationTime(healthRegen, manaRegen, powerRegen);
                _timer = 0f;
            }
        }

        private void RegenInFight()
        {
            _currentRegenTimer = regenTimerFight;
        }

        private void RegenInIdle()
        {
            _currentRegenTimer = regenTimerIdle;
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.MainCharacter
{
    public class FallDamageApplier : MonoBehaviour
    {
        [SerializeField] private LocomotionController _locomotionController;
        [SerializeField] private float _minDamageHeight;
        [SerializeField] private float _damagePerUnit;

        private bool _isCounting;
        private float _currentMaxHeight;
        private float _currentLandHeight;

        private bool _canApplyDamage => Mathf.Abs(_currentMaxHeight - _currentLandHeight) > _minDamageHeight;

        private void Update()
        {
            if (!_isCounting && !_locomotionController.IsGrounded)
                StartCounting();

            if (_isCounting && _locomotionController.IsGrounded)
                StopCounting();
        }

        private void StartCounting()
        {
            _isCounting = true;
            _currentMaxHeight = float.MinValue;
        }
        
        private void StopCounting()
        {
            _isCounting = false;

            if (_canApplyDamage)
            ApplyDamage();
        }

        private void ApplyDamage()
        {
            throw new NotImplementedException();
        }
    }
}
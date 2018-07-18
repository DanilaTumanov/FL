using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Spaceships.Systems
{

    public class MovementSystem : SpaceshipSystem
    {
        [Header("Основной двигатель")]
        [SerializeField]
        [Tooltip("Мощность основного двигателя.")]
        private float _mainEnginePower = 1;

        [Header("Маневровые двигатели")]

        [SerializeField]
        [Tooltip("Мощность двигателей тангажа.")]
        private float _pitchPower = 1;

        [SerializeField]
        [Tooltip("Мощность двигателей рысканья.")]
        private float _yawPower = 1;

        [SerializeField]
        [Tooltip("Мощность двигателей крена.")]
        private float _rollPower = 1;
        

        private float _force = 0;


        private void Update()
        {
            UpdateForce();
            UpdateSteering();

            transform.Translate(transform.forward * _force * Time.deltaTime);
        }


        private void UpdateForce()
        {
            _force = _mainEnginePower * _spaceship.PowerControl.PowerLevel;
        }

        private void UpdateSteering()
        {
            transform.Rotate(
                _spaceship.Steering.Pitch * Time.deltaTime * _pitchPower, 
                _spaceship.Steering.Yaw * Time.deltaTime * _yawPower, 
                _spaceship.Steering.Roll * Time.deltaTime * _rollPower
            );
        }
    }

}
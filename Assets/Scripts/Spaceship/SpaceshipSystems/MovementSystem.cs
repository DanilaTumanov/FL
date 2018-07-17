using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Spaceships.Systems
{

    public class MovementSystem : SpaceshipSystem
    {

        [SerializeField]
        [Tooltip("Мощность основного двигателя.")]
        private float _mainEnginePower = 1;

        
        private float _force = 0;


        private void Update()
        {
            UpdateForce();

            transform.Translate(transform.forward * _force * Time.deltaTime);
        }


        private void UpdateForce()
        {
            _force = _mainEnginePower * _spaceship.PowerControl.PowerLevel;
        }
    }

}
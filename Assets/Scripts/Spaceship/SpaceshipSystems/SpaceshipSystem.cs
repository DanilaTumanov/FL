using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Spaceships.Systems
{

    [RequireComponent(typeof(Spaceship))]
    public class SpaceshipSystem : MonoBehaviour
    {

        protected Spaceship _spaceship;


        protected virtual void Start()
        {
            _spaceship = GetComponent<Spaceship>();
        }

    }

}
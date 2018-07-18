using FL.SceneObjects;
using FL.Spaceships.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Spaceships
{

    [RequireComponent(typeof(MovementSystem))]
    public class Spaceship : BaseSceneObject
    {

        public MovementSystem MovementSystem { get; private set; }
        public InteractionPowerLever PowerControl { get; private set; }
        public InteractionSteering Steering { get; private set; }


        private void Start()
        {
            MovementSystem = GetComponent<MovementSystem>();
            PowerControl = GameObject.FindGameObjectWithTag("PowerControl").GetComponent<InteractionPowerLever>();
            Steering = GameObject.FindGameObjectWithTag("Steering").GetComponent<InteractionSteering>();
        }

    }

}
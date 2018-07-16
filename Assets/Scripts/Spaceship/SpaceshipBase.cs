using FL.SceneObjects;
using FL.Spaceships.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Spaceships
{

    [RequireComponent(typeof(MovementSystem))]
    public class SpaceshipBase : BaseSceneObject
    {

        public MovementSystem MovementSystem { get; private set; }


        private void Start()
        {
            MovementSystem = GetComponent<MovementSystem>();
        }

    }

}
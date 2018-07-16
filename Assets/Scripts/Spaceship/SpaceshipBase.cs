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

        private MovementSystem _movementSystem;



        private void Start()
        {
            _movementSystem = GetComponent<MovementSystem>();
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Spaceships.Systems
{

    public class MovementSystem : MonoBehaviour
    {

        private float _force = 0;


        private void Update()
        {
            transform.Translate(transform.forward * _force * Time.deltaTime);
        }


        public void SetForce(float force)
        {
            _force = force;
        }

    }

}
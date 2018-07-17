using FL.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    public class InteractionSteering : PositionTrackingInteraction
    {


        protected override void Update()
        {
            base.Update();

            if (!_activeInteraction)
                return;

            ProcessSteering();
        }


        public override void StopInteract()
        {
            base.StopInteract();

            // После завершения взаимодействия возвращаем в исходное положение (если отклонения сбрасываются родителем)
            ProcessSteering();
        }


        private void ProcessSteering()
        {
            VisualizeSteering();


        }


        private void VisualizeSteering()
        {
            transform.localRotation = Quaternion.Euler(_initialRotation.x + AbsDeltaY, _initialRotation.y + AbsDeltaZ, _initialRotation.z - AbsDeltaX); 
        }

    }

}

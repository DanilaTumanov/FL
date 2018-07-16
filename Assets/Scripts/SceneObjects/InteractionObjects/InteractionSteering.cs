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

            // После завершения взаимодействия возвращаем в исходное положение (отклонения сбросятся родителем)
            ProcessSteering();
        }


        private void ProcessSteering()
        {
            VisualizeSteering();


        }


        private void VisualizeSteering()
        {
            transform.localRotation = Quaternion.Euler(_initialRotation.x + _deltaY, _initialRotation.y + _deltaZ, _initialRotation.z + _deltaX);
        }

    }

}

using FL.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    public class InteractionSteering : PositionTrackingInteraction
    {

        /// <summary>
        /// Тангаж
        /// </summary>
        public float Pitch { get; private set; }

        /// <summary>
        /// Крен
        /// </summary>
        public float Roll { get; private set; }

        /// <summary>
        /// Рысканье
        /// </summary>
        public float Yaw { get; private set; }



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

            Pitch = - RelDeltaY; // Инвертируем вертикальную ось, чтобы управление было более интуитивным на VR контроллере - игрок ведет руку вверх, и корабль летит вверх
            Yaw = RelDeltaX;
            Roll = RelDeltaZ;
        }


        private void VisualizeSteering()
        {
            transform.localRotation = Quaternion.Euler(_initialRotation.x + AbsDeltaY, _initialRotation.y - AbsDeltaZ, _initialRotation.z - AbsDeltaX); 
        }

    }

}

using FL.Spaceships.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    public class InteractionPowerLever : PositionTrackingInteraction
    {


        private MovementSystem _movementSystem;




        protected override void Start()
        {
            base.Start();

            _movementSystem = Main.Instance.Spaceship.MovementSystem;
        }


        protected override void Update()
        {
            base.Update();

            if (!_activeInteraction)
                return;

            ProcessPowerControl();
        }


        public override void StopInteract()
        {
            base.StopInteract();
        }


        private void ProcessPowerControl()
        {
            VisualizeLever();

            // TODO: Сделать уровень от 0 до 1
            _movementSystem.SetForce(AbsDeltaY);
        }


        private void VisualizeLever()
        {
            transform.rotation = Quaternion.Euler(_initialRotation.x + AbsDeltaY, _initialRotation.y, _initialRotation.z);
        }

    }

}
using FL.Spaceships.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    public class InteractionPowerLever : PositionTrackingInteraction
    {

        public float PowerLevel { get; private set; }



        protected override void Start()
        {
            base.Start();
        }


        protected void LateUpdate()
        {
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

            PowerLevel = RelDeltaY;
        }


        private void VisualizeLever()
        {
            transform.localRotation = Quaternion.Euler(_initialRotation.x + AbsDeltaY, _initialRotation.y, _initialRotation.z);
        }

    }

}
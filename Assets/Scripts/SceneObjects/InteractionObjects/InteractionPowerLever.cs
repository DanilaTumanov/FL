using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    public class InteractionPowerLever : PositionTrackingInteraction
    {


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


        }


        private void VisualizeLever()
        {
            transform.rotation = Quaternion.Euler(_initialRotation.x + _deltaY, _initialRotation.y, _initialRotation.z);
        }

    }

}
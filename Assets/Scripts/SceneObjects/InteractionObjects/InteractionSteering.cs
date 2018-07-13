using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    public class InteractionSteering : InteractionObject
    {

        public override void Interact()
        {
            base.Interact();

            print("Start interaction with steering stick");
        }

        public override void StopInteract()
        {
            base.StopInteract();

            print("Stop interaction with steering stick");
        }

    }

}

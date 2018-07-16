using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Controllers
{

    public class PCInput : InputController
    {



        public Quaternion controllerRotationEmu = Quaternion.identity;





        public override bool Select
        {
            get
            {
                return Input.GetMouseButton(1);
            }
        }

        public override bool SelectDown
        {
            get
            {
                return Input.GetMouseButtonDown(1);
            }
        }

        public override bool SelectUp
        {
            get
            {
                return Input.GetMouseButtonUp(1);
            }
        }






        public override bool Action
        {
            get
            {
                return Input.GetMouseButton(0);
            }
        }

        public override bool ActionDown
        {
            get
            {
                return Input.GetMouseButtonDown(0);
            }
        }

        public override bool ActionUp
        {
            get
            {
                return Input.GetMouseButtonUp(0);
            }
        }






        public override bool Back
        {
            get
            {
                return Input.GetButton("Cancel");
            }
        }

        public override bool BackDown
        {
            get
            {
                return Input.GetButtonDown("Cancel");
            }
        }

        public override bool BackUp
        {
            get
            {
                return Input.GetButtonUp("Cancel");
            }
        }






        public override Ray PointerRay
        {
            get
            {
                return Camera.main.ScreenPointToRay(Input.mousePosition);
            }
        }




        public override Vector2 TouchPos
        {
            get
            {
                return Vector2.zero;
            }
        }




        public override Quaternion ControllerRotation
        {
            get
            {
                return controllerRotationEmu;
            }
        }

    }

}
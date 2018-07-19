using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Controllers
{

    public class GearVRInput : InputController
    {
        
        private Transform _controller;
        private Ray _pointerRay = new Ray();


        public GearVRInput()
        {
            _controller = GameObject.FindGameObjectWithTag("VRController").transform;
        }




        public override bool Select
        {
            get
            {
                return OVRInput.Get(OVRInput.Button.PrimaryTouchpad);
            }
        }

        public override bool SelectDown
        {
            get
            {
                return OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad);
            }
        }

        public override bool SelectUp
        {
            get
            {
                return OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad);
            }
        }





        public override bool Action
        {
            get
            {
                return OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
            }
        }

        public override bool ActionDown
        {
            get
            {
                return OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
            }
        }

        public override bool ActionUp
        {
            get
            {
                return OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger);
            }
        }





        public override bool Back
        {
            get
            {
                return OVRInput.Get(OVRInput.Button.Back);
            }
        }

        public override bool BackDown
        {
            get
            {
                return OVRInput.GetDown(OVRInput.Button.Back);
            }
        }

        public override bool BackUp
        {
            get
            {
                return OVRInput.GetUp(OVRInput.Button.Back);
            }
        }





        public override Ray PointerRay
        {
            get
            {
                _pointerRay.origin = _controller.position;
                _pointerRay.direction = _controller.forward;

                return _pointerRay;
            }
        }



        public override Vector2 TouchPos
        {
            get
            {
                return OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            }
        }



        public override Quaternion ControllerRotation
        {
            get
            {
                return _controller.rotation;
            }
        }
    }

}

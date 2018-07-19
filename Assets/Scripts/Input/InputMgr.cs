using FL.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Managers
{

    public static class InputMgr
    {

        static private InputController _inputController;

        public static InputController Controller
        {
            get
            {
                return _inputController;
            }
        }


        static InputMgr()
        {
#if UNITY_EDITOR
            _inputController = new PCInput();
#elif UNITY_ANDROID
            _inputController = new GearVRInput();
#endif
        }

        
    }

}
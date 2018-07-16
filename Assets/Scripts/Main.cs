﻿using FL.Controllers;
using FL.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL
{

    public class Main : MonoBehaviour
    {

        private GameObject _controllerGO;

        public static Main Instance { get; private set; }
        public GameObject Player { get; private set; }


        void Start()
        {
            Instance = this;

            Player = GameObject.FindGameObjectWithTag("Player");

            _controllerGO = new GameObject(name = "Controllers");

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_controllerGO);
        }



#if UNITY_EDITOR

        private void Update()
        {
            EmulateControllerRotation();
        }

        private void EmulateControllerRotation()
        {
            var rotation = (InputMgr.Controller as PCInput).controllerRotationEmu;
            (InputMgr.Controller as PCInput).controllerRotationEmu = Quaternion.Euler(0, 0, rotation.eulerAngles.z + Input.GetAxis("Horizontal"));
        }

#endif

    }

}
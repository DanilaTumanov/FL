using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Controllers
{

    public abstract class BaseController : MonoBehaviour
    {

        public bool Enabled { get; private set; }


        private void Start()
        {
            Enable();
        }


        public virtual void Enable()
        {
            Enabled = true;
        }

        public virtual void Disable()
        {
            Enabled = false;
        }

    }

}
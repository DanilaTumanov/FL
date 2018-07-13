using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    [RequireComponent(typeof(Outline))]
    public abstract class InteractionObject : BaseSceneObject
    {

        private Outline _outline;


        private void Start()
        {
            _outline = GetComponent<Outline>();
            SetActiveOutline(false);
        }


        private void Update()
        {
            SetActiveOutline(false);
        }


        public virtual void Interact()
        {
            SetActiveOutline(false);
        }


        public virtual void StopInteract()
        {
            SetActiveOutline(false);
        }


        public virtual void Hover()
        {
            SetActiveOutline(true);
        }


        private void SetActiveOutline(bool activeOutline)
        {
            _outline.eraseRenderer = !activeOutline;
        }

    }

}

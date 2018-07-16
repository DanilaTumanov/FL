using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    [RequireComponent(typeof(Outline))]
    public abstract class InteractionObject : BaseSceneObject
    {

        /// <summary>
        /// Признак активного взаимодействия
        /// </summary>
        protected bool _activeInteraction = false;

        private Outline _outline;


        protected virtual void Start()
        {
            _outline = GetComponent<Outline>();
            SetActiveOutline(false);
        }


        protected virtual void Update()
        {
            SetActiveOutline(false);
        }


        public virtual void Interact()
        {
            SetActiveOutline(false);
            _activeInteraction = true;
        }


        public virtual void StopInteract()
        {
            SetActiveOutline(false);
            _activeInteraction = false;
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

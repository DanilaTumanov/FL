using FL.Managers;
using FL.SceneObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Controllers
{

    public class InteractionController : BaseController
    {

        public LayerMask interactionLayers;

        private InteractionObject _hoveredInteraction;
        private InteractionObject _interactedObject;
        

        private void OnValidate()
        {
            interactionLayers |= 1 << LayerMask.NameToLayer("Interactions");
        }


        private void LateUpdate()
        {
            if (!Enabled)
                return;

            ProcessHover();
            ProcessInteract();
        }



        private void ProcessHover()
        {
            // TODO: Возможно проверка объекта на null не очень оптимальное решение.. Возможно стоит завести флаг того, что происходит взаимодействие...
            // с другой стороны наличие объекта взаимодействия однозначно определяет факт осуществления взаимодействия, а флаг бы просто дублировал это
            if (_interactedObject != null)
                return;

            RaycastHit hit;
            Ray ray = InputMgr.Controller.PointerRay;

            _hoveredInteraction = null;

            if (Physics.Raycast(ray, out hit, 10, interactionLayers))
            {
                var interaction = hit.transform.GetComponent<InteractionObject>();

                if(interaction != null)
                {
                    interaction.Hover();
                    _hoveredInteraction = interaction;
                }
            }
        }


        private void ProcessInteract()
        {
            if (_hoveredInteraction == null)
                return;

            if (_interactedObject == null 
                && InputMgr.Controller.SelectDown)
            {
                _interactedObject = _hoveredInteraction;
                _interactedObject.Interact();
            }
            else if (InputMgr.Controller.SelectDown)
            {
                _interactedObject.StopInteract();
                _interactedObject = null;
            }
        }
    }

}

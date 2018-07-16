using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.Controllers
{

    public abstract class InputController
    {

        // Нажатие на кнопку выбора (тачпад на VR контроллере)
        public abstract bool Select { get; }
        public abstract bool SelectDown { get; }
        public abstract bool SelectUp { get; }

        
        // Нажатие на кнопку действия (курок на VR контроллере)
        public abstract bool Action { get; }
        public abstract bool ActionDown { get; }
        public abstract bool ActionUp { get; }


        // Нажатие на кнопку назад
        public abstract bool Back { get; }
        public abstract bool BackDown { get; }
        public abstract bool BackUp { get; }


        // Луч указателя
        public abstract Ray PointerRay { get; }


        // Позиция пальца на тачпаде
        public abstract Vector2 TouchPos { get; }


        // Вращение контроллера
        public abstract Quaternion ControllerRotation { get; }

    }

}
using FL.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FL.SceneObjects
{

    public abstract class PositionTrackingInteraction : InteractionObject
    {

        private const float INITIAL_Z_ANGLE = 0;



        [Header("Нужно ли сохранять значения осей после взаимодействия")]
        public bool keepDeltaX = true;
        public bool keepDeltaY = true;
        public bool keepDeltaZ = true;

        [Header("Границы отклонений по осям (x - min, y - max)")]
        public Vector2 Xbounds = Vector2.zero;
        public Vector2 Ybounds = Vector2.zero;
        public Vector2 Zbounds = Vector2.zero;

        /// <summary>
        /// Направление инициализации взаимодействия. Для отслеживания отклонения
        /// </summary>
        protected Vector3 _initialDirection;


        private Vector3 _initialHorizontalProj;
        private Vector3 _initialVerticalProj;

        private Vector3 _initialHorizontalPerp;
        private Vector3 _initialVecrticalPerp;

        private float _initialDeltaX;
        private float _initialDeltaY;
        private float _initialDeltaZ;
        

        /// <summary>
        /// Система координат для рассчета (корабль или связанная с ним система)
        /// </summary>
        private Transform _coordinateSystem;


        protected Vector3 _initialRotation;


        /// <summary>
        /// Угол отклонения по X в градусах
        /// </summary>
        public float AbsDeltaX { get; private set; }

        /// <summary>
        /// Угол отклонения по Y в градусах
        /// </summary>
        public float AbsDeltaY { get; private set; }

        /// <summary>
        /// Угол отклонения по Z в градусах
        /// </summary>
        public float AbsDeltaZ { get; private set; }


        /// <summary>
        /// Отклонение по X в относительной величине (1 - максимальное)
        /// </summary>
        public float RelDeltaX { get; private set; }

        /// <summary>
        /// Отклонение по Y в относительной величине (1 - максимальное)
        /// </summary>
        public float RelDeltaY { get; private set; }

        /// <summary>
        /// Отклонение по Z в относительной величине (1 - максимальное)
        /// </summary>
        public float RelDeltaZ { get; private set; }







        protected override void Start()
        {
            base.Start();

            _coordinateSystem = GameObject.FindGameObjectWithTag("Spaceship").transform;
            _initialRotation = transform.localEulerAngles;
        }


        protected override void Update()
        {
            base.Update();

            if (!_activeInteraction)
                return;

            ProcessDelta();
        }



        public override void Interact()
        {
            base.Interact();

            _initialDirection = _coordinateSystem.InverseTransformDirection(InputMgr.Controller.PointerRay.direction);

            SetProjections(_initialDirection, out _initialHorizontalProj, out _initialVerticalProj);
            SetInitialPerpendiculars(_initialHorizontalProj, _initialVerticalProj, out _initialHorizontalPerp, out _initialVecrticalPerp);
            SetInitialDelta();
        }

        

        public override void StopInteract()
        {
            base.StopInteract();

            AbsDeltaX = keepDeltaX ? AbsDeltaX : 0;
            AbsDeltaY = keepDeltaY ? AbsDeltaY : 0;
            AbsDeltaZ = keepDeltaZ ? AbsDeltaZ : 0;
        }


        private void ProcessDelta()
        {
            var direction = _coordinateSystem.InverseTransformDirection(InputMgr.Controller.PointerRay.direction);

            // Переводим поворот контроллера из мирового значения в значение, относительно поворота корабля
            // TODO: разобраться, как это работает))))
            var relativeControllerRotation = Quaternion.Inverse(_coordinateSystem.rotation) * InputMgr.Controller.ControllerRotation;

            Vector3 verticalProj, horizontalProj;



            SetProjections(direction, out horizontalProj, out verticalProj);
            
            // Определяем угол
            AbsDeltaX = Vector3.Angle(_initialHorizontalProj, horizontalProj);
            AbsDeltaY = Vector3.Angle(_initialVerticalProj, verticalProj);

            // Определяем знак угла
            AbsDeltaX *= Mathf.Sign(Vector3.Dot(horizontalProj, _initialHorizontalPerp));
            AbsDeltaY *= Mathf.Sign(Vector3.Dot(verticalProj, _initialVecrticalPerp));

            // Определяем угол наклона
            AbsDeltaZ = Mathf.DeltaAngle(INITIAL_Z_ANGLE, relativeControllerRotation.eulerAngles.z);

            // Корректируем отклонение в зависимости от отклонения в начале взаимодействия
            AbsDeltaX += _initialDeltaX;
            AbsDeltaY += _initialDeltaY;
            AbsDeltaZ += _initialDeltaZ;

            // Обрезаем значения по установленным границам
            AbsDeltaX = Xbounds.SqrMagnitude() > 0 ? Mathf.Clamp(AbsDeltaX, Xbounds.x, Xbounds.y) : AbsDeltaX;
            AbsDeltaY = Ybounds.SqrMagnitude() > 0 ? Mathf.Clamp(AbsDeltaY, Ybounds.x, Ybounds.y) : AbsDeltaY;
            AbsDeltaZ = Zbounds.SqrMagnitude() > 0 ? Mathf.Clamp(AbsDeltaZ, Zbounds.x, Zbounds.y) : AbsDeltaZ;


            // Рассчитываем относительное отклонение
            RelDeltaX = GetRelativeDelta(AbsDeltaX, Xbounds);
            RelDeltaY = GetRelativeDelta(AbsDeltaY, Ybounds);
            RelDeltaZ = GetRelativeDelta(AbsDeltaZ, Zbounds);
        }


        private void SetProjections(Vector3 origin, out Vector3 horizontalProj, out Vector3 verticalProj)
        {
            horizontalProj = Vector3.ProjectOnPlane(origin, Vector3.up);
            verticalProj = Vector3.ProjectOnPlane(origin, Vector3.right);
        }

        /// <summary>
        /// Метод установит пердпендикуляры для проекций в вертикальной и горизонтальной плоскостях.
        /// Нужно для вычисления dot product и нахождения знака отклонения
        /// </summary>
        /// <param name="horizontalProj"></param>
        /// <param name="verticalProj"></param>
        /// <param name="horizontalPerp"></param>
        /// <param name="verticalPerp"></param>
        private void SetInitialPerpendiculars(Vector3 horizontalProj, Vector3 verticalProj, out Vector3 horizontalPerp, out Vector3 verticalPerp)
        {
            horizontalPerp = Vector3.Cross(Vector3.up, horizontalProj);
            verticalPerp = Vector3.Cross(verticalProj, Vector3.right);
        }


        private void SetInitialDelta()
        {
            _initialDeltaX = AbsDeltaX;
            _initialDeltaY = AbsDeltaY;
            _initialDeltaZ = AbsDeltaZ;
        }


        private float GetRelativeDelta(float absDelta, Vector2 bounds)
        {
            if (bounds.y > 0)
                return absDelta / bounds.y;
            else if (Xbounds.x > 0)
                return absDelta / -bounds.x;
            else
                return absDelta / 180;
        }

    }

}

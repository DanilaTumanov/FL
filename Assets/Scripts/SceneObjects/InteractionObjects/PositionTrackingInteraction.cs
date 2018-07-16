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
        /// Угол отклонения по X
        /// </summary>
        protected float _deltaX;

        /// <summary>
        /// Угол отклонения по Y
        /// </summary>
        protected float _deltaY;

        /// <summary>
        /// Угол отклонения по Z
        /// </summary>
        protected float _deltaZ;

        /// <summary>
        /// Система координат для рассчета (корабль или связанная с ним система)
        /// </summary>
        private Transform _coordinateSystem;


        protected Vector3 _initialRotation;




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

            SetProjections(_initialDirection, _coordinateSystem, out _initialHorizontalProj, out _initialVerticalProj);
            SetInitialPerpendiculars(_initialHorizontalProj, _initialVerticalProj, out _initialHorizontalPerp, out _initialVecrticalPerp);
            SetInitialDelta();
        }

        

        public override void StopInteract()
        {
            base.StopInteract();

            _deltaX = keepDeltaX ? _deltaX : 0;
            _deltaY = keepDeltaY ? _deltaY : 0;
            _deltaZ = keepDeltaZ ? _deltaZ : 0;
        }


        private void ProcessDelta()
        {
            var direction = _coordinateSystem.InverseTransformDirection(InputMgr.Controller.PointerRay.direction);
            Vector3 verticalProj, horizontalProj;

            SetProjections(direction, _coordinateSystem, out horizontalProj, out verticalProj);

            // Определяем угол
            _deltaX = Vector3.Angle(_initialHorizontalProj, horizontalProj);
            _deltaY = Vector3.Angle(_initialVerticalProj, verticalProj);

            // Определяем знак угла
            _deltaX *= Mathf.Sign(Vector3.Dot(horizontalProj, _initialHorizontalPerp));
            _deltaY *= Mathf.Sign(Vector3.Dot(verticalProj, _initialVecrticalPerp));

            // Определяем угол наклона
            _deltaZ = Mathf.DeltaAngle(INITIAL_Z_ANGLE, InputMgr.Controller.ControllerRotation.eulerAngles.z);

            // Корректируем отклонение в зависимости от отклонения в начале взаимодействия
            _deltaX += _initialDeltaX;
            _deltaY += _initialDeltaY;
            _deltaZ += _initialDeltaZ;

            // Обрезаем значения по установленным границам
            _deltaX = Xbounds.SqrMagnitude() > 0 ? Mathf.Clamp(_deltaX, Xbounds.x, Xbounds.y) : _deltaX;
            _deltaY = Ybounds.SqrMagnitude() > 0 ? Mathf.Clamp(_deltaY, Ybounds.x, Ybounds.y) : _deltaY;
            _deltaZ = Zbounds.SqrMagnitude() > 0 ? Mathf.Clamp(_deltaZ, Zbounds.x, Zbounds.y) : _deltaZ;
        }


        private void SetProjections(Vector3 origin, Transform coordinateSystem, out Vector3 horizontalProj, out Vector3 verticalProj)
        {
            horizontalProj = Vector3.ProjectOnPlane(origin, _coordinateSystem.up);
            verticalProj = Vector3.ProjectOnPlane(origin, _coordinateSystem.right);
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
            horizontalPerp = Vector3.Cross(horizontalProj, _coordinateSystem.up);
            verticalPerp = Vector3.Cross(verticalProj, _coordinateSystem.right);
        }


        private void SetInitialDelta()
        {
            _initialDeltaX = _deltaX;
            _initialDeltaY = _deltaY;
            _initialDeltaZ = _deltaZ;
        }

    }

}

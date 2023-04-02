using UnityEngine;

namespace RacePrototype
{
    public class PlayerInputController : BaseInputController
    {
        private CarControl _controls;


        protected override void FixedUpdate()
        {
            var direction = _controls.Car.Rotate.ReadValue<float>();

            if (direction == 0f && Rotate != 0f )
            {
                Rotate = Rotate >0f
                    ? Rotate - Time.fixedDeltaTime
                    : Rotate + Time.fixedDeltaTime;
            }
            else
            {
                Rotate = Mathf.Clamp(Rotate + direction * Time.fixedDeltaTime, -1f, 1f);
            }
            Acceleration = _controls.Car.Acceleration.ReadValue<float>();
        }

        private void Awake()
            => _controls = new CarControl();
        private void OnEnable()
        {
            _controls.Car.Enable();
            _controls.Car.HandBrake.performed += context => CallHandBrake(true);
            _controls.Car.HandBrake.canceled += context => CallHandBrake(false);
        }

        private void OnDisable()
            => _controls.Car.Disable();
        private void OnDestroy()
            => _controls.Dispose();

    }
}
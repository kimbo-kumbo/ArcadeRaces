using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RacePrototype
{
    [RequireComponent(typeof(WheelsComponent), typeof(BaseInputController), typeof(Rigidbody))]
    public class CarComponent : MonoBehaviour
    {
        private WheelsComponent _wheels;
        private BaseInputController _input;
        private Rigidbody _rigidbody;
        private Speedometr _speedometr;

        [SerializeField, Range(5f, 60f)]
        private float _maxSteerAngle = 25f;
        [SerializeField]
        private float _torque = 2500f;
        //[SerializeField]
        private float _handBrakeTorque = float.MaxValue;
        [SerializeField]
        private Vector3 _centerOfMass = Vector3.zero;

        [SerializeField] private TuningSO_Model _tuningSOmodel;

        private bool _movingForward;

        private void FixedUpdate()
        {
            _wheels.UpdateVisual(_input.Rotate * _maxSteerAngle);
            float torque = 0;

            if (_speedometr.GetSpeed() == 0)
            {
                if (_input.Acceleration > 0)
                {
                    _movingForward = true;
                    OnBrake(false);
                    torque = _input.Acceleration * _torque;// / 2f;
                }
                else
                {
                    _movingForward = false;
                    OnBrake(false);
                    torque = -_input.Acceleration * _torque;// / 2f;
                }
            }
            if (_movingForward)
            {
                if (_input.Acceleration > 0)
                {
                    OnBrake(false);
                    torque = _input.Acceleration * _torque;// / 2f;
                }
                else
                {
                    OnBrake(true);
                }
            }
            else
            {
                if (_input.Acceleration < 0)
                {
                    OnBrake(false);
                    torque = _input.Acceleration * _torque;// / 2f;
                }
                else
                {
                    OnBrake(true);
                }
            }


            //foreach (var wheel in _wheels.GetFrontWheels)//сделал задний привод. это ж мустанг ))
            foreach (var wheel in _wheels.GetRearWheels)
            {
                wheel.motorTorque = torque;
            }

        }

        private void OnHandBrake(bool value)
        {
            if (value)
            {
                foreach (var wheel in _wheels.GetRearWheels)
                {
                    wheel.brakeTorque = _handBrakeTorque;
                    wheel.motorTorque = 0f;
                }
            }
            else
            {
                foreach (var wheel in _wheels.GetRearWheels)
                    wheel.brakeTorque = 0f;
            }
        }

        private void OnBrake(bool value)
        {
            if (value)
            {
                foreach (var wheel in _wheels.GetAllWheels)
                {
                    wheel.brakeTorque = _handBrakeTorque;
                    wheel.motorTorque = 0f;
                }
            }
            else
            {
                foreach (var wheel in _wheels.GetAllWheels)
                    wheel.brakeTorque = 0f;
            }
        }


        private void Start()
        {           

            _wheels = GetComponent<WheelsComponent>();
            _input = GetComponent<BaseInputController>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = _centerOfMass;//центр массы

            _speedometr = FindObjectOfType<Speedometr>();
            
            _wheels.ChangeSettingsWhealsColliderAvto(_tuningSOmodel);
            GetSettingsRigidbodyAvto(_tuningSOmodel);
            _input.OnHandBrakeEvent += OnHandBrake;
        }

        private void OnDestroy()
        {
            _input.OnHandBrakeEvent -= OnHandBrake;
        }

        private void GetSettingsRigidbodyAvto(TuningSO_Model incomingValue)
        {
            _torque = incomingValue.Torque;
            _centerOfMass = new Vector3(0, 0, incomingValue.CenterMassAvto);
            _maxSteerAngle = incomingValue.MaxSteerAngle;
            _rigidbody.mass = incomingValue.MassAvto;  


        }
    }
}

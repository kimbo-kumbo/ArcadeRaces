using UnityEngine;

namespace RacePrototype
{
    [CreateAssetMenu(fileName = "Tunning", menuName = "New Tuning", order = 51)]
    public class TuningSO_Model : ScriptableObject
    {
        [SerializeField] private float _maxSteerAngle;
        [SerializeField] private float _damperAvto;
        [SerializeField] private float _massAvto;
        [SerializeField] private float _centerMassAvto;
        [SerializeField] private float _torque;


        public float MaxSteerAngle
        {
            get => _maxSteerAngle;
            set
            {
                if (value < 30)
                    _maxSteerAngle = 20;
                else
                    _maxSteerAngle = value;
            }
        }
        public float MassAvto
        {
            get => _massAvto;
            set
            {
                if (value < 200)
                    _massAvto = 200;
                else
                    _massAvto = value;
            }
        }
        public float Torque
        {
            get => _torque;
            set
            {
                if (value < 500)
                    _torque = 500;
                else
                    _torque = value;
            }
        }
        public float CenterMassAvto { get => _centerMassAvto; set => _centerMassAvto = value; }
        public float DamperAvto { get => _damperAvto; set => _damperAvto = value; }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacePrototype
{
    public class Speedometr : MonoBehaviour
    {
        private const float c_convertMeterInSecFromKmInH = 3.6f;       

        [SerializeField]
        private float _maxSpeed = 300f;
        [SerializeField]
        private Color _minColor = Color.yellow;
        [SerializeField]
        private Color _maxColor = Color.red;

        [Space, SerializeField, Range(0.1f, 1f)]
        private float _delay = 0.3f;
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Transform _player;
        private float _speed;

        private void Start()
        {
            StartCoroutine(Speed());
        }

        public float GetSpeed()
        { 
            return _speed;
        }
        private IEnumerator Speed()
        {
            var prevPos = _player.position;
            while (true) 
            {
                var distance = Vector3.Distance(prevPos, _player.position);
                _speed = (float)System.Math.Round(distance / _delay * c_convertMeterInSecFromKmInH, 1);

                _text.color = Color.Lerp(_minColor, _maxColor, _speed / _maxSpeed);
                _text.text = _speed.ToString();
                prevPos = _player.position;
                yield return new WaitForSeconds(_delay);
            
            }
        }
    }
}

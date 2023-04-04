using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RacePrototype
{
    public class СountdownTimer : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private int _timeСountdown;
        [SerializeField] private PlayerInputController _playerInputController;
        [SerializeField] private Timer _timer;

        private Action OnEnableText;


        private void OnEnable()
        {
            OnEnableText += OnEnableTimerText;
        }
        private void OnDisable()
        {
            OnEnableText -= OnEnableTimerText;
        }
        private void Start()
        {            
            StartCoroutine(Сountdown());            
        }

        private IEnumerator ScaleText()
        {
            for (float i = 1f; i < 2f; i+= 0.1f)
            {
                _text.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForSeconds(.02f);
            }                       
        }

        private IEnumerator Сountdown()
        {
            while (_timeСountdown != -1)
            {                
                _text.text = _timeСountdown.ToString();
                _timeСountdown--;
                if(_timeСountdown == -1)
                {
                    _text.text = "GO";
                    _playerInputController.enabled = true;
                    _timer.enabled = true;
                    OnEnableText?.Invoke();
                }
                StartCoroutine(ScaleText());
                yield return new WaitForSeconds(1f);
            }
        }

        private async void OnEnableTimerText()
        {
            await Task.Delay(2000);
            _text.gameObject.SetActive(false);
        }
    }
}

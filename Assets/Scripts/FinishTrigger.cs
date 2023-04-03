using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace RacePrototype
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private Statistics_Controller _statisticsController;
        [SerializeField] private StatisticsPanel_Marker _statisticsPanel;
        private Timer _timer;
        private Statistics_View _statistics_View;
        public Action<float> OnFinish;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Body_Marker>() /*|| other.GetComponent<NewPlayer>() */is null)
                return;

            SaveStatistics();

            OnEnableStaticsPanel();
            Debug.Log("Finish");
        }

        private void SaveStatistics()
        {
            OnFinish?.Invoke(_timer.ConvertTimeElapsedToFloat());

            //_statisticsController.SaveRecord(newRecord);
        }

        private void Start()
        {
            //_statisticsController = FindObjectOfType<Statistics_Controller>();
            _timer = FindObjectOfType<Timer>();
            _statistics_View = FindObjectOfType<Statistics_View>();
        }

        private async void OnEnableStaticsPanel()
        {
            await Task.Delay(1000);
            _statisticsPanel.gameObject.SetActive(true);
            _statistics_View.LoadStatsInfo();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

namespace RacePrototype
{
    public struct Record
    {
        //public int id;
        public string name;
        public float time;
    }
    public class Statistics_Controller : Base_Controller
    {
        [SerializeField] public Button _reStart;
        [SerializeField] public Button _reSetStats;
        public List<Record> _records = new();
        private bool _gameFirstLaunch = false;
        private Statistics_View _statistics_View;

        public void SaveRecord(Record newrecord)
        {
            //LoadRecords();
            _records.Add(newrecord);
            _records = _records.OrderBy(p => p.time).ToList();

            for (int i = 0; i < 10; i++)
            {
                PlayerPrefs.SetFloat(i.ToString(), _records[i].time);
                PlayerPrefs.SetString((i+10).ToString(), _records[i].name);
                
            }
        }
        private void Start()
        {
            Initialize(false);
            _gameFirstLaunch = (0 == PlayerPrefs.GetInt("_gameFirstLaunch"));
            PlayerPrefs.SetInt("_gameFirstLaunch", 1);
            _statistics_View = FindObjectOfType<Statistics_View>();
        }
        public Record ShowLastResult(Record newrecord)
        {
            return newrecord;
        }
        public void Initialize(bool fromButton)
        {
            if (_gameFirstLaunch || fromButton)
            {

                for (int i = 0; i < 10; i++)
                {
                    
                    PlayerPrefs.SetFloat(i.ToString(), float.MaxValue);
                    PlayerPrefs.SetString((i + 10).ToString(), "");
                }
                _statistics_View.LoadStatsInfo();
            }
            
        }
        public void AddListener()
        {
            _reStart.onClick.AddListener(delegate { LoadScene(SceneExample.Drive); });
            _reSetStats.onClick.AddListener(delegate { Initialize(true); });
        }

        private void OnDisable()
        {
            //_reStart.onClick.RemoveAllListeners();//   RemoveListener(delegate { LoadScene(SceneExample.Drive); });
        }
        public void LoadRecords()
        {
            Record record = new();
            //_records.Clear();
            for (int i = 0; i < 10; i++)
            {
                
                record.time = PlayerPrefs.GetFloat(i.ToString());
                record.name = PlayerPrefs.GetString((i+10).ToString());
                _records.Add(record);
            }

        }
    }
}
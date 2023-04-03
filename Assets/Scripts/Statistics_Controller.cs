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

        public Record(string name, float time)
        {
            this.name = name;
            this.time = time;
        }
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
            PlayerPrefs.SetInt("_gameFirstLaunch", 0);
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
            _reSetStats.onClick.AddListener(ResetStatsInfo);
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

        public void ResetStatsInfo()
        {            
            for (int i = 0; i < 10; i++)
            {
                _records.Add(new Record("", float.MaxValue));
                _records = _records.OrderByDescending(p => p.time).ToList();

                for (int j = 0; j < 10; j++)
                {
                    PlayerPrefs.SetFloat(j.ToString(), _records[j].time);
                    PlayerPrefs.SetString((j + 10).ToString(), _records[j].name);
                }                              
            }            
            LoadRecords();
            _statistics_View.LoadStatsInfo();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace RacePrototype
{
    public struct Record
    {       
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
        [SerializeField] private Button _reStart;
        [SerializeField] private Button _reSetStats;
        private List<Record> _records = new();
        private bool _gameFirstLaunch = false;
        private Statistics_View _statistics_View;

        public List<Record> Records => _records;

        public void SaveRecord(Record newrecord)
        {            
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
       
        public void LoadRecords()
        {
            Record record = new();            
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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

namespace RacePrototype
{
    public struct Record
    {
        //public int id;
        public string name;
        public float time;
    }
    public class Statistics_Controller : MonoBehaviour
    {
        public List<Record> _records = new();
        public void SaveRecord(Record newrecord)
        {
            LoadRecords();
            _records.Add(newrecord);
            _records = _records.OrderBy(p => p.time).ToList();

            for (int i = 0; i < 10; i++)
            {
                PlayerPrefs.SetString(i.ToString(), _records[i].name);
                PlayerPrefs.SetFloat(i.ToString(), _records[i].time);
            }
        }

        public Record ShowLastResult(Record newrecord)
        {
            return newrecord;
        }
        public void Initialize()
        {
            for (int i = 0; i < 10; i++)
            {
                PlayerPrefs.SetString(i.ToString(), "");
                PlayerPrefs.SetFloat(i.ToString(), float.MaxValue);
            }
        }
        private void Awake()
        {
            //Initialize();
        }
        public void LoadRecords()
        {
            Record record = new();
            _records.Clear();
            for (int i = 0; i < 10; i++)
            {
                record.name = PlayerPrefs.GetString(i.ToString());
                record.time = PlayerPrefs.GetFloat(i.ToString());
                _records.Add(record);
            }

        }
    }
}
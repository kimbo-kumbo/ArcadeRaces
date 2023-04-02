using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using System;

namespace RacePrototype
{
    public class Statistics_View : MonoBehaviour
    {
        private Dictionary<int, Text[]> staticsList = new Dictionary<int, Text[]>();
        private Text text;
        private Statistics_Controller _statistics_Controller;
        private FinishTrigger _finishTrigger;
        private StatisticsPanel_Marker _marker;
        [SerializeField] private Text _lastResultText;
        [SerializeField] private InputField _lastResultName;
        
        private void Awake()
        {
            _finishTrigger = FindObjectOfType<FinishTrigger>();
        }
        private void OnEnable()
        {
            _finishTrigger.OnFinish += ViewLastResult;
            _lastResultName.onEndEdit.AddListener(ToText);
        }

        public void ToText(string inputText)
        {
            Record newrecord;
            newrecord.name = inputText;
            newrecord.time = float.Parse(_lastResultText.text);
            _statistics_Controller.SaveRecord(newrecord);
            LoadStatsInfo();

        }

        private void ViewLastResult(Record record)
        {
            _lastResultText.text = record.time.ToString();
        }
        private void OnDisable()
        {
            _finishTrigger.OnFinish -= ViewLastResult;
        }

        private void Start()
        {
            _marker = GetComponentInChildren<StatisticsPanel_Marker>();
            _statistics_Controller = GetComponent<Statistics_Controller>();            
            var temp = GetComponentsInChildren<PlaceExample_Marker>();

            for (int i = 0; i < 10; i++)
            {
                staticsList.Add(i, new Text[] { temp.First(x => x.index == i).GetComponentInChildren<PlaceTim_Marker>().GetComponent<Text>(), temp.First(x => x.index == i).GetComponentInChildren<DriverName_Marker>().GetComponent<Text>() });
                Text[] tempList = staticsList[i];
                tempList[1].text = i.ToString();
            }

            _statistics_Controller.LoadRecords();
            _statistics_Controller.AddListener();
            LoadStatsInfo();
            _marker.gameObject.SetActive(false);
        }


        public void LoadStatsInfo()
        {
            for (int i = 0; i < 10; i++)
            {
                Text[] tempList = staticsList[i];
                tempList[1].text = _statistics_Controller._records[i].name;
                tempList[0].text = _statistics_Controller._records[i].time.ToString();
            }
        }
    }
}
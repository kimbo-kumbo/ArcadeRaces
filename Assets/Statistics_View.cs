using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace RacePrototype
{
    public class Statistics_View : MonoBehaviour
    {
        private Dictionary<int, Text[]> staticsList = new Dictionary<int, Text[]>();

        private Statistics_Controller _statistics_Controller;
        private StatisticsPanel_Marker _marker;

        private void Awake()
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
            LoadStatsInfo();
        }

        private void Start()
        {
            _marker.gameObject.SetActive(false);
        }


        private void LoadStatsInfo()
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
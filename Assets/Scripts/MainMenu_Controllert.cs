using UnityEngine;
using UnityEngine.UI;


namespace RacePrototype
{
    public class MainMenu_Controllert : Base_Controller
    {
        [SerializeField] private Button _start;
        [SerializeField] private Button _tuning;
        [SerializeField] private Button _exit;

        private void OnEnable()
        {
            _start.onClick.AddListener(delegate { LoadScene(SceneExample.Drive); });
            _tuning.onClick.AddListener(delegate { LoadScene(SceneExample.Tuning); });
            _exit.onClick.AddListener(delegate { LoadScene(SceneExample.Exit); });
        }

        private void OnDisable()
        {
            _start.onClick.RemoveListener(delegate { LoadScene(SceneExample.Drive); });
            _tuning.onClick.RemoveListener(delegate { LoadScene(SceneExample.Tuning); });
            _exit.onClick.RemoveListener(delegate { LoadScene(SceneExample.Exit); });
        }       
    }
}
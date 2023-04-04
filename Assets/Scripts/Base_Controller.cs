using UnityEngine;
using UnityEngine.SceneManagement;

namespace RacePrototype
{
    public class Base_Controller : MonoBehaviour
    {
        protected void LoadScene(SceneExample sceneExample)
        {
            if (sceneExample == SceneExample.Drive)
                SceneManager.LoadScene(1);
            if (sceneExample == SceneExample.Tuning)
                SceneManager.LoadScene(2);
            if (sceneExample == SceneExample.MainMenu)
                SceneManager.LoadScene(0);
            if (sceneExample == SceneExample.Exit)
            {
                Application.Quit();
                Debug.Log("Выход из приложения");
            }
        }
    }
}
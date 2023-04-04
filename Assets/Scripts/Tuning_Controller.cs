using System;
using UnityEngine;
using UnityEngine.UI;


namespace RacePrototype
{
    public class Tuning_Controller : Base_Controller
    {
        [SerializeField] private TuningSO_Model tuningSO;
        [SerializeField] private Slider _sliderStearAngle; 
        [SerializeField] private Slider _sliderDamperAvto; 
        [SerializeField] private Slider _sliderMassAvto; 
        [SerializeField] private Slider _sliderCenterMassAvto; 
        [SerializeField] private Slider _sliderTorqie;
        [SerializeField] private Button _driveButton;
        [SerializeField] private Button _mainMenuButton;
        public Action<TuningSO_Model> OnValueChange;

        private void OnEnable()
        {            
            _sliderStearAngle.value = tuningSO.MaxSteerAngle;
            _sliderDamperAvto.value = tuningSO.DamperAvto ;
            _sliderMassAvto.value = tuningSO.MassAvto ;
            _sliderCenterMassAvto.value = tuningSO.CenterMassAvto;
            _sliderTorqie.value = tuningSO.Torque ;

            _driveButton.onClick.AddListener(delegate { LoadScene(SceneExample.Drive); });
            _mainMenuButton.onClick.AddListener(delegate { LoadScene(SceneExample.MainMenu); });
        }

        private void OnDisable()
        {
            _driveButton.onClick.RemoveListener(delegate { LoadScene(SceneExample.Drive); });
            _mainMenuButton.onClick.RemoveListener(delegate { LoadScene(SceneExample.MainMenu); });
        }


        private void Update()
        {            
            tuningSO.MaxSteerAngle = _sliderStearAngle.value;            
            tuningSO.DamperAvto = _sliderDamperAvto.value;
            tuningSO.MassAvto = _sliderMassAvto.value;
            tuningSO.CenterMassAvto =_sliderCenterMassAvto.value ;
            tuningSO.Torque = _sliderTorqie.value;

            OnValueChange?.Invoke(tuningSO);
        }        
    }
}
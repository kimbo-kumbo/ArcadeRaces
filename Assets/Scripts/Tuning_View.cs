using RacePrototype;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuning_View : MonoBehaviour
{
    [SerializeField] private Text _textStearAngle;
    [SerializeField] private Text _textDamperAvto;
    [SerializeField] private Text _textMassAvto;
    [SerializeField] private Text _textCenterMassAvto;
    [SerializeField] private Text _textSlidingTorque;

    [SerializeField] private Tuning_Controller _tuningController;

    private void OnEnable()
    {
        _tuningController.OnValueChange += ChangeViewUIText;
    }

    private void OnDisable()
    {
        _tuningController.OnValueChange -= ChangeViewUIText;
    }

    //заполняем данные UI из TuningSO_Model
    private void ChangeViewUIText(TuningSO_Model currentSettingsAvto)
    {
        _textStearAngle.text = currentSettingsAvto.MaxSteerAngle.ToString();
        _textDamperAvto.text = currentSettingsAvto.DamperAvto.ToString();
        _textMassAvto.text = currentSettingsAvto.MassAvto.ToString();
        _textCenterMassAvto.text = currentSettingsAvto.CenterMassAvto.ToString();
        _textSlidingTorque.text = currentSettingsAvto.Torque.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is NULL");
            }
            return _instance;
        } }

    [SerializeField]
    private Slider _masterSlider = null, _musicSlider = null, _sFXSlider = null;

    private void Awake()
    {
        _instance = this;
    }

    public void SetVolumeSliders(float masterVol, float musicVol, float sFXVol)
    {
        _masterSlider.value = masterVol;
        _musicSlider.value = musicVol;
        _sFXSlider.value = sFXVol;
    }
}

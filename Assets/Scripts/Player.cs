using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    public static event Action<int> onMusicChange;

    private void Start()
    {
        onMusicChange?.Invoke(0);
    }

    public void NewMusic(int type)
    {
        onMusicChange?.Invoke(type);
    }
}

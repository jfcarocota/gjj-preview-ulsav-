using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    Button btnSource;

    void Start()
    {
        btnSource = GetComponent<Button>();
        btnSource.onClick.AddListener(PauseClick);
    }

    void PauseClick() => Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
}

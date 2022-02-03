using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button btnStart;
    [SerializeField]
    Button btnOptions;
    [SerializeField]
    Button btnExit;

    void Start()
    {
        btnStart.onClick.AddListener(()=> {
            SceneManager.LoadScene(1);
        });
        btnOptions.onClick.AddListener(()=> {

        });
        btnExit.onClick.AddListener(()=> {
            Application.Quit();
        });
    }
}

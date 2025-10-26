using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class black : MonoBehaviour
{
    public Button startButton;
    public Button settingButton;
    public Button progressButton;
    public Button exitButton;


    private void Awake()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener((() => { SceneManager.LoadScene(1); }
            ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
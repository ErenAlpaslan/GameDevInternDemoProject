using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public GameObject startUi;
    public GameObject progressUi;

    public bool isStop = true;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(){
        if (isStop)
        {
            startUi.SetActive(false);
            progressUi.SetActive(true);
            Time.timeScale = 1;
            isStop = false;
            StartCoroutine(CylinderSpawner.instance.spawnCylinder());
        }
    }
}

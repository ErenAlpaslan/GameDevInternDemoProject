using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private int amountOfCylinderPiece = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        amountOfCylinderPiece += 1;
        if (amountOfCylinderPiece > 9)
        {
            GetComponent<Animator>().SetTrigger("open");
            amountOfCylinderPiece = 0;
        }
    }
}

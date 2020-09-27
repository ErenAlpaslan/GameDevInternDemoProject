using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    private int surfaceHitCount = 0;
    private PhysicMaterial bouncyPhysicMaterial;

    public float shakeAmount = 1f;

    public bool isShaked = false;

    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bouncyPhysicMaterial = new PhysicMaterial();
        bouncyPhysicMaterial.bounciness = 1;
        bouncyPhysicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;
        bouncyPhysicMaterial.dynamicFriction = 0.2f;
        bouncyPhysicMaterial.staticFriction = 0.2f;
        GetComponent<CapsuleCollider>().material = bouncyPhysicMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "main_surface" && surfaceHitCount < 2){
            
            
            if (!isShaked)
            {
                switch (shakeAmount)
                {
                    case 1f:
                        ScreenShakeController.ShakeOnce(amount: new Vector3(shakeAmount, shakeAmount, 0));
                        audioSource.Play();
                        shakeAmount = 0.3f;
                        break;
                    case 0.3f:
                        ScreenShakeController.ShakeOnce(amount: new Vector3(shakeAmount, shakeAmount, 0));
                        audioSource.volume = shakeAmount;
                        audioSource.Play();
                        shakeAmount = 0.1f;
                        break;
                    case 0.1f:
                        ScreenShakeController.ShakeOnce(amount: new Vector3(shakeAmount, shakeAmount, 0));
                        isShaked = true;
                        break;    
                }
            }
            GetComponent<CapsuleCollider>().material.bounciness -= 0.5f;
            surfaceHitCount += 1;
        }
    }




}

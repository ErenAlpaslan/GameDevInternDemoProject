using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{
    private int playerPoint = 0;
    public GameObject PlaneSlicer;
    public GameObject sawdustParticleSystem;

    private GameObject previousGameObject = null;

    [Header("Audio Clips")]
    public AudioClip cutAudioClip;
    public AudioClip successfulCutAudioClip;
    public AudioClip unsuccessfulCutAudioClip;

    private AudioSource audioSource;

    public TextMeshProUGUI pointText;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pointText.SetText(playerPoint+"");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other) {
        Debug.Log("TAG, "+other.tag);
        
        if (other.tag == "cylinder_hit_point")
        {
            GameObject parentGameObject = other.gameObject.transform.parent.gameObject;

            if (previousGameObject != parentGameObject)
            {
                Destroy(other.gameObject);
                Debug.Log("VELOCITY: "+parentGameObject.GetComponent<Rigidbody>().velocity);
                StartCoroutine(sliceCylinder(parentGameObject));
                previousGameObject = parentGameObject;
            }
           
        }

    }


    IEnumerator sliceCylinder(GameObject parent){
        audioSource.clip = cutAudioClip;
        audioSource.Play();
        float speed = this.GetComponent<PlayerMovement>().playerMovementSpeed;
        this.GetComponent<PlayerMovement>().playerMovementSpeed = 0;
        sawdustParticleSystem.GetComponent<ParticleSystem>().Play();
        parent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        ScreenShakeController.ShakeOnce(amount: new Vector3(0.5f, 0.5f, 0));
        yield return new WaitForSecondsRealtime(0.1f);
        PlaneSlicer.GetComponent<SlicerPlane>().slice(parent);
        playerPoint += 1;
        pointText.SetText(playerPoint+"");
        audioSource.clip = successfulCutAudioClip;
        audioSource.Play();
        this.GetComponent<PlayerMovement>().playerMovementSpeed = speed;
    }

}

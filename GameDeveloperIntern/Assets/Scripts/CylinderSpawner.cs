using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSpawner : MonoBehaviour
{


    public static CylinderSpawner instance;
    enum CylinderType {
        Fat,
        LongLarge,
        Long,
        Meduim,
    }
    
    [Header("Cylinder Spawner")]
    public GameObject cylinder;

    public GameObject cylinderSpawnPoint;
    public float spawnTimeMax;
    public float spawnTimeMin;  

    

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
    public IEnumerator spawnCylinder(){
        while(true){
            yield return new WaitForSecondsRealtime(Random.Range(spawnTimeMin, spawnTimeMax));
            GameObject spawnedCylinder = cylinder;
            resizeCylinder(spawnedCylinder);

            Instantiate(spawnedCylinder, new Vector3(
                cylinderSpawnPoint.transform.position.x + Random.Range(-1.8f, 1.8f), 
                cylinderSpawnPoint.transform.position.y, cylinderSpawnPoint.transform.position.z), 
                spawnedCylinder.transform.rotation);
        }
    }

    private void resizeCylinder(GameObject spawnedCylinder){
        var cylinderType = RandomEnumValue<CylinderType> ();

        Rigidbody spawnedCylinderRigid = spawnedCylinder.GetComponent<Rigidbody>();
        Transform cylinderHitPoint = spawnedCylinder.transform.GetChild(0);

        cylinderHitPoint.localPosition = new Vector3(
          spawnedCylinder.transform.position.x ,
          cylinderHitPoint.position.y + Random.Range(spawnedCylinder.transform.localScale.y * -0.50f, spawnedCylinder.transform.localScale.y * 0.50f),
          spawnedCylinder.transform.position.z  
        );

        switch (cylinderType)
        {
            case CylinderType.Fat:
                spawnedCylinder.transform.localScale = new Vector3(1.2f, 1.1f, 1.2f);
                spawnedCylinderRigid.drag = 0.8f;
                break;
            case CylinderType.Long:
                spawnedCylinder.transform.localScale = new Vector3(0.6f, 1.5f, 0.6f);
                break;
            case CylinderType.LongLarge:
                spawnedCylinder.transform.localScale = new Vector3(0.6f, 1.2f, 0.6f);
                break;
            case CylinderType.Meduim:
                spawnedCylinder.transform.localScale = new Vector3(0.7f, 1f, 0.7f);
                break;
            
        }
    }

    static T RandomEnumValue<T> ()
    {
        var v = CylinderType.GetValues (typeof (T));
        return (T) v.GetValue (Random.Range(0, v.Length));
    }
}

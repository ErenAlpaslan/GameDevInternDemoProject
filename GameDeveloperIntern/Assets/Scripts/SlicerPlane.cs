using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SlicerPlane : MonoBehaviour
{

    public LayerMask mask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        EzySlice.Plane cuttingPlane = new EzySlice.Plane();

        cuttingPlane.Compute(this.transform);

        cuttingPlane.OnDebugDraw();
    }

    public void slice(GameObject obj){
        
        Material material = obj.GetComponent<MeshRenderer>().material;
        //GameObject[] slicedObjects = SliceObject(gameObject, transform.position, transform.rotation.eulerAngles, mat);
        
        /*GameObject[] slicedObjects = SliceObject(obj, transform.position, transform.rotation.eulerAngles, mat);
        SlicedHull cuttedObject = cutObjects(obj, material);
        GameObject leftSide = cuttedObject.CreateUpperHull(obj, material);
        GameObject rightSide = cuttedObject.CreateLowerHull(obj, material);

        addComponent(slicedObjects);
        Destroy(obj);*/
        Destroy(obj);

        Collider[] cuttedObjects = Physics.OverlapBox(this.transform.position, new Vector3(2f, 1f, 2f), Quaternion.identity, mask);
   
        foreach (Collider item in cuttedObjects)
        {

            Destroy(item.gameObject);

            SlicedHull cuttedObject = cutObjects(item.GetComponent<Collider>().gameObject, material);
            GameObject leftSide = cuttedObject.CreateUpperHull(item.gameObject, material);
            GameObject rightSide = cuttedObject.CreateLowerHull(item.gameObject, material);

            addComponent(leftSide);
            addComponent(rightSide);
            
           
        }
    }

    private SlicedHull cutObjects(GameObject obj, Material mat = null){
        return obj.Slice(gameObject.transform.position, transform.up * -1f, mat);
    }

    private void addComponent(GameObject obj){
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        obj.GetComponent<Rigidbody>().AddExplosionForce(100, obj.transform.position, 20);
    }
}

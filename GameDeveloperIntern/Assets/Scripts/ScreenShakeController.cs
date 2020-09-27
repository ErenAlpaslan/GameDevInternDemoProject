using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{

    public static ScreenShakeController instance;

    public Vector3 Amount = new Vector3(1f, 1f, 0);

    public float Duration = 1;

    public float Speed = 10;

    public AnimationCurve Curve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    public bool DeltaMovement = true;

    protected Camera mainCamera;

    private float time = 0;
    private Vector3 lastPos;
    private Vector3 nextPos;
    private float lastFoV;
    private float nextFoV;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        mainCamera = GetComponent<Camera>();
    }

    public static void ShakeOnce(float duration = 1f, float speed = 10f, Vector3? amount = null, Camera camera = null, bool deltaMovement = true, AnimationCurve curve = null){
        //set data 
        var instance = ((camera != null) ? camera : Camera.main).gameObject.AddComponent<ScreenShakeController>();
        instance.Duration = duration;
        instance.Speed = speed;
        if(amount != null){
            instance.Amount = (Vector3) amount;
        }

        if (curve != null)
        {
            instance.Curve = curve;
        }

        instance.DeltaMovement = deltaMovement;
        instance.Shake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake(){
        ResetCam();
        time = Duration;
    }

    private void LateUpdate(){
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time > 0)
            {
                //next position based on perlin noise
                nextPos = (Mathf.PerlinNoise(time * Speed, time * Speed * 2) - 0.5f) * Amount.x * transform.right * Curve.Evaluate(1f - time / Duration)
                 + (Mathf.PerlinNoise(time * Speed * 2, time * Speed) - 0.5f) * Amount.y * transform.up * Curve.Evaluate(1f - time / Duration);
                nextFoV = (Mathf.PerlinNoise(time * Speed * 2, time * Speed * 2) - 0.5f) * Amount.z * Curve.Evaluate(1f - time / Duration);

                mainCamera.fieldOfView += (nextFoV - lastFoV);
                mainCamera.transform.Translate(DeltaMovement ? (nextPos - lastPos) : nextPos);

                lastPos = nextPos;
                lastFoV = nextFoV;

            }else {
                //last frame
                ResetCam();
            }
        }
    }

    private void ResetCam(){

        mainCamera.transform.Translate(DeltaMovement ? -lastPos : Vector3.zero);
        mainCamera.fieldOfView -= lastFoV;

        //clear values
        lastPos = nextPos = Vector3.zero;
        lastFoV = nextFoV = 0f;
    }



}

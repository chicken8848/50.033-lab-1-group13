using UnityEngine;

public class DiscoBackground : MonoBehaviour
{
    public Transform mariotransform;
    public Vector3 cameraOffset = new Vector3(0,0,-10);
    Camera cam;
    Transform camTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>(); 
        camTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        cam.backgroundColor = new Color(Random.value, Random.value, Random.value);
        camTransform.position = mariotransform.position + cameraOffset;
        camTransform.localRotation = mariotransform.localRotation;
    }
}

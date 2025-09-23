using UnityEngine;

public class DiscoBackground : MonoBehaviour
{
    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>(); 
    }

    void FixedUpdate()
    {
        cam.backgroundColor = new Color(Random.value, Random.value, Random.value);
    }
}

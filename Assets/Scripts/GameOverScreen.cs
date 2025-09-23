using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<Canvas>(); 
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver() {
        canvas.enabled = true; 
    }

    void GameStart() {
        canvas.enabled = false;
    }
}

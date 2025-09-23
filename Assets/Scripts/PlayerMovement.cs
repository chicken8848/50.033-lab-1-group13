using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // changeable values
    public float speed = 10;
    public float up_speed = 20;
    public float max_speed = 20;
    public float linear_damp = 1.2f;
    public float spin_speed = 300f;
    // press and hold
    private bool charging;
    private float charge_timer;
    // Mario Physics
    private bool on_ground_state = false;
    private Rigidbody2D mariobody;
    private Vector2 move_vec;
    // Input controller
    PlayerInput playerInput;
    public Canvas canvas;
    // Mario Sprites
    SpriteRenderer mario_sp;
    private bool faceRightState = true;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        mario_sp = GetComponent<SpriteRenderer>();
        InputSystem.actions.Disable();
        playerInput.currentActionMap?.Enable();
        Application.targetFrameRate = 30;
        mariobody = GetComponent<Rigidbody2D>();
    }

    void processMovement() {
        if (Mathf.Abs(move_vec.magnitude) > 0) {
            Vector2 movement = new Vector2(move_vec.x, 0);
            if (mariobody.linearVelocity.magnitude < max_speed) {
                mariobody.AddForce(movement * speed);
            }
        }
        if (move_vec == Vector2.zero) {
            //Vector2 opp_vector = mariobody.linearVelocity * -1 * linear_damp;
            //mariobody.AddForce(opp_vector);
            //mariobody.linearVelocity = Vector2.zero;
        }
        if (on_ground_state) {
            mariobody.rotation = Mathf.LerpAngle(mariobody.rotation, 0, 0.2f);
            mariobody.angularVelocity = 0f;
        }
        if (move_vec.x > 0 && !faceRightState) {
            faceRightState = true;
            mario_sp.flipX = false;
        }
        if (move_vec.x < 0 && faceRightState) {
            faceRightState = false;
            mario_sp.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        processMovement();
    }

    public void OnMove(InputValue value) {
        move_vec = value.Get<Vector2>();
    }

    public void OnJump(InputValue value) {
        Vector2 upVector = new Vector2(0, 1);
        if (on_ground_state) {
            mariobody.AddForce(upVector * up_speed, ForceMode2D.Impulse);
            mariobody.AddTorque(spin_speed);
            on_ground_state = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Ground")) on_ground_state = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with goomba!");
            Time.timeScale = 0.0f;
            canvas.enabled = true;
        }
    }

    // other variables
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    // other methods

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        canvas.enabled = false;
        Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {
        // reset position
        mariobody.transform.position = new Vector3(-2.7f, 0.0f, 0.0f);
        move_vec = new Vector2(0f, 0f);
        mariobody.linearVelocity = Vector2.zero;
        mariobody.angularVelocity = 0.0f;
        // reset sprite direction
        faceRightState = true;
        mario_sp.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

    }
}

using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Rigidbody myRigidBody;
    [SerializeField] private LineRenderer myLineRenderer;
    [SerializeField] private float swinningSpeed;

    public LayerMask grappleLayer;
    private Vector3 _ropePoint;
    private bool _isStarted;
    private bool _isSwinning;
    #endregion 

    private void Start()
    {
        #region Starting Settings
        myRigidBody.useGravity = false;
        myLineRenderer.enabled = true;
        _isStarted = false;
        #endregion

        #region Creating the Starting Rope
        // We Determine the Point for the First Oscillation.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            _ropePoint = hit.point;
        }

        // Start swinging
        StartCoroutine(InitialSwing());
        #endregion
    }



    private void Update()
    {
        #region Player Controller
        if (GameManager.isGameStarted)
        {

            if (Input.GetMouseButtonDown(0))
            {
                StartSwinning();
                FindObjectOfType<AudioManager>().Play("Player Swinning");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopSwinning();
            }

            // If the oscillation continues
            else if (_isSwinning || !_isStarted)
            {
                // Draw the line
                myLineRenderer.positionCount = 2;
                myLineRenderer.SetPosition(0, transform.position);
                myLineRenderer.SetPosition(1, _ropePoint);

                // Push the rigidbody around the pivot point
                myRigidBody.velocity = Vector3.Cross(-transform.right, (transform.position - _ropePoint).normalized) * swinningSpeed;
            }

        }
        #endregion
    }
    private void StopSwinning()
    {
        // Enable Gravity to Get Out of Swing.
        _isSwinning = false;
        myRigidBody.useGravity = true;
        myLineRenderer.enabled = false;
    }

    private void StartSwinning()
    {
        // Set swinging state, in which we drive rather than gravity/physics
        _isSwinning = true;
        myLineRenderer.enabled = true;
        myRigidBody.useGravity = false;


        if (!_isStarted)
        {
            _isStarted = true;
        }
        else
        {
            // Cast a ray to forward/up
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward + transform.up, out hit,grappleLayer))
            {
                // Build line to next move
                _ropePoint = hit.point;
            }
        }
    }


    #region INITIALSWING
    // Pingpong speed to swing the sphere
    private IEnumerator InitialSwing()
    {
        // We discard the original speed value.
        float initialSpeed = swinningSpeed;
        float timeSpent = 0;

        while (!_isStarted)
        {
            // Adjusting the swing rate
            swinningSpeed = Mathf.PingPong(timeSpent * initialSpeed, initialSpeed * 2) - initialSpeed;
            timeSpent += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Done swinging. Restore speed value
        swinningSpeed = initialSpeed;
    }
    #endregion


    #region TRIGGERS

    private void OnCollisionEnter(Collision collision)
    {
        
        // If the name of the object we hit is Obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Play Player Impact Sound
            FindObjectOfType<AudioManager>().Play("Player Impact");
            // Open Game Over Panel
            Debug.Log("GAME OVER!");
            UIManager.Singleton.GameOver();
            // TODO : Score Save and Best Score.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the name of the object we hit is Window
        if (other.gameObject.CompareTag("Window"))
        {
            // Play Glass Breaking Sound
            FindObjectOfType<AudioManager>().Play("Glass Breakage");
            // Destroy Object
            Destroy(other.gameObject);
            // Add 10 to the score
            GameManager.Singleton.SetScore(10);
        }

    }

    #endregion
}


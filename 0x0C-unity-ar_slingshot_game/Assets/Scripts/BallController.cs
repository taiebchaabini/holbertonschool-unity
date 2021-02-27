using UnityEngine;
using TMPro;
public class BallController : MonoBehaviour
{
    /// <summary>
    /// Scene used for trajectory line
    /// </summary>
    [SerializeField]
    public SimulatePhysics PredictionLineManager;
    /// <summary>
    /// Number of ammo in the game
    /// </summary>
    public int ammo = 7;
    /// <summary>
    /// Force used to launch the ball
    /// </summary>
    public float forceMultiplyer;
    /// <summary>
    /// Sound used when the ball is launched
    /// </summary>
    public AudioSource launch;
    /// <summary>
    /// Sound used when the ball is missed
    /// </summary>
    public AudioSource targetMiss;
    /// <summary>
    /// Sound used when the ball is hit
    /// </summary>
    public AudioSource targetHit;
    /// <summary>
    /// Sound used when the game is over
    /// </summary>
    public AudioSource gameOver;
    /// <summary>
    /// Button used at the end of the game to play again.
    /// </summary>
    public GameObject leaderBoard;
    // Used to check if the game is DEV mode or not
    [SerializeField]

    public GameController gameController;
    // MaxBall Y position on drag
    private float maxBallY;
    // Line used to draw the trajectory line

    private LineRenderer line;
    // checks if the mouse is actually pressed
    private bool mouseClick = false;
    // Gets mouse position on mouseDown
    private Vector3 mousePosition;
    // Rigid body of the ball
    private Rigidbody rb;
    // newPos used to handle new ball position
    private Vector3 newPos;
    // Keeps the ball in the center of the screen when reloaded
    private bool reloaded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        line.GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition;
        mouseClick = true;
    }

    public void FixedUpdate()
    {
        if (rb.velocity.z > 0f)
            line.positionCount = 0;
    }

    public void checkGameOver()
    {
        if (ammo <= 0)
        {
            reloaded = false;
            leaderBoard.SetActive(true);
            gameOver.Play();
            GameObject.Find("StartSound").GetComponent<AudioSource>().Stop();
            rb.isKinematic = true;
            transform.position = new Vector3(999, 999, 999);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Target(Clone)")
        {
            targetHit.Play();
            GameController.score += 10;
            GameObject.Find("ScoreText").GetComponent<TMP_Text>().text = GameController.score.ToString();
            Destroy(collision.gameObject);
            if (collision.gameObject.transform.parent.transform.childCount - 1 == 0)
            {
                ammo = 7;
                // Resets ammo UI
                foreach (Transform child in GameObject.Find("List").transform)
                    child.gameObject.SetActive(true);
                GameObject.Find("Manager").GetComponent<GameController>().initTargets();
            }
        }
        else if (ammo > 1)
            targetMiss.Play();

        Reload(true);
        checkGameOver();
    }

    public void Update()
    {
        if (reloaded || gameController.devMode && transform.position.y < -10f)
            Reload(false);
        if (!gameController.devMode)
        {
            if (GameController.gameStarted && transform.position.y < PlaneController.gamePlane.center.y)
            {
                if (ammo > 1)
                    targetMiss.Play();
                Reload(true);
                checkGameOver();
            }
        }
        if (mouseClick)
        {
            Vector3 y = transform.position + new Vector3(0, 0, transform.position.z) * -CalculDirection().y * forceMultiplyer * 2;
            y = Camera.main.WorldToViewportPoint(y);
            y.z = Mathf.Clamp01(y.z);
            y = Camera.main.ViewportToWorldPoint(y);
            if (y.y > maxBallY)
                y.y = maxBallY;
            transform.position = y;
            PredictionLineManager.LinePrediction(CalculDirection());
        }
    }

    public Vector3 CalculDirection()
    {
        // Rotation on the Y
        Vector3 direction = (mousePosition - Input.mousePosition) * forceMultiplyer;

        // Makes the direction relative to camera view
        direction = Camera.main.transform.TransformDirection(direction);
        return (direction);
    }

    public void Reload(bool useAmmo)
    {
        if (useAmmo)
        {
            ammo -= 1;
            GameObject.Find("List").transform.GetChild(ammo).gameObject.SetActive(false);
        }
        rb.isKinematic = true;
        newPos = Camera.main.transform.position;
        newPos.x = Screen.width / 2;
        newPos.z = 1.2f;
        newPos.y = Screen.height / 3;
        newPos = Camera.main.ScreenToWorldPoint(newPos);
        maxBallY = newPos.y;
        rb.isKinematic = true;
        transform.position = newPos;
        reloaded = true;
    }

    private void OnMouseUp()
    {
        reloaded = false;
        launch.Play();
        mouseClick = false;
        line.positionCount = 0;
        PredictionLineManager.Launch(CalculDirection(), transform.gameObject);
    }


}

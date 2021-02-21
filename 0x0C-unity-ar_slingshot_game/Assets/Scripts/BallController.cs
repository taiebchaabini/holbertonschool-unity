using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    public SimulatePhysics PredictionLineManager;

    public int ammo = 7;
    public float forceMultiplyer;
    private LineRenderer line;
    private bool mouseClick = false;
    private Vector3 mousePosition;
    private Vector3 mouseReleasePos;
    private Rigidbody rb;
    private GameObject Manager;
    private Vector3 newPos;
    private Camera cam;
    private bool reloaded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        Manager = GameObject.Find("Manager");
        cam = Camera.main;
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Target(Clone)")
        {
            GameController.score += 10;
            UIController.scoreText.text = GameController.score.ToString();
            Destroy(collision.gameObject);
        }
        rb.isKinematic = true;
        transform.position = new Vector3(999, 999, 999);
        ammo -= 1;
        Destroy(UIController.list.transform.GetChild(ammo).gameObject);
    }

    public void Update()
    {

        // Helps to keep the ball following the camera for a better experience
        if (reloaded)
            Reload();
        if (mouseClick)
        {
            PredictionLineManager.LinePrediction(CalculDirection());
        }
    }

    public Vector3 CalculDirection()
    {
        Vector3 cameraRot = Camera.main.transform.eulerAngles;
        var rotation = Quaternion.AngleAxis(cameraRot.y, Vector3.up);

        // Rotation on the Y
        Vector3 direction = rotation * (mousePosition - Input.mousePosition);

        
        rotation = Quaternion.AngleAxis(cameraRot.x, Vector3.forward);
        direction = rotation * direction;

     
        direction = new Vector3(Mathf.Abs(direction.x), direction.y, direction.z) * forceMultiplyer;
        return (direction);
    }

    public void Reload()
    {
        newPos = cam.transform.position;
        newPos.x = Screen.width / 2;
        newPos.z = 1.2f;
        if (newPos.y < 80f) newPos.y = 80f;
        if (newPos.y > 100f) newPos.y = 100f;
        newPos = cam.ScreenToWorldPoint(newPos);
        rb.isKinematic = true;
        transform.position = newPos;
        reloaded = true;
    }

    private void OnMouseUp()
    {
        mouseClick = false;
        reloaded = false;
        line.positionCount = 0;
        PredictionLineManager.Launch(CalculDirection(), transform.gameObject);
    }
}

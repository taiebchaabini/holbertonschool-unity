using UnityEngine;
using TMPro;
public class BallController : MonoBehaviour
{
    [SerializeField]
    public SimulatePhysics PredictionLineManager;

    public int ammo = 7;
    public float forceMultiplyer;
    private LineRenderer line;
    private bool mouseClick = false;
    private Vector3 mousePosition;
    private Vector3 mouseCurPosition;
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Target(Clone)")
        {
            GameController.score += 10;
            GameObject.Find("ScoreText").GetComponent<TMP_Text>().text = GameController.score.ToString();
            Destroy(collision.gameObject);
        }
        rb.isKinematic = true;
        transform.position = new Vector3(999, 999, 999);
        ammo -= 1;
        GameObject.Find("List").transform.GetChild(ammo).gameObject.SetActive(false);
    }

    public void Update()
    {
        // Helps to keep the ball following the camera for a better experience
        if (reloaded)
            Reload();
        if (mouseClick)
            PredictionLineManager.LinePrediction(CalculDirection());
    }

    public Vector3 CalculDirection()
    {
        Vector3 cameraRot = Camera.main.transform.eulerAngles;
        // var rotation = Quaternion.AngleAxis(cameraRot.y, Vector3.up);

        // Rotation on the Y
        Vector3 direction = (mousePosition - Input.mousePosition) * forceMultiplyer;

        // Makes the direction relative to camera view
        direction = Camera.main.transform.TransformDirection(direction);
        // rotation = Quaternion.AngleAxis(cameraRot.z, Vector3.forward);

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

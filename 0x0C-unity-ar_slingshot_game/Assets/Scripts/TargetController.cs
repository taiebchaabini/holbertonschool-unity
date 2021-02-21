using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Vector3 dest;

    public float timer;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 0.5f;
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, 5f, 1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

}

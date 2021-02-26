using UnityEngine;
using UnityEngine.AI;

// Walk to a random position and repeat
[RequireComponent(typeof(NavMeshAgent))]
public class RandomWalk : MonoBehaviour
{
    // Range of destination
    public float m_Range = 5.0f;
    // Used to move the target depending on the baked navmesh surface
    NavMeshAgent m_Agent;
    // Used to let targets looks at the ball always
    private GameObject ball;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        ball = GameObject.Find("Ammo");
    }

    void Update()
    {
        transform.LookAt(new Vector3(ball.transform.position.x, transform.position.y, ball.transform.position.z));
        if (m_Agent.pathPending || m_Agent.remainingDistance > 0.1f)
            return;

        m_Agent.destination = m_Range * Random.insideUnitCircle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    public Transform TargetCollectable;
    public Collider[] AroundCollectables;

    public float radius;
    public LayerMask layerMask;




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetDestination();
        }
    }

    void FixedUpdate()
    {
        CheckCollectableAround();
        FindClosestCollectable();
    }

    void SetDestination()
    {
        agent.destination = TargetCollectable.position;
    }

    void CheckCollectableAround()
    {
        Collider[] collectableAround = Physics.OverlapSphere(transform.position, radius, layerMask);

        AroundCollectables = collectableAround;
    }


    void FindClosestCollectable()
    {
        float lowestDist = Mathf.Infinity;

        for (int i = 0; i < AroundCollectables.Length; i++)
        {

            float dist = Vector3.Distance(AroundCollectables[i].transform.position, transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                TargetCollectable = AroundCollectables[i].transform;
            }

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Collectable":
                other.gameObject.layer = LayerMask.NameToLayer("Default");
                Invoke("SetDestination", 0.25f);
                break;
        }
    }


}

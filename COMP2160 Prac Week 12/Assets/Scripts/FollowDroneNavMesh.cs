using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class FollowDroneNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    private MovePlayerNavMesh player;
    private NavMeshAgent nav;
    void Awake()
    {
        player = FindObjectOfType<MovePlayerNavMesh>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
    }
}

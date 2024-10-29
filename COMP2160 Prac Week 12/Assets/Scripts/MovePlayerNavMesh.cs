using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovePlayerNavMesh : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputAction mousePosition;
    private InputAction mouseClick;
    [SerializeField] private LayerMask layerMask;
    private Vector3 destination;
    private NavMeshAgent agent;

    void Awake()
    {
        playerActions = new PlayerActions();
        mousePosition = playerActions.Movement.Position;
        mouseClick = playerActions.Movement.Click;
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        mousePosition.Enable();
        mouseClick.Enable();
    }

    void OnDisable()
    {
        mousePosition.Disable();
        mouseClick.Disable();
    }

    void Start()
    {
        mouseClick.performed += GetDestination;
        destination = transform.position;
    }

    void Update()
    {
        agent.SetDestination(destination);
    }

    private void GetDestination(InputAction.CallbackContext context)
    {
        Vector2 position = mousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            destination = hit.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        NavMeshPath navPath = GetComponent<NavMeshAgent>().path;
        if (navPath.corners.Length < 2)
        {
            return;
        }
        int lastWaypoint = navPath.corners.Length - 1;

        for (int i = 0; i < navPath.corners.Length-1; i++)
        {
            Gizmos.DrawLine(navPath.corners[i], navPath.corners[i+1]);
        }

    }
}

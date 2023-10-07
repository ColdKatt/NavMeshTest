using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Camera _cam;
    [SerializeField] private NavMeshSurface _surface;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"SURFACE: {_surface.name}\n" +
                  $"SIZE: {_surface.size}\n" +
                  $"BOUNDS:\n" +
                  $"    MIN: {_surface.navMeshData.sourceBounds.min}\n" +
                  $"    MAX: {_surface.navMeshData.sourceBounds.max}\n");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        var cameraRay = _cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(cameraRay, out var raycastHit);

        if (raycastHit.collider != null)
        {
            Debug.Log(raycastHit.point);
            _agent.SetDestination(raycastHit.point);
        }
        else
        {
            Debug.LogWarning("Collider is null");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Collector : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshData[] _surfaceDatas;
    [SerializeField] private GameObject _collectionPrefab;
    [SerializeField] private TMP_Text _collectionText;

    private int _currentRandomSurface = -1;

    private GameObject _currentCollection;

    private int _collectionCollected = 0;

    void Start()
    {
        SpawnCollection();
    }

    public void SpawnCollection()
    {
        var randomSurface = Random.Range(0, _surfaceDatas.Length);

        if (_currentRandomSurface == randomSurface)
        {
            SpawnCollection();
            return;
        }

        _currentRandomSurface = randomSurface;

        var _currentSurfaceBounds = _surfaceDatas[_currentRandomSurface];

        var bounds = new Vector3[] { _surfaceDatas[_currentRandomSurface].position - _surfaceDatas[_currentRandomSurface].sourceBounds.extents,
                                     _surfaceDatas[_currentRandomSurface].position + _surfaceDatas[_currentRandomSurface].sourceBounds.extents };

        var randomPrefabPosition = new Vector3(Random.Range(bounds[0].x, bounds[1].x), 0.5f, Random.Range(bounds[0].z, bounds[1].z));

        _currentCollection = Instantiate(_collectionPrefab, randomPrefabPosition, Quaternion.identity);

        Transport();
    }

    private void Transport()
    {
        _agent.SetDestination(_currentCollection.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionGameobject = collision.gameObject;

        if (collisionGameobject.TryGetComponent<Collection>(out Collection collection))
        {
            Destroy(collisionGameobject);
            SpawnCollection();
            ChangeText();
        }
    }

    private void ChangeText()
    {
        _collectionCollected++;
        _collectionText.text = $"Collected: {_collectionCollected}";
    }
}

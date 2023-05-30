using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloudHandler : MonoBehaviour
{
    public List<Transform> dwellerList;
    public float cloudMovingTime = 1f;
    [SerializeField] private float cloudVerticalOffset;
    [SerializeField] private Transform _cloud;
    [SerializeField] private ParticleSystem _rain;
    private int _dwellerIndex = 0;
    private bool _isMoving = false;
    
    private void Start()
    {
        _rain.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveToNextDweller();
        }
    }

    private void MoveToNextDweller()
    {
        if (dwellerList.Count == 0 ||
            _cloud == null ||
            _rain == null ||
            _isMoving)
        {
            return;
        }

        _isMoving = true;
        if (_rain.isPlaying)
        {
            _rain.Stop();
        }

        Transform currentDweller = dwellerList[_dwellerIndex];
        Vector3 targetPosition;
        targetPosition = new Vector3(
            currentDweller.position.x,
            currentDweller.position.y + cloudVerticalOffset,
            currentDweller.position.z 
            );

        StopAllCoroutines();
        StartCoroutine(MoveRoutine(targetPosition));

        _dwellerIndex++;
        if (_dwellerIndex == dwellerList.Count) { _dwellerIndex = 0; }
    }

    private IEnumerator MoveRoutine(Vector3 target)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = _cloud.position;
        while (elapsedTime < cloudMovingTime)
        {
            _cloud.position = Vector3.Lerp(startPosition, target, elapsedTime / cloudMovingTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _cloud.position = target;
        _rain.Play();
        _isMoving = false;
        yield return null;
    }
}

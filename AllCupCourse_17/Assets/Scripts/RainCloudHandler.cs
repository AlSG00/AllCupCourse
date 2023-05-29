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
        if (_cloud == null)
        {
            return;
        }

        if (_rain == null)
        {
            return;
        }

        if (_rain.isPlaying)
        {
            _rain.Stop();
        }

        Transform currentDweller = dwellerList[_dwellerIndex];
        Vector3 targetPosition;
        targetPosition = new Vector3(
            currentDweller.position.x,
            currentDweller.position.y,
            currentDweller.position.z + cloudVerticalOffset
            );

        StopAllCoroutines();
        StartCoroutine(MoveRoutine(targetPosition));

        _dwellerIndex++;
        if (_dwellerIndex == dwellerList.Count) { _dwellerIndex = 0; }
    }

    private IEnumerator MoveRoutine(Vector3 target)
    {
        float elapsedTime = 0f;
        
        while(elapsedTime < cloudMovingTime)
        {
            elapsedTime += Time.deltaTime;
            _cloud.position = Vector3.Lerp(_cloud.localPosition, target, elapsedTime / cloudMovingTime);
            yield return null;
        }

        _cloud.position = target;
        _rain.Play();
        yield return null;
    }
}

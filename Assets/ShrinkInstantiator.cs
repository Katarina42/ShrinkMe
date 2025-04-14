using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ShrinkInstantiator : MonoBehaviour
{
    public GameObject shrinkMePrefab;
    private GameObject spawnedShrink;
    private ARRaycastManager raycastManager;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0 || EventSystem.current.IsPointerOverGameObject(0))
            return;

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (!raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            return;
        }
        
        var hitPose = hits[0].pose;

        if (spawnedShrink == null)
            spawnedShrink = Instantiate(shrinkMePrefab, hitPose.position, hitPose.rotation);
        else
            spawnedShrink.transform.position = hitPose.position;
    }
}

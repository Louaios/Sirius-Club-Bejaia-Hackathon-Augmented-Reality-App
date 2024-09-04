using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ImageTrackingHandler : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public GameObject prefabToSpawn;

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            
            GameObject obj = Instantiate(prefabToSpawn, trackedImage.transform.position, Quaternion.identity);
            spawnedObjects.Add(trackedImage.referenceImage.name, obj);
        }

        foreach (var trackedImage in args.updated)
        {
            
            if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out var obj))
            {
                obj.transform.position = trackedImage.transform.position;
                obj.transform.rotation = trackedImage.transform.rotation;
            }
        }

        foreach (var trackedImage in args.removed)
        {
            
            if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out var obj))
            {
                Destroy(obj);
                spawnedObjects.Remove(trackedImage.referenceImage.name);
            }
        }
    }
}
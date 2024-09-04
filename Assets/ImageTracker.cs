using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] arPrefabs;

    
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        
        foreach (var trackedImage in args.added)
        {
            foreach (var arPrefab in arPrefabs)
            {
                if (trackedImage.referenceImage.name == arPrefab.name)
                {
                    
                    GameObject newPrefab = Instantiate(arPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                   
                    prefabDict[trackedImage.referenceImage.name] = newPrefab;
                }
            }
        }

        
        foreach (var trackedImage in args.updated)
        {
            
            if (prefabDict.TryGetValue(trackedImage.referenceImage.name, out GameObject prefab))
            {
               
                prefab.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        
        foreach (var trackedImage in args.removed)
        {
            
            if (prefabDict.TryGetValue(trackedImage.referenceImage.name, out GameObject prefab))
            {
                
                Destroy(prefab);
                
                prefabDict.Remove(trackedImage.referenceImage.name);
            }
        }
    }
}

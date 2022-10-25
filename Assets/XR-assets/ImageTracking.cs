using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageTracking : MonoBehaviour
{

    [SerializeField] GameObject[] placeablePrefabs;

    Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);

        }

    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {

        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            SpawnImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void SpawnImage(ARTrackedImage trackedImage)
    {


        StartCoroutine(Spawn(trackedImage));

        foreach (GameObject go in spawnedPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }

        }
    }

    IEnumerator Spawn(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        while (trackedImage.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            yield return new WaitForEndOfFrame();
        }
        // yield return new WaitForSeconds(1);
        Vector3 position = trackedImage.transform.position;
        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        // animation
        float originalValue = prefab.transform.localScale.z;
        float shrinkValue = 0.1f;
        prefab.transform.localScale = prefab.transform.localScale * shrinkValue;
        while (prefab.transform.localScale.z < originalValue)
        {
            prefab.transform.localScale += prefab.transform.localScale * shrinkValue * 0.4f;
            yield return new WaitForEndOfFrame();
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

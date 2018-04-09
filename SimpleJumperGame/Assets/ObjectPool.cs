using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private List<GameObject> enabledObjects = new List<GameObject>();

    [SerializeField]
    private List<GameObject> disabledObjects = new List<GameObject>();

	void Awake ()
    {
        Instance = this;
	}
	
    public GameObject Instantiate(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        GameObject goToInstantiate = gameObject;
        if (disabledObjects.Contains(gameObject))
        {
            goToInstantiate.transform.position = position;
            goToInstantiate.transform.rotation = rotation;

            disabledObjects.Remove(goToInstantiate);

            goToInstantiate.SetActive(true);

            enabledObjects.Add(goToInstantiate);

            return goToInstantiate;
        }
        else 
        {
            goToInstantiate = Instantiate(gameObject, position, rotation);
            enabledObjects.Add(goToInstantiate);

            Debug.LogWarning("GameObject:" + gameObject.name +" wasn't in the disableObjects list, and was instantiated the old way");

            return goToInstantiate;
        }
    }

    public void Destroy(GameObject gameObject)
    {
        if (enabledObjects.Contains(gameObject))
        {
            gameObject.SetActive(false);
            disabledObjects.Add(gameObject);
        }
        else
        {
            Debug.LogWarning("The gameObject: " + gameObject.name + " was not in the pool, and could not be destroyed");
        }
    }
}

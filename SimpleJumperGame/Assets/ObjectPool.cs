using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private List<GameObject> enabledObjects = new List<GameObject>();

    private List<GameObject> disabledObjects = new List<GameObject>();

	void Awake ()
    {
        Instance = this;
	}
	
    
}

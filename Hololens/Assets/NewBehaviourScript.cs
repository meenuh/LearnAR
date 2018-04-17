using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    [SerializeField]
    GameObject obj;
    Hashtable ht;
	// Use this for initialization
	void Start () {
        ht.Add("y", 10);
        ht.Add("time", 4);
		iTween.MoveTo(obj, ht);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

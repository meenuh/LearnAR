using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCurrentManager : MonoBehaviour {
    [SerializeField]
    GameObject trailObj;
    [SerializeField]
    GameObject planeObj;
    [SerializeField]
    List<Transform> PointTransforms;
    [SerializeField]
    [Range(0f, 1f)]
    float Period = 0.5f;


    //void SetPoints(...) // use normalized coordinates
    //{
    //    PointTransforms =... // insantate new game objects
    //    pt.parent = planeObj.transform; // parent them to the plane
    //    pt.localPosition = ...
    //}

    void SetLocalPoints(IEnumerable<Vector3> local_points)
    {
        int node_number = 1;
        foreach(var local_point in local_points)
        {
            var node = new GameObject("node" + node_number);
            var node_trasform = node.transform;
            node_trasform.SetParent(planeObj.transform);
            node_trasform.localPosition = local_point;
            node_number++;
        }
    }

	// Use this for initialization
	IEnumerator Start () {
        //trailObj.transform.parent = planeObj.transform;
        //trailObj.transform.position = planeObj.transform.position;
        //trailObj.transform.SetParent(planeObj.transform, false);
        //Hashtable ht = new Hashtable();

        Hashtable opts = new Hashtable();
        for (; ;)
        {
            foreach(var pt in PointTransforms)
            {
                opts.Add("easetype", iTween.EaseType.linear);
                var local_pos = pt.localPosition;
                var world_pos = planeObj.transform.TransformPoint(local_pos);
                opts.Add("x", world_pos.x);
                opts.Add("y", world_pos.y);
                opts.Add("z", world_pos.z);
                opts.Add("time", Period);
                //iTween.MoveTo(trailObj, world_pos, Period);
                iTween.MoveTo(trailObj, opts);
                opts.Clear();
                yield return new WaitForSeconds(Period);
            }
        }

        //iTween.MoveTo(trailObj, ht);
        //Hashtable test = new Hashtable();


        //test.Add("x", 5);
        //iTween.MoveTo(trailObj, test);
        //test.Add("x", planeObj.transform.right);
        //test.Add("y", planeObj.transform.left);
        //test.Add("z", planeObj.transform.up);
        //iTween.MoveTo(trailObj, test);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // should be called by display manager when it receives positions from backend
    void ShowCurrentFlow()
    {
        
    }
}

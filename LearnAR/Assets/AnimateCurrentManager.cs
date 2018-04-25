using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCurrentManager : MonoBehaviour
{
    [SerializeField]
    GameObject trailObj;
    [SerializeField]
    GameObject planeObj;
    [SerializeField]
    List<Transform> PointTransforms;
    [SerializeField]
    [Range(0f, 1f)]
    float Period = 0.5f;

    void SetLocalPoints(IEnumerable<Vector3> local_points)
    {
        int node_number = 1;
        foreach (var local_point in local_points)
        {
            var node = new GameObject("node" + node_number);
            var node_trasform = node.transform;
            node_trasform.SetParent(planeObj.transform);
            node_trasform.localPosition = local_point;
            node_number++;
        }
    }

    IEnumerator Start()
    {

        Hashtable opts = new Hashtable();
        for (; ; )
        {
            foreach (var pt in PointTransforms)
            {
                opts.Add("easetype", iTween.EaseType.linear);
                var local_pos = pt.localPosition;
                var world_pos = planeObj.transform.TransformPoint(local_pos);
                opts.Add("x", world_pos.x);
                opts.Add("y", world_pos.y);
                opts.Add("z", world_pos.z);
                opts.Add("time", 0.5f);
                //iTween.MoveTo(trailObj, world_pos, Period);
                iTween.MoveTo(trailObj, opts);
                opts.Clear();
                yield return new WaitForSeconds(Period);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // should be called by display manager when it receives positions from backend
    void ShowCurrentFlow()
    {

    }
}
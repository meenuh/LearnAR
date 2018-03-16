using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {
    [SerializeField]
    public Texture2D circuitImage;
    private RawImage img = null;
    [SerializeField]
    private TextMesh t;
    [SerializeField]
    GameObject plane;

    public Material frontPlane;

    // Use this for initialization
    void Start () {
        t.text = "shit happened";
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    DisplayCircuit(0);
        //}
        //string filename = string.Format(@"CircuitImage{0}.jpg", circuitImageScript.imageCount - 1);
        //string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);
    }

    public void DisplayCircuit(int imageCount)
    {
        string filename = string.Format(@"CircuitImage{0}.jpg", imageCount);
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

        byte[] rawimage = File.ReadAllBytes(filePath);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (byte b in rawimage)
        {
            sb.Append(b);
        }
        //t.text = sb.ToString();
        t.text = "we did it";

        circuitImage = new Texture2D(4, 4);
        circuitImage.LoadImage(rawimage);
        frontPlane.mainTexture = circuitImage;

        // Apply to the plane
        MeshRenderer renderer = plane.GetComponent<MeshRenderer>();
        renderer.material = frontPlane;
    }
}

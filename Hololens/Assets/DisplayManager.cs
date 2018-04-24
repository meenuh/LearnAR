using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    [SerializeField]
    public Texture2D circuitImage;
    [SerializeField]
    private RawImage img = null;
    [SerializeField]
    private TextMesh t;
    [SerializeField]
    GameObject plane;
    Dictionary<string, float> components = new Dictionary<string, float>();

    public Material frontPlane;

    // Use this for initialization
    void Start()
    {
        t.text = "shit happened";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayCircuit(Texture2D targetTexture)
    {
        img.texture = targetTexture;
        //circuitImage = new Texture2D(4, 4);
        //circuitImage.LoadImage(rawimage);
        //frontPlane.mainTexture = circuitImage;

        //// Apply to the plane
        //MeshRenderer renderer = plane.GetComponent<MeshRenderer>();
        //renderer.material = frontPlane;
    }

    private void DisplayValues()
    {
        StringBuilder str = new StringBuilder();
        foreach (KeyValuePair<string, float> entry in components)
        {
            str.Append(entry.Key).Append(" = ").Append(entry.Value).Append('\n');
        }

        t.text = str.ToString();
    }

    public void SetVoltageFive()
    {
        if (components.ContainsKey("V"))
        {
            components["V"] = 5.0f;
        }
        else
        {
            components.Add("V", 5.0f);
        }
        // update values
        DisplayValues();
    }

    public void SetR1oneK()
    {
        if (components.ContainsKey("R1"))
        {
            components["R1"] = 1000.0f;
        }
        else
        {
            components.Add("R1", 1000.0f);
        }
        // update values
        DisplayValues();
    }
}

using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using Newtonsoft.Json;

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

    // place host IP here
    private string host = "127.0.0.1:5000";
    // expected URI here
    private string uri = "/circuit/image";

    private string url = "127.0.0.1:5000/circuit/image";


    public Material frontPlane;

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SendCircuitImage(byte []rawImage)
    {

        StringBuilder str = new StringBuilder();
        Dictionary<string, string> jsonObj = new Dictionary<string, string>();
        jsonObj.Add("image", rawImage.ToString());

        string jsonStr = JsonConvert.SerializeObject(jsonObj);

        str.Append(jsonStr);

        var req = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if(!req.isNetworkError)
        {

            str.Append(req.responseCode);
            str.Append(req.downloadHandler.text);
            //t.text = req.downloadHandler.text;
            if (req.downloadHandler.text == "")
            {
                str.Append("it's empty");
                t.text = str.ToString();
            }
        }
        else
        {
            t.text = "there was an error!";
        }
    }

    public void DisplayCircuit(Texture2D targetTexture)
    {
        img.texture = targetTexture;

        // not sure which to send yet ... raw bytes or base64 encoded string
        byte [] imgData = targetTexture.GetRawTextureData();
        string encodedImg = System.Convert.ToBase64String(targetTexture.EncodeToPNG());

        // send circuit image
        StartCoroutine(SendCircuitImage(imgData));
        //SendCircuitImage(imgData);
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
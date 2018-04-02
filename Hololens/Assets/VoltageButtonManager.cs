using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;

public class VoltageButtonManager : MonoBehaviour, IInputClickHandler
{
    private GestureRecognizer recognizer;
    [SerializeField]
    private TextMesh output;
   
    public void OnInputClicked(InputClickedEventData source)
    {
        // AirTap code goes here
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        output.text = "it was tapped!";
    }
    //public void OnInputDown(InputEventData eventData)
    //{
    //    output.text = "input down";
    //}
    //public void OnInputUp(InputEventData eventData)
    //{
    //    output.text = "input up";
    //}

    // Use this for initialization
    void Start () {
        output.text = "start";
        //recognizer = new GestureRecognizer();
        //recognizer.TappedEvent += OnInputClicked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

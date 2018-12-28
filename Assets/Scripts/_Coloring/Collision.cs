using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Collision : MonoBehaviour {

    [SerializeField]
    Colors.COLORS Color;

    [SerializeField]
    RawImage GreyLayer, WhiteLayer;    
      
    bool clicked = false;
    [SerializeField]
    AudioClip clipCorrect, clipWrong;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MouseDown() {
        OnMouseDown();
    }
   
    void OnMouseDown()
    {
        if (!clicked)
        {
            clicked = true;
            Colors.instance.ClickCheck();
        }

        //print("Clicked " + gameObject.name);
        if (Colors.selectedColor == Color)
        {            
            GreyLayer.enabled = false; WhiteLayer.enabled = false;
            print("correct color");
            UI_SoundFX.ins.Play(clipCorrect);
        }
        else if(Colors.selectedColor == Colors.COLORS.NULL)
        {

        }
        else
        {          
            print("not correct color, selected color: " + Colors.selectedColor + " area color: " + Color);
            GreyLayer.enabled = true; WhiteLayer.enabled = false;
            GreyLayer.color = Colors.instance.SelectedColorValue();
            UI_SoundFX.ins.Play(clipWrong);
        }                   
    }

	void OnMouseUp()
	{
		if(Colors.selectedColor == Colors.COLORS.NULL)
		{
			print("UP but null");
		}
		else
		{
			print("up with color");
		}
	}

    //public Colors.COLORS MyColor
    //{
    //    get { return Color; }
    //}
}

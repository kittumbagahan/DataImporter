using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorButton : MonoBehaviour {

	public static Transform activeButton;

    [SerializeField]
    Colors.COLORS myColor;

	Animator animator;

	[SerializeField]
	RawImage rawImage;

	// Use this for initialization
	void Start () {
		activeButton = null;
		animator = GetComponent<Animator>();
	}

	public Animator myAnimator
	{
		get{return animator;}
	}

	public RawImage MyRawImage
	{
		get {return rawImage;}
	}

	public void RawImageAlpha(Color32 newColor)
	{
		//rawImage.color = newColor;
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, newColor.a);
    }

    public Colors.COLORS ButtonColor
    {
        get { return myColor; }
    }
}

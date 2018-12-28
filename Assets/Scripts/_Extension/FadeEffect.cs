using UnityEngine;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public enum fadeEnum
	{
		toBlack, toClear
	}
	public fadeEnum FadeTo;
	Color c;
	GUITexture gText;
	
	void Awake ()
	{
		gText = GetComponent<GUITexture>();
		// Set the texture so that it is the the size of the screen and covers it.
		gText.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		c = gText.color;
	}
	void Start(){

	}

	void OnEnable(){
		gText.color = c;
		if(FadeTo == fadeEnum.toBlack){
			StartCoroutine(IEFadeToBlack());
		}else{
			StartCoroutine(IEFadeToClear());
		}

	}

	IEnumerator IEFadeToBlack()
	{
		while(gText.color.a < 0.98f){
			yield return new WaitForSeconds(Time.deltaTime);
			FadeToBlack();
		}
		gameObject.SetActive(false);
	}
	IEnumerator IEFadeToClear(){
		while(gText.color.a > 0.05f){
			yield return new WaitForSeconds(Time.deltaTime);
			FadeToClear();
		}
		gameObject.SetActive(false);
	}
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		gText.color = Color.Lerp(gText.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		gText.color = Color.Lerp(gText.color, Color.black, fadeSpeed * Time.deltaTime);

	}

	
	

	

}
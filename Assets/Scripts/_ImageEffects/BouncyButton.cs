using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BouncyButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {


    public bool interactible = true;
    public Vector2 mouseDownScale;
    public Vector2 mouseClickScale;

    [SerializeField]
    bool pointerUp;
    Vector2 originalScale;
    Button btn;
	void Start () {
        if(mouseClickScale == new Vector2(0,0))
        {
            float x = transform.localScale.x, y = transform.localScale.y;
            x -= x * 0.1f;
            y -= y * 0.1f;
            mouseDownScale = new Vector2(x, y);
            x += x * 0.1f;
            y += y * 0.1f;
            mouseClickScale = new Vector2(x, y);
        }
        btn = GetComponent<Button>();
        originalScale = transform.localScale;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        //print("mouse click");
        //pointerUp = true;
        //StartCoroutine(IEGrow());
        //StartCoroutine(IEBounce());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (btn != null)
        {
            if (btn.interactable && btn.enabled)
            {
                //print("mousedown");
                pointerUp = false;
                StartCoroutine(IEShrink());
            }
        }
        else {
            //print("mousedown");
            pointerUp = false;
            StartCoroutine(IEShrink());
        }
        
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (btn != null)
        {
            if (btn.interactable && btn.enabled)
            {
                pointerUp = true;
                interactible = false;
                StartCoroutine(IEGrow(0.4f, 0.3f));
                //transform.localScale = originalScale;
            }
        }
        else {
            pointerUp = true;
            interactible = false;
            StartCoroutine(IEGrow(0.4f, 0.3f));
        }
        
    }

    IEnumerator IEShrink()
    { 
        while((transform.localScale.x + transform.localScale.y > mouseDownScale.x + mouseDownScale.y) && pointerUp == false)
        {
            transform.localScale = Vector2.Lerp(transform.localScale,mouseDownScale, 0.5f);
            yield return null;
        }

        yield break;
    }

    IEnumerator IEShrink(Vector2 localscale, float spd)
    {
        
        while ((transform.localScale.x + transform.localScale.y > localscale.x + localscale.y))
        {
            transform.localScale = Vector2.Lerp(transform.localScale, localscale, spd);
            yield return null;
        }

        yield break;
    }

    IEnumerator IEGrow(float time, float spd)
    {
        //bool stop = false;
        //while ((transform.localScale.x + transform.localScale.y < mouseClickScale.x + mouseClickScale.y) && !stop)
        float t = time;
        while(transform.localScale.x <= mouseClickScale.x && t >= 0 && pointerUp == true)
        {
            t -= Time.fixedDeltaTime * 1f;
           transform.localScale = Vector2.Lerp(transform.localScale, mouseClickScale, spd);
           yield return new WaitForFixedUpdate();
        }
        interactible = true;
        //print("grow up dude");
        //transform.localScale = originalScale;
        StartCoroutine(IEShrink(originalScale, 0.2f));
        yield break;
    }

    //bounce 3
    //grow, shrink, grow

    
    
}

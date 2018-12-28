using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

interface IGrow { 
  void Grow();
}

interface IShrink {
    void Shrink();
}

public class BookCategory : MonoBehaviour, IPointerClickHandler, IGrow, IShrink {

	BookCarouselArrange book;

	[SerializeField]
	byte bookIndex;

	[SerializeField]
	string sceneName;

//	[SerializeField]
//	Text text;

	RectTransform rect;

	// Use this for initialization
	void Start () {
		book = GetComponent<BookCarouselArrange>();
		rect = GetComponent<RectTransform>();
	}

    public void Grow()
    { 
        if(transform.localScale.x < 1.5f)
        {
            transform.SetLocalHeight(Mathf.Lerp(transform.GetLocalHeight(), 1.5f, 0.1f));
            transform.SetLocalWidth(Mathf.Lerp(transform.GetLocalWidth(), 1.5f, 0.1f));
        }
    }

    public void Shrink()
    {
        if (transform.localScale.x > 0.8f)
        {
            transform.SetLocalHeight(Mathf.Lerp(transform.GetLocalHeight(), 0.8f, 0.1f));
            transform.SetLocalWidth(Mathf.Lerp(transform.GetLocalWidth(), 0.8f, 0.1f));
        }
    }

	void Update () {
		//Debug.Log(transform.position);
		if(rect.localPosition.x >= 120 && rect.localPosition.x <= 250)
		{
			//transform.localScale = new Vector2(1.5f, 1.5f);
            Grow();
            transform.SetAsLastSibling();
			Book.instance.BookCover(bookIndex);
			Book.instance.BOokDescription(bookIndex);
			GetComponent<Image>().raycastTarget = true;
		}
		else
		{
			//transform.localScale = new Vector2(0.8f, 0.8f);
            Shrink();
			GetComponent<Image>().raycastTarget = false;
		}

		if(rect.localPosition.x > 850)
		{
			rect.localPosition = new Vector3(book.AfterMe.GetComponent<RectTransform>().localPosition.x - 135,
			                                 book.AfterMe.GetComponent<RectTransform>().localPosition.y,
			                                 book.AfterMe.GetComponent<RectTransform>().localPosition.z);
		}
		else if(rect.position.x <= -350)
		{
			transform.localPosition = new Vector3(book.BeforeMe.GetComponent<RectTransform>().localPosition.x + 135,
			                                      book.BeforeMe.GetComponent<RectTransform>().localPosition.y,
			                                      book.BeforeMe.GetComponent<RectTransform>().localPosition.z);
		}
	}

	void ObjectDrag (float distance)
	{
		transform.position = new Vector2(transform.position.x + distance, transform.position.y);
		//text.text = rect.transform.position.ToString();
	}

	void OnEnable()
	{
		ContainerDrag.OnObjectDrag += ObjectDrag;
	}

	void OnDisable()
	{
		ContainerDrag.OnObjectDrag -= ObjectDrag;
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		KuyaTamAnimation.instance.OK();

		StartCoroutine("SceneLoadWait");

	}
	#endregion

	IEnumerator SceneLoadWait()
	{
		yield return new WaitForSeconds(1f);
		StartCoroutine(SceneLoader.instance.Load(sceneName));
	}
}
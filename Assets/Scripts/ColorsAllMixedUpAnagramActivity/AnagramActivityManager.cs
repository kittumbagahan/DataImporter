using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnagramActivityManager : MonoBehaviour {

	public static AnagramActivityManager instance;

	List<string> letter;

	[SerializeField] Image imgColorImage;
	[SerializeField] string[] color;
	[SerializeField] GameObject slotContainer, slotItem;
	[SerializeField] Sprite[] colorImage;
	[SerializeField] string correctAnswer;
	[SerializeField] InventoryManager inventoryManager;
	[SerializeField] AudioClip clipCorrect, clipInstruction;

	bool isCorrect;

	[SerializeField] int currIndex, endIndex;

	// Use this for initialization
	void Start () 
	{
		instance = this;

		currIndex = SaveTest.Set;
		endIndex = currIndex + 3;
		Item.OnBeginDrag += BeginDrag;
		isCorrect = true;
		StartCoroutine("PlayInstruction");
		switch(currIndex)
		{
		case 0: 
			ScoreManager.ins.maxMove = 12;
			break;
		case 3: 
			ScoreManager.ins.maxMove = 17;
			break;
		case 6: 
			ScoreManager.ins.maxMove = 15;
			break;
		case 9: 
			ScoreManager.ins.maxMove = 14;
			break;
		case 12: 
			ScoreManager.ins.maxMove = 16;
			break;	
		}
		ScoreManager.ins.AW();
	}
	
	IEnumerator PlayInstruction()
	{
		GetComponent<AudioSource>().PlayOneShot(clipInstruction);
		//yield return new WaitForSeconds(clipInstruction.length);
        yield return new WaitForSeconds(0.5f);
        CreateQuestion();
	}

	void BeginDrag(GameObject obj)
	{
		ScoreManager.ins.IncNumOfMoves();
	}

	void CreateQuestion()
	{

		if(slotContainer.transform.childCount > 0)
		{
			ClearInventory();

			for (int i = 0; i < slotContainer.transform.childCount; i++) 
			{
				Destroy(slotContainer.transform.GetChild(i).gameObject);
			}
		}

		correctAnswer = this.color[currIndex];	// set correct answer

		imgColorImage.sprite = colorImage[currIndex]; // set image for the image component

		letter = new List<string>(); // create new list

		//letter = this.color[currIndex].ToCharArray();
		for (int i = 0; i < this.color[currIndex].Length; i++)// get letters by index value and add to letter list
		{
			letter.Add(this.color[currIndex].Substring(i, 1));
		}

		letter = Shuffle(letter, 3, this.color[currIndex]); // shuffle the letters

		print(correctAnswer);

		for (int i = 0; i < letter.Count; i++) 
		{
			print(letter[i]);
		}

		ClearInventory();

		for (int i = 0; i < letter.Count; i++) // create letter slots
		{
			GameObject slot = Instantiate(slotItem);

			// adds slot and item in InventoryManager class
			inventoryManager.slots.Add(slot);
			inventoryManager.items.Add(slot.transform.GetChild(0).gameObject);

			slot.transform.GetChild(0).GetComponent<Item>().letterValue = letter[i];
            slot.transform.GetChild(0).GetComponent<Item>().Locked = true;
			slot.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = letter[i];
			slot.transform.SetParent(slotContainer.transform);
		}
        Invoke("UnlockItem", 2f);
        
		currIndex++;
	}

	public void Check()
	{
		print("check it");
		for (int i = 0; i < letter.Count; i++) 
		{
			print("check " + correctAnswer.Substring(i, 1) + " and " + slotContainer.transform.GetChild(i).transform.GetChild(0).GetComponent<Item>().letterValue);

			if(correctAnswer.Substring(i, 1) != slotContainer.transform.GetChild(i).transform.GetChild(0).GetComponent<Item>().letterValue)
			{
				isCorrect = false;
				break;
			}
			else
			{
				print("correct");
                
				isCorrect = true;
			}
		}

		if(isCorrect)
		{
            for (int i = 0; i < inventoryManager.items.Count; i++)
            {

                inventoryManager.items[i].GetComponent<Item>().Locked = true;
            }
			GetComponent<AudioSource>().PlayOneShot(clipCorrect);
			StartCoroutine("Delay");
		}
	}

    void UnlockItem()
    {
        for (int i = 0; i < inventoryManager.items.Count; i++ )
        {
            
            inventoryManager.items[i].GetComponent<Item>().Locked = false;
        }
    }


	IEnumerator Delay()
	{
		yield return new WaitForSeconds(1);
		if(currIndex < endIndex)
			CreateQuestion();
		else
			ActivityDone.instance.Done();

	}

	List<string> Shuffle(List<string> theStringList, int shuffleCount, string answer)
	{
		string tempString;
		int tempIndex;

		for (int shuffleIndex = 0; shuffleIndex < shuffleCount; shuffleIndex++)// repeat based on shuffle count
		{
			for (int letterIndex = 0; letterIndex < theStringList.Count; letterIndex++) 
			{
				for (int swapIndex = 0; swapIndex < letterIndex; swapIndex++) // loop and swap letters
				{
					tempIndex = Random.Range(0, letterIndex + 1);
					tempString = theStringList[swapIndex];
					theStringList[swapIndex] = theStringList[tempIndex];
					theStringList[tempIndex] = tempString;
				}
			}
		}

		string listTostring = "";
		for (int i = 0; i < theStringList.Count; i++) 
		{
			listTostring += theStringList[i];
		}

		print("list to string " + listTostring + " answer " + answer);

		if(listTostring == answer)
		{
			tempString = theStringList[0];
			theStringList[0] = theStringList[1];
			theStringList[1] = tempString;
		}

		return theStringList;
	}

	void ClearInventory()
	{
		inventoryManager.slots.Clear();
		inventoryManager.items.Clear();
	}
}

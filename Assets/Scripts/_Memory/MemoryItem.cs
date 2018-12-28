using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MemoryItem : MonoBehaviour {

    FadeOut fadeOut;
    RectTransform rect;
    Shrink shrink;
    Image img;
   
    void Start()
    { 
      fadeOut = transform.parent.GetComponent<FadeOut>();
      rect = GetComponent<RectTransform>();
      shrink = GetComponent<Shrink>();
      img = GetComponent<Image>();
      MemoryManager.ins.lstBtns.Add(GetComponent<Button>());
      img.enabled = false;
      Invoke("Cover",1f);
    }
   
    public void Peek(GameObject memory)
    {
      
		ScoreManager.ins.IncNumOfMoves();
           if (MemoryManager.ins.Memory1 == "")
           {
               MemoryManager.ins.Memory1 = memory.transform.parent.GetComponent<Image>().sprite.name;
               MemoryManager.ins.ObjMemory1 = this.gameObject;
			  
              
           }
           else if (MemoryManager.ins.Memory2 == "")
           {
               MemoryManager.ins.Memory2 = memory.transform.parent.GetComponent<Image>().sprite.name;
               MemoryManager.ins.ObjMemory2 = this.gameObject;
				
           }
           else
           {
               //MATCHED
           }
           if (MemoryManager.ins.Memory1 != "" && MemoryManager.ins.Memory2 != "")
           {
           
               MemoryManager.ins.Check();
            
           }
   
    }

    

    void FadeAway(bool fade) { 
        if(fade)
        {   
            fadeOut.Play();
          
        }
    }

    public void Cover()
    {
        shrink.Stop();
        img.enabled = false;
        Invoke("_cover", 0.5f);
    }


    void _cover()
    {
        rect.SetHeight(95f);
        rect.SetWidth(95f);
        img.enabled = true;
        MemoryManager.ins.ActivateItems(true);
    }
}

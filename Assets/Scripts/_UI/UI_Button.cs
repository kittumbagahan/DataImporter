using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class UI_Button : MonoBehaviour, IPointerClickHandler {


    public void OnPointerClick(PointerEventData e)
    {
        UI_SoundFX.ins.PlayUIButtonClick();
    }
	
}

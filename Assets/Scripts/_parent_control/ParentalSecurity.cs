using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParentalSecurity : MonoBehaviour {

    [SerializeField]
    Text txtInput, txtQ;

    int maxChar = 3, numOfInput;
    double answer;
    bool active = true;
    [SerializeField]
    AudioClip clipWrong, clipRight, clipKeypress;

    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        GenerateSimpleMathProblem();
    
    }
    void GenerateSimpleMathProblem()
    { 
        //div or multiply
        //int _operator = Random.Range(0,2);
        double a=0, b=0;
        a = Random.Range(2, 11); b = Random.Range(2, 11);
        answer = a * b;
        txtQ.text = a.ToString("") + "X" + b.ToString("");
        txtInput.text = "";
        //switch (_operator)
        //{ 
        //    case 0:
        //        a = Random.Range(1, 10); b = Random.Range(1,10);
        //        answer = a * b;
        //        break;
        //    default:
        //        a = Random.Range(1, 10); b = Random.Range(1,10);
        //        answer = a / b;
        //        txtQ.text = a.ToString("") + "/" + b.ToString("");
        //        break;
        //}
    }

    public void KeyInput(string s)
    {
        if(active)
        {
            if (s == "clear")
            {
                numOfInput = 0;
                txtInput.text = "";
            }
            else
            {

                if (numOfInput < maxChar)
                {
                    numOfInput++;
                    txtInput.text += s;
                }
                else {
                    StartCoroutine(IEWrong());
                
                }
            }
                
        }
        UserParentalManager.ins.PlayClip(clipKeypress);
        
    }

    public void Enter()
    { 
        if(!txtInput.text.Equals("") && active)
        {
            print("not null");
            print(answer);
            if (txtInput.text.Equals(answer.ToString()))
            {
                UserParentalManager.ins.PlayClip(clipRight);
                print("correct!");
                //UserParentalManager.ins.SpawnParentControl();
                StartCoroutine(IECorrect());
            }
            else
            {
                UserParentalManager.ins.PlayClip(clipWrong);
                StartCoroutine(IEWrong());
            }
        }
    }

    IEnumerator IEWrong()
    {
        Color c = txtInput.color;
        txtInput.color = Color.red;
        active = false;
        yield return new WaitForSeconds(1f);
        txtInput.color = c;
        txtInput.text = "";
        numOfInput = 0;
        active = true;
    }

    IEnumerator IECorrect()
    {
        Color c = txtInput.color;
        txtInput.color = Color.green;
        active = false;
        yield return new WaitForSeconds(0.5f);
        txtInput.color = c;
        active = true;
        UserParentalManager.ins.SpawnParentControl();
        this.gameObject.SetActive(false);
    }
}

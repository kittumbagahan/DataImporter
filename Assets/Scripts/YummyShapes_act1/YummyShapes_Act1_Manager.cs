using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class YummyShapes_Act1_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipCorrect, clipWrong;
    // public Shapes shapeOfObj;
    public Image imgObject;

    public Button[] UIBtn;
    public Sprite[] sprtBtnState_idle;
    public Sprite[] sprtBtnState_pressed;
    public Sprite[] sprtObject;

  
    AudioSource audSrc;
    [SerializeField]
    int index = 0, pts = 0;
    EShapes shapeAnswer;

   // bool[] booleanShapeUsed;

    bool[] booleanShapeUsed = new bool[6]; //random buttons

    Grow grow;

    void Start () {
       
        grow = imgObject.GetComponent<Grow>();
        audSrc = GetComponent<AudioSource>();
        //sprtObject.Shuffle();
        index = SaveTest.Set;
        GameOn();
		ScoreManager.ins.AW();
    }

    void GameOn()
    {
        Shape btnShape;
        FadeIn fadeIn;

       
        grow.Play();
        for (int i=0; i<booleanShapeUsed.Length; i++)
        {
            booleanShapeUsed[i] = false;
        }
        for (int i=0; i<UIBtn.Length; i++)
        {
            Shape shape = UIBtn[i].GetComponent<Shape>();
            shape.eShape = EShapes.non;
        }

        imgObject.sprite = sprtObject[index];
        GetObjectShape(imgObject.sprite.name); //answer
        UIBtn.Shuffle();
        btnShape = UIBtn[0].GetComponent<Shape>();
        btnShape.eShape = shapeAnswer;
        ChangeButtonSprite(UIBtn[0]);
        fadeIn = UIBtn[0].GetComponent<FadeIn>();
        fadeIn.Play();
        //-------------------------------------
        btnShape = UIBtn[1].GetComponent<Shape>();
        RandomizeShape(UIBtn[1]);
        //ChangeButtonSprite(UIBtn[1]);
        fadeIn = UIBtn[1].GetComponent<FadeIn>();
        fadeIn.Play();

        btnShape = UIBtn[2].GetComponent<Shape>();
        RandomizeShape(UIBtn[2]);
        //ChangeButtonSprite(UIBtn[2]);
        fadeIn = UIBtn[2].GetComponent<FadeIn>();
        fadeIn.Play();

        btnShape = UIBtn[3].GetComponent<Shape>();
        RandomizeShape(UIBtn[3]);
        //ChangeButtonSprite(UIBtn[3]);
        fadeIn = UIBtn[3].GetComponent<FadeIn>();
        fadeIn.Play();
    }

    void ChangeButtonSprite(Button btn)
    {
        SpriteState btnTempState = btn.spriteState;
        Image img = btn.GetComponent<Image>();
        Shape shape = btn.GetComponent<Shape>();
        string sprtName = imgObject.sprite.name;

        switch (shape.eShape)
        {
            case EShapes.non:
                break;
            case EShapes.circle:
                img.sprite = sprtBtnState_idle[0];
                btnTempState.pressedSprite = sprtBtnState_pressed[0];
                btnTempState.disabledSprite = sprtBtnState_pressed[0];
                break;
            case EShapes.oval:
                img.sprite = sprtBtnState_idle[1];
                btnTempState.pressedSprite = sprtBtnState_pressed[1];
                btnTempState.disabledSprite = sprtBtnState_pressed[1];
                break;
            case EShapes.rectangle:
                img.sprite = sprtBtnState_idle[2];
                btnTempState.pressedSprite = sprtBtnState_pressed[2];
                btnTempState.disabledSprite = sprtBtnState_pressed[2];
                break;
            case EShapes.square:
                img.sprite = sprtBtnState_idle[3];
                btnTempState.pressedSprite = sprtBtnState_pressed[3];
                btnTempState.disabledSprite = sprtBtnState_pressed[3];
                break;
            case EShapes.star:
                img.sprite = sprtBtnState_idle[4];
                btnTempState.pressedSprite = sprtBtnState_pressed[4];
                btnTempState.disabledSprite = sprtBtnState_pressed[4];
                break;
            case EShapes.triangle:
                img.sprite = sprtBtnState_idle[5];
                btnTempState.pressedSprite = sprtBtnState_pressed[5];
                btnTempState.disabledSprite = sprtBtnState_pressed[5];
                break;
            default:
                break;
        }
       
        btn.spriteState = btnTempState;
    }

    void RandomizeShape(Button btn)
    {
        for(int i=0; i<booleanShapeUsed.Length; i++)
        {
            Shape _shape = btn.GetComponent<Shape>();
            switch (i)
            {
                case 0:
                    if(booleanShapeUsed[i] == false && _shape.eShape == EShapes.non)
                    {
                        _shape.eShape = EShapes.circle;
                        booleanShapeUsed[i] = true;
                    }
                    break;
                case 1:
                    if (booleanShapeUsed[i] == false && _shape.eShape == EShapes.non)
                    {
                        _shape.eShape = EShapes.oval;
                        booleanShapeUsed[i] = true;
                    }
                    break;
                case 2:
                    if (booleanShapeUsed[i] == false && _shape.eShape == EShapes.non)
                    {
                        _shape.eShape = EShapes.rectangle;
                        booleanShapeUsed[i] = true;
                    }
                    break;
                case 3:
                    if (booleanShapeUsed[i] == false && _shape.eShape == EShapes.non)
                    {
                        _shape.eShape = EShapes.square;
                        booleanShapeUsed[i] = true;
                    }
                    break;
                case 4:
                    if (booleanShapeUsed[i] == false && _shape.eShape == EShapes.non)
                    {
                        _shape.eShape = EShapes.star;
                        booleanShapeUsed[i] = true;
                    }
                    break;
                case 5:
                    if (booleanShapeUsed[i] == false && _shape.eShape == EShapes.non)
                    {
                        _shape.eShape = EShapes.triangle;
                        booleanShapeUsed[i] = true;
                    }
                    break;
                default: break;
            }
        }
        ChangeButtonSprite(btn);
    }

    void GetObjectShape(string sprtName)
    {
     
        if (sprtName.ToUpper().Contains("CIRCLE"))
        {
            shapeAnswer = EShapes.circle;
            booleanShapeUsed[0] = true;
        }
        if (sprtName.ToUpper().Contains("OVAL"))
        {
            shapeAnswer = EShapes.oval;
            booleanShapeUsed[1] = true;
        }

        if (sprtName.ToUpper().Contains("SQUARE"))
        {
            shapeAnswer = EShapes.square;
            booleanShapeUsed[3] = true;
        }

        if (sprtName.ToUpper().Contains("RECTANGLE"))
        {
            shapeAnswer = EShapes.rectangle;
            booleanShapeUsed[2] = true;
        }

        if (sprtName.ToUpper().Contains("STAR"))
        {
            shapeAnswer = EShapes.star;
            booleanShapeUsed[4] = true;
        }
        if (sprtName.ToUpper().Contains("TRIANGLE"))
        {
            shapeAnswer = EShapes.triangle;
            booleanShapeUsed[5] = true;
        }
    }

    public void Choose(Shape shape)
    {
		ScoreManager.ins.IncNumOfMoves();
        Button btn = shape.GetComponent<Button>();
         btn.interactable = false;
        // StartCoroutine(IENext(shape, btn)); 
         Check(shape, btn);
    }

    void Check(Shape shape, Button btn)
    {
        if (shape.eShape == shapeAnswer)
        {
            print("win!");
            audSrc.PlayOneShot(clipCorrect);
            if (index < sprtObject.Length - 1 && pts < 3)
            {
                index++;
                pts++;
                //GameOn();
                if (pts < 3)
                {
                    Invoke("GameOn", 1f);
                }
                else {
                    Invoke("Done", 1f);
                }
                
            }
            else
            {
                Invoke("Done",0.2f);
                print("game over!");
            }

        }
        else
        {
            audSrc.PlayOneShot(clipWrong);
            print("Wrong");
        }
        StartCoroutine(IEEnableBtns(btn));
    }
    void Done()
    {
        ActivityDone.instance.Done();
    }

    IEnumerator IEEnableBtns(Button recentBtn)
    {
        yield return new WaitForSeconds(1f);
        recentBtn.interactable = true;
        UIBtn[0].enabled = true;
        UIBtn[1].enabled = true;
        UIBtn[2].enabled = true;
        UIBtn[3].enabled = true;

    }

    IEnumerator IENext(Shape shape, Button btn)
    {
        yield return new WaitForSeconds(1f);
         index++;
         GameOn();
    }

    public void DisableButton(Button btn)
    {
        btn.enabled = false;
    }
   

}

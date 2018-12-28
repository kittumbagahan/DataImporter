using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgotPassword : MonoBehaviour {

    public void ContactUs()
    {
        MessageBox.ins.ShowOk("Please email us at palabaydev@gmail.com", MessageBox.MsgIcon.msgInformation, null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Exp_2.score = StaticMethods.CheckCorrectness(Exp_2.V_Real, Exp_2.V_User, Exp_2.UV_User, Exp_2.A_Real, Exp_2.A_User, Exp_2.UA_User, Exp_2.B_Real, Exp_2.B_User, Exp_2.UB_User, Exp_2.C_Real, Exp_2.C_User, Exp_2.UC_User);

        GetComponent<Text>().text = "您的得分为：" + Exp_2.score;
    }

    
}

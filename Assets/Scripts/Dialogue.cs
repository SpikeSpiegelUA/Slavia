using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue : MonoBehaviour
{
    //Some code to write dialogs and dialoguer name in inspector
    public string personTag;
    public string personName;
    [TextArea(3,10)]
    public string[] sentences;
}

using UnityEngine;
using System.Collections;

public class ConfigurationManager : MonoBehaviour {
    public static int size;

    public static Color defaultColor;
    public static Color currColor;


    // Use this for initialization
    void Start () {
        defaultColor = Color.black;
        currColor = defaultColor;
	}
}

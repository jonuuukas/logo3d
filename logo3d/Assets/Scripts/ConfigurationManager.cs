using UnityEngine;

public class ConfigurationManager : MonoBehaviour {
    public static int size;

    public static Color defaultColor;
    public static Color currColor;


    // Use this for initialization
    void Start () {
        size = 300;
        defaultColor = Color.black;
        currColor = defaultColor;
	}
}

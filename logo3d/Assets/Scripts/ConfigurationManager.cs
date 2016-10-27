using UnityEngine;

public class ConfigurationManager : MonoBehaviour {

    public static int size = 300;

    public static Color defaultColor = Color.black;
    public static Color currColor;

    public enum CameraMode { Ortographic, Perspective, Action };
    public enum PaintMode { Paint, Erase, Hover };

    public static CameraMode camMode = CameraMode.Ortographic;
    public static PaintMode paintMode = PaintMode.Paint;

    // Use this for initialization
    void Start() {
        currColor = defaultColor;

    }

}

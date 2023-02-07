using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class addShader : MonoBehaviour
{
   void Start()
    {
        gameObject.GetComponent<Image>().material.shader = Shader.Find("Custom/CrosshairShader");
        gameObject.GetComponent<Image>().material.SetColor("_LinesColor", Color.red);
    }
}

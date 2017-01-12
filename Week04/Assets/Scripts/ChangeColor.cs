// Quick and dirty approach to changing a material's color.
using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    // A GameObject's Renderer can be used to access things such as Materials: https://docs.unity3d.com/ScriptReference/Renderer.html
    private Renderer render;

    // Public Color will create a nice colorpicker in the Inspector
    public Color fromColor, toColor;

    // Public variables can be hidden in the Inspector using [HideInInspector] to reduce clutter.
    // This bool will be used from another script to change the color of this GameObject.
    [HideInInspector]
    public bool change;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        // Reset color
        if (!change)
            render.material.color = fromColor;
    }

    // LateUpdate is called after all Updates have been completed. The mechanism changing the change bool will be done in another script's Update
    void LateUpdate()
    {
        // Change color
        if (change)
        {
            render.material.color = toColor;

            // Make it so that the material can be switched back automatically
            change = false;
        }

    }
}

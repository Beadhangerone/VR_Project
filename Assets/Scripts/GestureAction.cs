using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct MaterialNamed
{
    public Material Material;
    public String Name;
}
public class GestureAction : MonoBehaviour
{
    public List<String> gestureNames = new List<string>();

    private List<String> gestureOrder = new List<string>();

    public int lenght = 4;
    
    public List<MaterialNamed> MaterialNameds;
    
    private Dictionary<String, Material> materials = new Dictionary<string, Material>();

    public GameObject display;

    private Renderer displayRender;
    
    // Start is called before the first frame update
    void Start()
    {
        displayRender = display.GetComponent<Renderer>();
        foreach (var mat in MaterialNameds)
        {
            materials.Add(mat.Name, mat.Material);
        }
        displayRender.material = materials[gestureNames.First()];

        for (int i = 0; i < lenght; i++)
        {
            gestureOrder.Add(gestureNames[Random.Range(0, gestureNames.Count)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckGesture(String name)
    {
        if (gestureOrder.Count > 0)
        {
            if (name.Equals(gestureOrder.First()))
            {
                gestureOrder.RemoveAt(0);
                if (gestureOrder.Count > 0)
                {
                    displayRender.material = materials[gestureOrder.First()];
                }
                else
                {
                    displayRender.material = materials["done"];
                }
                
            }
        }
        else
        {
            displayRender.material = materials["done"];
        }
        
        Debug.LogError(name);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Transform bicycle;
    //public string name;
    public Transform bicyclePart;



    void Start()
    {
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = bicyclePart.name.Split(char.Parse(" "))[0];
        //this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = name.Split(char.Parse(" "))[0];
        //bicyclePart = bicycle.transform.Find("BidonRosu");

        /*
        bicyclePart = bicycle.transform.Find(name.Split(char.Parse(" "))[0]);
        
        if (bicyclePart == null)
            bicyclePart=bicycle.GetComponentInChildren<Transform>().Find(name.Split(char.Parse(" "))[0]);

        */
        bicyclePart.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("onnnn!!!");
        bicyclePart.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exiiit!!!");
        bicyclePart.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clickedd!!!");
        // translates bicycle
        bicyclePart.transform.Translate(0f, 1.0f,0f);
        // Detaches the transform from its parent.
        transform.parent = null;

        //.... trying to give the bicycle part to the vr hands ...

    }
}

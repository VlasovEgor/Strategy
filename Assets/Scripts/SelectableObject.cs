using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecectableObject : MonoBehaviour
{
    public GameObject SelectionIndicator;
    public virtual void Start()
    {
        SelectionIndicator.SetActive(false);    
    }
    public virtual void OnHower()
    {
        transform.localScale*=  1.1f;
    }
    public virtual void OnUnHower()
    {
        transform.localScale = transform.localScale/1.1f;
    }
    public virtual void Select()
    {
        SelectionIndicator.SetActive(true);
    }
    public virtual void UnSelect()
    {
        SelectionIndicator.SetActive(false);
    }

    public virtual void WhenClickOnGround(Vector3 point)
    {

    }
}

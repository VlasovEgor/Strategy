using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState
{UnitsSelected,
Frame,
 Other
}


public class Managment : MonoBehaviour
{

    public Camera Camera;
    public SecectableObject Howered;
    public List<SecectableObject> ListOfSelected = new List<SecectableObject>();
    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;
    public SelectionState CurrentSelectionState;

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<SelectableCollider>())
            {
                SecectableObject hitSelectable = hit.collider.GetComponent<SelectableCollider>().SecectableObject;
                if (Howered)
                {
                    if (Howered != hitSelectable)
                    {
                        Howered.OnUnHower();
                        Howered = hitSelectable;
                        Howered.OnHower();
                    }
                }
                else
                {
                    Howered = hitSelectable;
                    Howered.OnHower();
                }

            }
            else
            {
                UnhoverCurrent();
            }
        }
        else
        {
            UnhoverCurrent();
        }



        if (Input.GetMouseButtonUp(0))
        {
            if (Howered)
            {   
                if(Input.GetKey(KeyCode.LeftControl)==false)
                {
                    UnSelectedAll();
                }
                CurrentSelectionState = SelectionState.UnitsSelected;
                Select(Howered);
            }
        }
        if(CurrentSelectionState==SelectionState.UnitsSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit.collider.tag == "Ground")
                {
                    int roreNumber = Mathf.CeilToInt( Mathf.Sqrt(ListOfSelected.Count));
                    for (int i = 0; i < ListOfSelected.Count; i++)
                    {
                        

                        int row = i / roreNumber;
                        int column = i % roreNumber;

                        Vector3 point = hit.point + new Vector3(row, 0, column);


                        ListOfSelected[i].WhenClickOnGround(point);


                    }
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            UnSelectedAll();
        }



        if(Input.GetMouseButtonDown(0))
        {
            
            _frameStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _frameEnd = Input.mousePosition;
            
            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 size = max - min;

            if (size.magnitude>10)
            {
                
                FrameImage.enabled = true;
                FrameImage.rectTransform.anchoredPosition = min;
                FrameImage.rectTransform.sizeDelta = size;

                UnSelectedAll();
                CurrentSelectionState = SelectionState.Frame;
                Rect rect = new Rect(min, size);
                Unit[] allUnits = FindObjectsOfType<Unit>();
                for (int i = 0; i < allUnits.Length; i++)
                {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(allUnits[i].transform.position);
                    if (rect.Contains(screenPosition))
                    {
                        Select(allUnits[i]);
                    }
                }
            }
           
        }

        if(Input.GetMouseButtonUp(0))
        {
            FrameImage.enabled = false;
            if(ListOfSelected.Count>0)
            {
                CurrentSelectionState = SelectionState.UnitsSelected;
            }
            else
            {
                CurrentSelectionState = SelectionState.Other;
            }
        }
    }

    void Select(SecectableObject secectableObject)
    {
        if (ListOfSelected.Contains(secectableObject) == false)
        {
            ListOfSelected.Add(secectableObject);
            secectableObject.Select();
        }
    }
    public void UnSelect(SecectableObject secectableObject)
    {
        if(ListOfSelected.Contains(secectableObject))
        {
            ListOfSelected.Remove(secectableObject);
        }
    }
    void UnSelectedAll()
    {
        for (int i = 0; i < ListOfSelected.Count; i++)
        {
            ListOfSelected[i].UnSelect();
        }
        ListOfSelected.Clear();
        CurrentSelectionState = SelectionState.Other;
    }
    void UnhoverCurrent()
    {
        if (Howered)
        {
            Howered.OnUnHower();
            Howered = null;
        }
    }
}

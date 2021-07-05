using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Text outText;
    private void Update() {
        
        Ray ray;
        RaycastHit2D hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(Input.mousePosition, -Vector2.up);
        //Physics.Raycast(Camera.main.transform.position, Input.mousePosition, out hit , Mathf.Infinity);
        

    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EyeRay : MonoBehaviour
{
    [SerializeField]
    private float rayDisatnce = 1.0f;

    [SerializeField]
    private float rayWidth = 0.01f;

    [SerializeField]
    private LayerMask layersToInclude;

    private LineRenderer lineRenderer;

    private List<EyeInteractable> eyeInteractables = new List<EyeInteractable>();

    private EyeInteractable previousInteractable;

    private float previousEndTime = 0;

    private StreamWriter writer ;

    // Start is called before the first frame update
    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();

        SetupRay();
        
        writer = new StreamWriter("C:\\Users\\students20-21\\Documents\\Jolie\\Exports\\" + gameObject.scene.name + "Full.csv");
        StartCoroutine(WriteCoords());

    }

    IEnumerator WriteCoords()
    {
        yield return new WaitForSeconds(0.0f);
       
    }
    void SetupRay()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;
        lineRenderer.startColor = Color.clear;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z + rayDisatnce));
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 rayCastDirection = transform.TransformDirection(Vector3.forward) * rayDisatnce;

        if (Physics.Raycast(transform.position, rayCastDirection, out hit, Mathf.Infinity, layersToInclude))
        {
            UnSelect();
            var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();

            eyeInteractables.Add(eyeInteractable);
            eyeInteractable.IsHovered = true;

            //increment framesObserved
            eyeInteractable.framesObserved += 1;

            //log raw data
            writer.WriteLine(eyeInteractable.name.ToString() + "," + transform.position.ToString());

            //if interactable switched
            if (eyeInteractable != previousInteractable & previousInteractable != null)
            {
                //Debug.LogError("switch");
                previousInteractable.glanceLengths.Add(Time.time - previousEndTime);
                previousInteractable.numberGlances += 1;

                previousEndTime = Time.time;
            }
            previousInteractable = eyeInteractable;
        }
        else
        {
            UnSelect(true);
        }
    }

    //reset 
    void UnSelect(bool clear = false)
    {
        foreach(var interactable in eyeInteractables)
        {
            interactable.IsHovered = false;
        }

        if (clear)
        {
            eyeInteractables.Clear();
        }
    }

    void OnApplicationQuit()
    {
        writer.Close();
    }
}

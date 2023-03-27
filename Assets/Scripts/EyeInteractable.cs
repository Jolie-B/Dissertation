using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System.IO;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EyeInteractable : MonoBehaviour
{

    public bool IsHovered { get; set; }

    public int numberGlances { get; set; }

    public int framesObserved { get; set; }

    public List<float> glanceLengths = new List<float>();

    [SerializeField]
    private UnityEvent<GameObject> OnObjectHover;

    private MeshRenderer meshRenderer;



    // Start is called before the first frame update
    void Start() {
        
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (IsHovered)
        {
            OnObjectHover?.Invoke(gameObject);
        }

    }


    void OnApplicationQuit()
    {
       
        StreamWriter writer = new StreamWriter("C:\\Users\\students20-21\\Documents\\Jolie\\Exports\\" + gameObject.scene.name + gameObject.name + ".txt");
        writer.WriteLine("the followind data was collected for " + gameObject.scene.name + gameObject.name);

        writer.WriteLine("Number of glances: " + numberGlances);

        writer.WriteLine("Frames observed: " + framesObserved);

        writer.WriteLine("Glance Lengths:");

        foreach (var elt in glanceLengths)
        {
            writer.WriteLine(elt); ;
        }
       
        writer.WriteLine("- - - - - DONE - - - - -\n");
        writer.Close();

       
        Debug.Log("Application ending after " + Time.time + " seconds");
   
}


}

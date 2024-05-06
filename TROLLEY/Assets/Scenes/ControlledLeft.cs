using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledLeft : MonoBehaviour
{
    // Start is called before the first frame update
    bool triggerEnabled = true;
    public GameObject ref;
    void Start()
    {
        TrolleyController controller = ref.GetComponent<TrolleyController>();
    }
    void onTriggerEnter(Collider other){
        if(other.CompareTag("Trolley")){
            triggerEnabled = true;
        }
    }
    void onTriggerExit(Collider other){
        if(other.CompareTag("Trolley"))
            triggerEnabled = false;
    }
    // Update is called once per frame
    void Update(Collider other)
    {
        if(triggerEnabled && Input.getKeyDown(keyCode.left)){
            triggerEnabled = false;
            turnTrolley(controller);
        }
    }
    void turnTrolley(TrolleyController other){
        other.RotateTrolley(-other.rotationAmount);
    }
}

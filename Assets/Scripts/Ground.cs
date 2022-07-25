using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    private bool isActive = false;
    public GroundManager groundManager;
    // Start is called before the first frame update
    [SerializeField] private MeshRenderer mr;
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material deactivatedMaterial;
    void Start()
    {
        //mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate() {
        isActive = true;
        mr.material = activatedMaterial;
    }

    public void deactivate() {
        isActive = false;
        mr.material = deactivatedMaterial;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "Body" && isActive) {
            deactivate();
            groundManager.changeActiveGround();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject buildObj;
    private Vector3 position;

    public LayerMask layerMask;
    private Vector3 offset;
    private bool canBuild = true;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            Vector3 InstantiatedCubsPos = Vector3.zero;
            if (hit.transform.CompareTag("Ground"))
            {
                Vector3 hitCubePos = hit.point;
                hitCubePos.y += hit.transform.GetComponent<BoxCollider>().size.y / 2f;
                this.gameObject.transform.position = hitCubePos;
                InstantiatedCubsPos = hitCubePos;
            }
            else if(hit.transform.CompareTag("Wall"))
            {
                Vector3 SurfaceVec = hit.normal;
                Vector3 hitCubePos = hit.transform.position;
                InstantiatedCubsPos = SurfaceVec + hitCubePos;
                this.gameObject.transform.position = InstantiatedCubsPos;
                GetComponent<MeshRenderer>().material.color = Color.gray;
                canBuild = true;
            }


            if (Input.GetMouseButtonDown(0) && canBuild)
            {
                Destroy(this.gameObject);
                Instantiate(buildObj, InstantiatedCubsPos, Quaternion.identity);
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag("Wall"))
            {
                canBuild = false;
                return;
            }
        }
        canBuild = true;
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            IsBuild = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponent<MeshRenderer>().material.color = Color.gray;
        IsBuild = true;
    }
    */
}

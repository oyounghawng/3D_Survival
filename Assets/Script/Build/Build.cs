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
    private bool canBuild = true;
    Vector3 hitCubePos;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            /*
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                canBuild = false;
                GetComponent<MeshRenderer>().material.color = Color.red;
                return;
            }
            */

            Vector3 InstantiatedCubsPos = Vector3.zero;
            if (hit.transform.CompareTag("Ground"))
            {
                float height = GetComponent<BoxCollider>().size.y / 2f;
                Vector3 SurfaceVec = hit.normal * height; // 보정치 필요
                hitCubePos = hit.point + SurfaceVec;
                this.gameObject.transform.position = hitCubePos;
                InstantiatedCubsPos = hitCubePos;
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                Vector3 SurfaceVec = hit.normal;
                hitCubePos = hit.transform.position;
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

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Wall"))
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            canBuild = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponent<MeshRenderer>().material.color = Color.gray;
        canBuild = true;
    }

}

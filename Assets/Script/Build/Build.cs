using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Build : MonoBehaviour
{
    public GameObject buildObj;

    public LayerMask layerMask;
    private bool canBuild = true;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            Vector3 SurfaceVec = hit.normal; // 보정치 필요
            Vector3 hitCubePos = Vector3.zero;
            Vector3 InstantiatedCubsPos = Vector3.zero;
            /*
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
                canBuild = false;
                float height = GetComponent<BoxCollider>().size.y / 2f;
                SurfaceVec *= height;
                hitCubePos = hit.point + SurfaceVec;
                InstantiatedCubsPos = hitCubePos;
                this.gameObject.transform.position = InstantiatedCubsPos;
                return;
            }
            */
            if (hit.transform.CompareTag("Ground"))
            {
                float height = GetComponent<BoxCollider>().size.y / 2f;
                SurfaceVec *= height;
                hitCubePos = hit.point + SurfaceVec;
                InstantiatedCubsPos = hitCubePos;
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                canBuild = true;
                GetComponent<MeshRenderer>().material.color = Color.gray;
                SurfaceVec = hit.normal;
                hitCubePos = hit.transform.position;
                InstantiatedCubsPos = SurfaceVec + hitCubePos;
                this.gameObject.transform.position = InstantiatedCubsPos;
            }
            this.gameObject.transform.position = InstantiatedCubsPos;


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

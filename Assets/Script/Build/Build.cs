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
    private bool inWallTrigger = false;

    Color basicColor;
    private void Start()
    {
        basicColor = GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 20f, layerMask))
        {
            Vector3 SurfaceVec = hit.normal; // 보정치 필요
            Vector3 hitCubePos = Vector3.zero;
            Vector3 InstantiatedCubsPos = Vector3.zero;

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

            if(!inWallTrigger)
            {
                GetComponent<MeshRenderer>().material.color = basicColor;
                canBuild = true;
            }

            if (hit.transform.CompareTag("Ground"))
            {
                float height = GetComponent<BoxCollider>().size.y / 2f;
                SurfaceVec *= height;
                hitCubePos = hit.point + SurfaceVec;
                InstantiatedCubsPos = hitCubePos;
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                GetComponent<MeshRenderer>().material.color = basicColor;
                canBuild = true;
                SurfaceVec = hit.normal;
                hitCubePos = hit.transform.position;
                InstantiatedCubsPos = SurfaceVec + hitCubePos;
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
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Resources"))
        {
            inWallTrigger = true;
            GetComponent<MeshRenderer>().material.color = Color.red;
            canBuild = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inWallTrigger = false;
        GetComponent<MeshRenderer>().material.color = basicColor;
        canBuild = true;
    }
}
    
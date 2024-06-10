using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float checkRate = 0.5f;
    private float prevCheckTime;
    public float checkDistance = 10f;
    public LayerMask mask;

    public GameObject interactGO;
    private IInteractable interactable;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        if(Time.time -prevCheckTime > checkRate) 
        {
            prevCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,checkDistance,mask))
            {
                if(hit.collider.gameObject != interactGO)
                {
                    interactGO = hit.collider.gameObject;
                    interactable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText(interactable.GetData());
                }
            }
            else
            {
                interactGO = null;
                interactable = null;
                (Managers.UI.SceneUI as UI_HUD).promptTextBG.SetActive(false);
            }
        }
    }
    private void SetPromptText(string text)
    {
        string[] comp = text.Split(" | ");
        (Managers.UI.SceneUI as UI_HUD).promptTextBG.SetActive(true);
        (Managers.UI.SceneUI as UI_HUD).promptText.text = string.Format
            ($"{comp[(int)PromptFormat.Name]}\n\n" +
             $"{comp[(int)PromptFormat.Description]}");
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && interactable != null)
        {
            interactable.OnInteract();
            interactGO = null;
            interactable = null;
            (Managers.UI.SceneUI as UI_HUD).promptTextBG.SetActive(false);
        }
    }
}
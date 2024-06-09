using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float checkRate = 0.5f;
    private float prevCheckTime;
    public float checkDistance;
    public LayerMask mask;

    public GameObject interactGO;
    private IInteractable interactable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        //promptText = Util.GetOrAddComponent<TextMeshProUGUI>(Managers.UI.FindPopup<UI_PromptText>().gameObject);
    }
    private void Start()
    {
//        promptText = Managers.UI.FindPopup<UI_PromptText>().gameObject.GetComponent<TextMeshProUGUI>();
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
                    SetPromptText();
                }
            }
            else
            {
                interactGO = null;
                interactable = null;
//                promptText.gameObject.SetActive(false);
            }
        }
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = interactable.GetData();
    }


}
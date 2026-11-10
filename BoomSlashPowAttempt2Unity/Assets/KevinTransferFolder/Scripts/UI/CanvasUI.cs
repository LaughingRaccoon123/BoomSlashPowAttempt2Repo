using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] public GameObject deadScreen;
    void Start()
    {
        deadScreen.SetActive(false);
    }

    void Update()
    {
        
    }

    public void deadScreenOverlay(bool onOff)
    {
        deadScreen.SetActive(onOff);
    }
}

using UnityEngine;

public class Vereda : MonoBehaviour
{
    public string playerTag = "Player";
    public float giroPorSeg;
    public float restGiro; // valor que se le suma al giro cuando sale para restaurar la estabilidad

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag) other.SendMessage("SumaGiro", restGiro);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == playerTag) other.SendMessage("SumaGiro", giroPorSeg * T.GetDT());
    }
}
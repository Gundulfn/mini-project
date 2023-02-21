using UnityEngine;
using TMPro;

public class Gate : Interactable
{
    [SerializeField]
    private int gateValue = 1;
    private TextMeshPro gateValueText;

    [SerializeField]
    private Gate connectedGate;

    private void Start()
    {
        gateValueText = GetComponentInChildren<TextMeshPro>();

        if (gateValue > 0)
        {
            gateValueText.SetText("+" + gateValue.ToString());
        }
        else
        {
            gateValueText.SetText(gateValue.ToString());
        }
    }

    public override void Interact(object obj)
    {
        if (obj is PlayerCollider)
        {
            if (gateValue > 0)
            {
                GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }

            if(connectedGate)
            {
                connectedGate.GetComponent<Renderer>().enabled = false;
                Destroy(connectedGate);
            }
        }
    }

    public int GetGateValue()
    {
        return gateValue;
    }
}
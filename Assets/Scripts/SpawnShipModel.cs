using UnityEngine;

public class SpawnShipModel : MonoBehaviour
{
    public Vector3 targetPosition;
    public Quaternion targetRotation;


    private GetStats selectionStats;
    private GameObject shipModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get references to scripts
        selectionStats = GetComponent<GetStats>();

        
    }

    void Update()
    {
        shipModel.transform.localPosition = targetPosition;
        shipModel.transform.localRotation = targetRotation;
    }
}

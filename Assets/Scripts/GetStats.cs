using UnityEngine;

//[ExecuteInEditMode]
public class GetStats : MonoBehaviour
{
    [Header("Player Selection")]
    public CharacterData currentCharacter;
    public ShipData currentShip;
    public TrackData currentTrack;


    [Header("Character Stats")]
    public float recoverySpeed;
    
    [Header("Ship Status")]
    public float speed;
    public float accel;
    public float weight;
    public float handling;
    public float maxCharge;
    public float rechargeRate;
    
    [Header("Bonuses")]
    public bool matchingBoost = false;
    public bool specialtyBoost = false;

    [Header("Models")]
    public GameObject characterModelPrefab;
    public GameObject shipModelPrefab;
    public Vector3 shipModelScaleFactor;


    // reads character, ship, and track classes for ship stats (should be from 0 - 10)
    // adds offsets to real usable values
    void GetSelection()
    {
        // Character
        recoverySpeed = currentCharacter.recoverySpeed;

        // Ship
        speed        = currentShip.speed;
        accel        = currentShip.accel;
        weight       = currentShip.weight;
        handling     = currentShip.handling;
        maxCharge    = currentShip.maxCharge;
        rechargeRate = currentShip.rechargeRate;

        // models
        characterModelPrefab = currentCharacter.characterModelPrefab;
        shipModelPrefab      = currentShip.shipModelPrefab;

        shipModelScaleFactor = currentShip.shipModelScaleFactor;

        // matching bonus
        if (currentCharacter.specialty == currentShip.specialty) matchingBoost = true;

        // specialty bonus
        if (currentShip.specialty == currentTrack.type) specialtyBoost = true;
    }

    void Awake()
    {
        GetSelection();
        Debug.Log("Stats read and set from player selection");
    }
}

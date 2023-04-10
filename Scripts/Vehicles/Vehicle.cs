using UnityEngine;
using Mirror;
using System.Collections;

[RequireComponent(typeof(CarControl))]
public class Vehicle : NetworkBehaviour
{
    public Seat[] Seats;
    public string VehicleName;
    [SyncVar]
    public string VehicleID;
    [HideInInspector]
    public bool incontrol;
    [SyncVar(hook = "OnSeatDataChanged")]
    public string SeatsData;
    [SyncVar]
    private Vector3 positionSync;
    [SyncVar]
    private Quaternion rotationSync;
    public bool hasDriver;

    void Awake()
    {
        if (Seats.Length <= 0)
        {
            var seat = this.GetComponentsInChildren(typeof(Seat));
            Seats = new Seat[seat.Length];
            for (int i = 0; i < seat.Length; i++)
            {
                Seats[i] = seat[i].GetComponent<Seat>();
            }
        }
    }

    public override void OnStartClient()
    {
        if (isServer)
        {
            VehicleID = netId.ToString();
        }
        OnSeatDataChanged(SeatsData);
        base.OnStartClient();
    }

    void OnSeatDataChanged(string seatsdata)
    {
        SeatsData = seatsdata;
        string[] passengerData = seatsdata.Split(","[0]);
        if (passengerData.Length >= Seats.Length)
        {
            for (int i = 0; i < Seats.Length; i++)
            {
                int.TryParse(passengerData[i], out Seats[i].PassengerID);
            }
        }
    }

    void GenSeatsData()
    {
        string seatdata = "";
        for (int i = 0; i < Seats.Length; i++)
        {
            if (Seats[i].PassengerID != -1)
            {
                seatdata += Seats[i].PassengerID + ",";
            }
            else
            {
                seatdata += "-1,";
            }
        }
        SeatsData = seatdata;
    }

void UpdatePassengerOnSeats()
{
    hasDriver = false;
    for (int i = 0; i < Seats.Length; i++)
    {
        if (Seats[i].PassengerID != -1)
        {
            uint passengerid = (uint)(Seats[i].PassengerID < 0 ? 0 : Seats[i].PassengerID);
            GameObject obj = NetworkIdentity.spawned.TryGetValue(passengerid, out NetworkIdentity identity) ? identity.gameObject : null;
            if (obj)
            {
                CharacterDriver driver = obj.GetComponent<CharacterDriver>();
                if (driver)
                {
                    driver.transform.position = Seats[i].transform.position;
                    driver.transform.parent = Seats[i].transform;
                    driver.CurrentVehicle = this;
                    driver.character.controller.enabled = false;
                    driver.DrivingSeat = Seats[i];
                    hasDriver = true;
                    if (driver.character.IsAlive == false)
                    {
                        Seats[i].PassengerID = -1;
                    }
                }
            }
        }
        else
        {
            Seats[i].CleanSeat();
        }
    }

    if (isServer)
    {
        GenSeatsData();
    }
}

public void GetOutTheVehicle(CharacterDriver driver)
{
    for (int i = 0; i < Seats.Length; i++)
    {
        if (Seats[i].PassengerID == driver.netId)
        {
            Seats[i].PassengerID = -1;
            driver.transform.parent = null;
            driver.character.controller.enabled = true;
            driver.CurrentVehicle = null;
            driver.DrivingSeat = null;
            return;
        }
    }
}

public int FindOpenSeatID()
{
    for (int i = 0; i < Seats.Length; i++)
    {
        if (Seats[i].PassengerID == -1)
        {
            return i;
        }
    }
    return -1;
}

public virtual void Drive(Vector2 input, bool brake)
{

}

public virtual void Update()
{
    UpdatePassengerOnSeats();

    if (isServer)
    {
        positionSync = this.transform.position;
        rotationSync = this.transform.rotation;
    }

    this.transform.position = Vector3.Lerp(this.transform.position, positionSync, 0.5f);
    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotationSync, 0.5f);
    UpdateDriver();
}
}
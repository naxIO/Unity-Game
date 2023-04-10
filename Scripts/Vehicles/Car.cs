using UnityEngine;
using System.Collections;
using Mirror;

public class Car : Vehicle
{
    public CarControl car;
    private Vector2 inputTemp;
    private bool brakeTemp;

    public void Start()

    {
        Audiosource = this.GetComponent<AudioSource>();
        car = this.gameObject.GetComponent<CarControl>();
        base.Start();
    }

    public override void Drive(Vector2 input, bool brake)
    {
        driving(new Vector3(input.x, input.y, 0), brake);
        base.Drive(input, brake);
    }


    public void driving(Vector3 input, bool brake)
    {
        inputTemp = input;
        brakeTemp = brake;
        incontrol = true;

        if (input.x == 0 && input.y == 0 && !brake)
        {
            incontrol = false;
        }
    }

    public override void Update()

    {
        if (isServer)
        {

            if (incontrol)
            {
                if (car)
                {
                    car.Controller(new Vector2(inputTemp.x, inputTemp.y), brakeTemp);

                }
            }

            if (car)
                car.StartEngine(hasDriver);
        }
        base.Update();
    }
}

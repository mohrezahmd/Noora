using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHolder : MonoBehaviour
{
    public PowerUp_SO powerUp_SO;
    float activeTime;
    int pCounter = 0;

    enum powerUpState { ready, active, disactive }

    powerUpState state = powerUpState.disactive;

    void Update()
    {
        switch (state)
        {
            case powerUpState.ready:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    powerUp_SO.Activate(gameObject);
                    state = powerUpState.active;
                    activeTime = powerUp_SO.activeTime;
                }
                break;

            case powerUpState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = powerUpState.disactive;
                }
                break;

            case powerUpState.disactive:
                Debug.Log("power up disactive: ");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    powerUp_SO.Activate(gameObject);
                    state = powerUpState.active;
                    activeTime = powerUp_SO.activeTime;
                }
                break;
        }
    }
}

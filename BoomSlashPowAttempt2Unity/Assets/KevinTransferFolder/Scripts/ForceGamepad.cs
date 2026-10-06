using UnityEngine;
using UnityEngine.InputSystem;

public class ForceGamepad : MonoBehaviour
{
    void Awake()
    {
        // Force Unity to treat your unrecognized Microsoft device as a Gamepad layout
        InputSystem.RegisterLayoutOverride(
            "{ \"name\": \"XboxGamepadOverride\", \"extend\": \"Gamepad\", \"deviceMatchers\": [ { \"vendorId\": 1118 } ] }"
        );

        Debug.Log("Xbox Gamepad layout override registered!");
    }
}

using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.JumpPadLaunch(jumpPower);
        }
    }
}
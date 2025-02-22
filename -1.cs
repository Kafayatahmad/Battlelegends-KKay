// Battle Legends: Powered by K Kay - Initial Game Code

using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Update()
    {
        if (!isLocalPlayer)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10f;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2f);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}

// Simple Network Manager
public class GameNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Player connected: " + conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("Player disconnected: " + conn);
        base.OnServerDisconnect(conn);
    }
}

// Voice Chat Placeholder (To be added in updates)
// Premium Skins and Features will also be integrated in future versions.

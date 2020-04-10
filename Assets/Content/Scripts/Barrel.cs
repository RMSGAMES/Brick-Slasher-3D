using UnityEngine;

public class Barrel : MonoBehaviour
{
    [HideInInspector] public GameController gameController;
    [HideInInspector] public int id = 0;
    [HideInInspector] public bool isDestroyed = false;

    public void Initialization(GameController t_gameController, int t_id)
    {
        gameController = t_gameController;
        id = t_id;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.transform.CompareTag("Ground"))
        {
            gameController.TargetDestroyed(id);
            isDestroyed = true;
        }
    }
}
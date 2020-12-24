using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;
public class DrawController : MonoBehaviour
{
    public Transform GridHolder;
    
    [SerializeField] private LayerMask GridLayer;
    [SerializeField] private float MovementSpeed;
    
    private List<Block> GridBlocks;

    public bool GameStart;
    public bool isClearGrid;


    // Start is called before the first frame update
    public void Start()
    {
        InitGrid();
        isClearGrid = false;
    }

    // Update is called once per frame
    void Update()
    {
     
        GridHolder.position += GridHolder.forward * MovementSpeed;
        
        var inputState = InputManager.Instance.InputState;

        if (inputState == InputState.FirstTouch)
        {
            ClearGrid();
        }

        //if (isClearGrid == true)
        //{

        //    ClearGrid();
        //    isClearGrid = false;
        //}

        if(inputState == InputState.FirstTouch ||inputState == InputState.Hold)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100f, GridLayer)) {
                if (hit.transform.GetComponent<Block>() != null)
                {
                    if (!hit.transform.GetComponent<Block>().IsAlive)
                    {
                        SpawnBlock(hit.transform.GetComponent<Block>());
                    }
                }
            }
        }

            /*
            if (inputState == InputState.Released)
            {

                var isBindedBlock = true;
                foreach (Block gridBlock in GridBlocks)
                {
                    var rayForward = gridBlock.transform.forward;
                    var rayPosition = gridBlock.transform.position;

                    Debug.DrawRay(rayPosition, rayForward, Color.red);

                    if (!gridBlock.IsAlive)
                    {


                        if (!Physics.Raycast(rayPosition, rayForward, out var hitFromBlock, 10))
                        {
                            Debug.DrawRay(rayPosition, rayForward, Color.red);
                            Debug.Log("TRUE");
                            isBindedBlock = false;
                        }
                    }
                    else
                    {
                        if (Physics.Raycast(rayPosition, rayForward, out var hitFromBlock, 10))
                        {
                            Debug.DrawRay(rayPosition, rayForward, Color.red);

                            Debug.Log("FALSE");
                            isBindedBlock = false;
                        }
                    }
                }

                if (!isBindedBlock)
                {
                    ClearGrid();
                }
                else
                {
                    // move faster to other cube and patlama
                }
            }
            */
        

    }


    private void SpawnBlock(Block block)
    {
        block.MakeBlock();
    }

    private void InitGrid()
    {
        GridBlocks = new List<Block>();
        for (int i = 0; i < GridHolder.childCount; i++)
        {
            GridBlocks.Add(GridHolder.GetChild(i).GetComponent<Block>());
        }
    }
    
    public void ClearGrid()
    {
        for (int i = 0; i < GridHolder.childCount; i++)
        {
            GridBlocks[i].KillBlock();
        }
    }
}

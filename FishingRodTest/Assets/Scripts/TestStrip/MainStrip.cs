using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStrip : MonoBehaviour {

    

    public Vector3 StartPos;
    public Vector3 EndPos;
    public float StripLength = 0;

    private int MaxNodeNum = 11;

    private List<StripNode> nodes;

	// Use this for initialization
	void Start () {
        this.init();
        //this.changeStatus(new Vector3(10,0,0));
    }

    void init()
    {
        StartPos = Vector3.zero;
        EndPos = Vector3.up * 10;
        Vector3 diffvector = EndPos - StartPos;
        Vector3 diff = diffvector / MaxNodeNum;
        StripLength = diffvector.magnitude;
        //Debug.Log(StripLength);
        Vector3 lastpos = StartPos;
        nodes = new List<StripNode>();
        float l = (EndPos - StartPos).magnitude;
        float diffl = l / (float)(MaxNodeNum - 1);
        for (int i = 0; i < MaxNodeNum; i++)//分10段，有11个节点
        {
            StripNode tempnode = new StripNode();
            tempnode.gameObject = StripUtils.CreatStripNode(i.ToString());
            tempnode.Pos = lastpos;
            lastpos += diff;
            tempnode.Width = StripUtils.GetWidth(i *  diffl, l);
            nodes.Add(tempnode);
        }
    }

    //private Vector3 GetRealPos(Vector3 oriPos,Vector3 targetPos)
    //{
    //}

    private bool changeStatus(Vector3 targetPos)
    {
        int length = nodes.Count;
        StripNode lastnode = nodes[length - 1];
        Vector3 endnodediff = targetPos - lastnode.OriPos;

        for (int i = 0; i < length; i++)
        {
            nodes[i].Pos = nodes[i].OriPos + ((float)i / length) * endnodediff * nodes[i].Width;
        }
        return true;
    }

    
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            if (Input.GetMouseButton(0))
            {
 
                Vector3 curpos = Input.mousePosition;
                Vector3 spacePos = this.GetForkWorldPos(curpos);//Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 recurrectPos = new Vector3(spacePos.x, spacePos.y, 0);
                Vector3 currectPos = reCurrectPos(recurrectPos);
                this.changeStatus(currectPos);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            resetStrip();
        }
    }

    private void resetStrip()
    {
        foreach (var item in nodes)
        {
            item.SpringBack();
        }
    }

    private Vector3 reCurrectPos(Vector3 pos)
    {
        var deltapos = pos - this.StartPos;
        var malPos = deltapos.normalized;
        return (this.StartPos + malPos) * this.StripLength;
    }

    private Vector3 GetForkWorldPos(Vector3 screenPos)
    {
        return screenPos - new Vector3(Screen.width, Screen.height, 0) / 2f;
    }
    
}


public class StripNode
{
	public GameObject gameObject;
    [HideInInspector]
	public float Width;
	public Vector3 OriPos
	{
		get { return this.oriPos; }
	}
	public Vector3 Pos
	{
		set
		{
			this.pos = value;
			if (!isInit)
			{
				oriPos = this.pos;
				this.isInit = true;
			}
			if (this.gameObject)
			{
				this.gameObject.transform.position = this.pos;
			}
		}
	}
	private bool isInit = false;
	private Vector3 pos;
	private Vector3 oriPos;
	private float backDelta = 0.01f;
	private float coroutineBreakTime = 2f;

	public void SpringBack()
	{
		CoroutineManager.Instance.AddCoroutine(BackToOrigin());
	}

	private IEnumerator BackToOrigin()
	{
		float curtime = 0.0f;
		float springinput = 0.0f;
		Vector3 tmpPos = this.pos;
		//while (Vector3.Distance(this.oriPos,this.pos) >= backDelta)
		while (springinput < 1 && curtime < coroutineBreakTime)
		{
			curtime += Time.deltaTime;
			float w = StripUtils.getInterpolation(springinput);
			this.Pos = StripUtils.mylerp(tmpPos, this.oriPos, w);
			springinput += 0.009f;
			yield return new WaitForEndOfFrame();
		}
		this.pos = this.oriPos;
	}
}
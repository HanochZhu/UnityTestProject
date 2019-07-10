using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class BaseScriptPIpeline : RenderPipelineAsset
{
    public Color screenColor = Color.yellow;
#if UNITY_EDITOR
    [UnityEditor.MenuItem("tool/creatlwrptestasset")]
    static void CreatLwrpAsset()
    {
        var scriptable = ScriptableObject.CreateInstance<BaseScriptPIpeline>();
        UnityEditor.AssetDatabase.CreateAsset(scriptable,"Assets/SRPAssets/lwrptest.asset");
    }

#endif
    protected override IRenderPipeline InternalCreatePipeline()
    {
        Debug.Log("create an instance");
        return new BasicPipeInstance(screenColor);//create a srp instance
    }
}

internal class BasicPipeInstance : RenderPipeline
{
    private Color screenColor;

    public BasicPipeInstance(Color screenColor)
    {
        this.screenColor = screenColor;
    }

    public override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
    {
        //
        //culling
        base.Render(renderContext, cameras);
        //core 核心 1设置渲染操作，2执行渲染操作，3上传渲染操作
        var cmd = new CommandBuffer();
        cmd.ClearRenderTarget(true, true, screenColor);//1
        renderContext.ExecuteCommandBuffer(cmd);//2
        cmd.Release();
        renderContext.Submit();//3
    }
}
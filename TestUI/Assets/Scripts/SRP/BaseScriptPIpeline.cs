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
    private bool stereoEnable = false;

    public BasicPipeInstance(Color screenColor)
    {
        this.screenColor = screenColor;
    }

    public override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
    {


        //example
        base.Render(renderContext, cameras);
        //core 核心 1设置渲染操作，2执行渲染操作，3上传渲染操作
        //var cmd = new CommandBuffer();

        //culling
        ScriptableCullingParameters cullingParams;
        Debug.Log(cameras[0].name);
        if (!CullResults.GetCullingParameters(Camera.main, stereoEnable,out cullingParams))
        {
            return;
        }

        cullingParams.isOrthographic = true;
        CullResults cullResults = new CullResults();
        CullResults.Cull(ref cullingParams, renderContext, ref cullResults);
        
        List<VisibleLight> lights = cullResults.visibleLights;
        foreach (var item in lights)
        {
            if (item.lightType == LightType.Directional)//设置直射光 
            {
                Vector4 dir = -item.localToWorld.GetColumn(2);
                Shader.SetGlobalVector("LightColor0", item.finalColor);
                Shader.SetGlobalVector("WorldSpaceLightPos0", item.finalColor);
                break;
            }
        }
        //end culling

        //filter
        var opaqueRange = new FilterRenderersSettings();
        opaqueRange.renderQueueRange = new RenderQueueRange()
        {
            min = 0,
            max = (int)UnityEngine.Rendering.RenderQueue.GeometryLast,
        };

        opaqueRange.layerMask = ~0;

        //end filter

        //draw
        var drs = new DrawRendererSettings(Camera.main, new ShaderPassName("Opaque"));
        drs.flags = DrawRendererFlags.EnableInstancing;
        drs.rendererConfiguration = RendererConfiguration.PerObjectLightProbe | RendererConfiguration.PerObjectLightmaps;
        drs.sorting.flags = SortFlags.CommonOpaque;
        //draw!!!
        renderContext.DrawRenderers(cullResults.visibleRenderers, ref drs, opaqueRange);

        //end draw
        //cmd.DrawRenderer(renderContext, new Material());
        //cmd.ClearRenderTarget(true, true, screenColor);//1
        //renderContext.ExecuteCommandBuffer(cmd);//2
        //cmd.Release();
        renderContext.Submit();//3
    }
}
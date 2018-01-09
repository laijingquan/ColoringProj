using UnityEngine;
using System.Collections;

//普通的MeshRenderer的材质球合并：
//1.合并所有材质球所携带的贴图，新建一个材质球，并把合并好的贴图赋予新的材质球。
//2.记录下每个被合并的贴图所处于新贴图的Rect，用一个Rect[] 数组存下来。
//3.合并网格，并把需要合并的各个网格的uv，根据第2步得到的Rect[] 刷一遍。
//4.把新的材质球赋予合并好的网格，此时就只占有1个drawcall了

//    下面我们将讨论SkinnedMeshRenderer的合并。
//SkinnedMeshRenderer比MeshRenderer稍微麻烦一点，因为SkinnedMeshRenderer要处理bones。
//以下是步骤：
//1.合并所有材质球所携带的贴图，新建一个材质球，并把合并好的贴图赋予新的材质球。
//2.记录下每个被合并的贴图所处于新贴图的Rect，用一个Rect[] 数组存下来。
//3.记录下需要合并的SkinnedMeshRenderer的bones。
//4.合并网格，并把需要合并的各个网格的uv，根据第2步得到的Rect[] 刷一遍。
//5.把合并好的网格赋予新的SkinnedMeshRenderer，并把第3步记录下的bones赋予新的SkinnedMeshRenderer。
//6.把新的材质球赋予合并好的网格，此时就只占有1个drawcall了
public class CombineMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DoCombineMesh();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// 合并该gameObject下mesh,达到一个drawcall
    /// </summary>
    void DoCombineMesh()
    {
        MeshFilter[] mfCHildren = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[mfCHildren.Length];

        MeshRenderer[] mrChildren = GetComponentsInChildren<MeshRenderer>();
        Material[] materials = new Material[mfCHildren.Length];

        MeshRenderer mrSelf = gameObject.AddComponent<MeshRenderer>();
        MeshFilter mfSelf = gameObject.AddComponent<MeshFilter>();

        Texture2D[] textures = new Texture2D[mrChildren.Length];
        for(int i =0; i < mrChildren.Length;i++)
        {
            if(mrChildren[i].transform==transform)
            {
                continue;
            }

            materials[i] = mrChildren[i].sharedMaterial;
            Texture2D tx = materials[i].GetTexture("_MainTex") as Texture2D;

            Texture2D tx2D = new Texture2D(tx.width, tx.height, TextureFormat.ARGB32, false);
            tx2D.SetPixels(tx.GetPixels(0, 0, tx.width, tx.height));
            tx2D.Apply();
            textures[i] = tx2D;
        }
        Material materialNew = new Material(materials[0].shader);//用同一个shader创建新的材质
        materialNew.CopyPropertiesFromMaterial(materials[0]);//拷贝材质属性
        mrSelf.sharedMaterial = materialNew;//赋予给新的MeshRenderer

        Texture2D texture = new Texture2D(1024,1024);//新创一个贴图来合并所有小图
        materialNew.SetTexture("_MainTex", texture);//新材质使用这个新贴图
        Rect[] rects = texture.PackTextures(textures, 10, 1024);//将多个贴图合并到新创建的贴图上

        for(int i =0; i < mfCHildren.Length;i++)
        {
            if(mfCHildren[i].transform==transform)
            {
                continue;
            }
            Rect rect = rects[i];//拿出对应的贴图的rect
            Mesh meshCombine = mfCHildren[i].mesh;//贴图对应的mesh
            Vector2[] uvs = new Vector2[meshCombine.uv.Length];//mesh的uv数组
            for(int j = 0; j < uvs.Length;j++)
            {
                //这里感觉是计算了rect，除以贴图的长和宽能够得到0~1的值
                //不过超过了1同样可以采样
                uvs[j].x = rect.x + meshCombine.uv[j].x * rect.width;
                uvs[j].y = rect.y + meshCombine.uv[j].y * rect.height;
            }
            meshCombine.uv = uvs;//这样就可以在合并后的贴图找到属于自己的小图
            combine[i].mesh = meshCombine;//赋予 “合并结构体”CombineInstance
            combine[i].transform = mfCHildren[i].transform.localToWorldMatrix;
            mfCHildren[i].gameObject.SetActive(false);

        }

        Mesh newMesh = new Mesh();
        newMesh.CombineMeshes(combine, true, true);//合并网格
        mfSelf.mesh = newMesh;
    }
}

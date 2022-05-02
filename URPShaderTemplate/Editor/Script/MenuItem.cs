using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace URPShaderTemplate
{
    // 커스텀 쉐이더 템플릿 유니티 메뉴
    public class MenuItem
    {
        // 기본 메뉴 경로
        private const string MENUITEM_BASEPATH = "Assets/Create/URP Shader/";
        // 쉐이더 템플릿 폴더 이름
        private const string SHADERTEMPLATE_FOLDERNAME = "ShaderTemplates";
        
        // Unlit 쉐이더 - 템플릿 경로, 메뉴 경로
        private const string MENUITEMNAME_UNLIT = "Unlit Shader";
        private const string SHADERTEMPLATE_UNLIT = "URPUnlitShader.shader.txt";
        [UnityEditor.MenuItem(MENUITEM_BASEPATH + MENUITEMNAME_UNLIT, false, 85)]
        public static void GenerateNewURPShader_Unlit()
        {
            string shaderTemplatePath = GenerateURPShader.GetCurrentScriptFolerPath(SHADERTEMPLATE_FOLDERNAME);
            GenerateURPShader.CreateShaderTemplate(shaderTemplatePath + SHADERTEMPLATE_UNLIT);
        }
       
        /*
        // 추가 쉐이더 템플릿 예제
        private const string MENUITEMNAME_EXAMPLE = "Example Shader";
        private const string SHADERTEMPLATE_EXAMPLE = "URPExampleShader.shader.txt";
        [UnityEditor.MenuItem(MENUITEM_BASEPATH + MENUITEMNAME_EXAMPLE, false, 85)]
        public static void GenerateNewURPShader_Example()
        {
            string shaderTemplatePath = GenerateURPShader.GetCurrentScriptFolerPath(SHADERTEMPLATE_FOLDERNAME);
            GenerateURPShader.CreateShaderTemplate(shaderTemplatePath + SHADERTEMPLATE_EXAMPLE);
        }
        */
    }

}


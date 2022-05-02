using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace URPShaderTemplate
{
    // 커스텀 쉐이더 템플릿 생성 클래스
    public class GenerateURPShader
    {
        private const string LOG_FILENOTEXIST = "TemplateShader is not exists : ";
        
        // 신규 쉐이더 접두사
        private const string NEWSHADERWORD_FRONT = "New";
        
        // 쉐이더 템플릿 내부 키워드
        private const string KEYWORD_SHADERNAME = "#NAME#";
        
        
        // 쉐이더 템플릿 생성
        public static void CreateShaderTemplate(string templateShaderPath)
        {
            // 템플릿 쉐이더 파일 유무 체크
            if (!System.IO.File.Exists(templateShaderPath))
            {
                Debug.LogError(LOG_FILENOTEXIST + templateShaderPath);
                return;
            }
            
            // 템플릿 쉐이더 경로 & 이름 정보 가져오기
            string newShaderFileName = GetShaderFileName(templateShaderPath);
            string newShaderName = newShaderFileName.Substring(0, newShaderFileName.Length - 7);
            
            // 생성할 폴더 경로 + 쉐이더 파일명 세팅
            string generateDirectoryPath = GetGenerateDirectoryPath(); 
            string generateAssetPath = AssetDatabase.GenerateUniqueAssetPath(generateDirectoryPath + "/" + newShaderFileName);

            // 쉐이더 코드 복사 & 키워드 수정
            string fileContent = System.IO.File.ReadAllText(templateShaderPath);
            fileContent = fileContent.Replace(KEYWORD_SHADERNAME, newShaderName);

            // 신규 쉐이퍼 파일 생성
            System.IO.FileInfo file = WriteTextFile(generateAssetPath, fileContent);
            AssetDatabase.Refresh();
        }

        // 쉐이더 템플릿 폴더 경로 가져오기
        public static string GetCurrentScriptFolerPath(string shaderTemplateFolderName)
        {
            string[] findAssets = AssetDatabase.FindAssets($"t:Script {nameof(URPShaderTemplate.GenerateURPShader)}");
            if (findAssets.Length <= 0) return string.Empty;
            string currentScriptPath = AssetDatabase.GUIDToAssetPath(findAssets[0]);
            
            string editorPath = currentScriptPath.Replace(currentScriptPath.Substring(currentScriptPath.LastIndexOf("/")), "");
            editorPath = editorPath.Replace(editorPath.Substring(editorPath.LastIndexOf("/") + 1), "");
            
            string shaderTemplatePath = string.Format("{0}{1}/", editorPath, shaderTemplateFolderName);
            return shaderTemplatePath;
        }
        
        
        // 텍스트 파일 생성
        private static System.IO.FileInfo WriteTextFile(string generatePath, string content)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(generatePath);
            file.Directory.Create();
            File.WriteAllText(file.FullName, content);
            return file;
        }

        // 신규 파일 생성할 경로 얻기
        private static string GetGenerateDirectoryPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if(path == "" )
            {
                path = "Assets";
            }
            else if (System.IO.Path.GetExtension(path) != "")
            {
                path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            return path;
        }

        // 신규 쉐이더파일 이름 얻기
        private static string GetShaderFileName(string templateShaderPath)
        {
            string newShaderFileName = templateShaderPath.Substring(0, templateShaderPath.Length - 4);;
            newShaderFileName = newShaderFileName.Substring(newShaderFileName.LastIndexOf("/") + 1);
            newShaderFileName = string.Format("{0}{1}", NEWSHADERWORD_FRONT, newShaderFileName);
            return newShaderFileName;
        }
    }
    
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;

namespace RiseEngine.Core.Plugin
{
    public static class Builder
    {

        public static bool BuildPlugin(string PluginPath)
        {

            System.IO.Directory.CreateDirectory(PluginPath + "\\Assemblies\\");

            // On nétoye le dossier assemblie

            string[] Files = System.IO.Directory.GetFiles(PluginPath + "\\Assemblies\\");
            foreach (string f in Files)
            {
                System.IO.File.Delete(f);
            }



            //On verifie si le fichier build.txt existe
            if (System.IO.File.Exists(PluginPath + "\\Build.txt"))
            {

                //on lit le fichier build.txt
                System.IO.StreamReader sr = new System.IO.StreamReader(PluginPath + "\\Build.txt");
                string content = sr.ReadToEnd();
                string[] Lines = content.Split(';');

                //Pour chaque ligne du fichier
                foreach (string s in Lines)
                {
                    string Path = PluginPath + "\\Sources\\" + s;
                    if (System.IO.File.Exists(Path))
                    {

                        //On compile le fichier cible
                        return Build(Path, PluginPath + "\\Assemblies\\" + s.Split('.').First() + ".dll");

                    }
                    else
                    {

                        Debug.Logs.Write("[Plugin.Builder] The file '" + Path + "' does't exist !", Debug.LogType.Warning);

                    }


                }

            }

            return false;

        }

        public static bool Build(string FilePath, string OutputFile, string References = "none")
        {

            Debug.Logs.Write("[Plugin.Builder] Compilling '" + FilePath + "'", Debug.LogType.Info);

            System.IO.StreamReader sr = new System.IO.StreamReader(FilePath);

            bool result = false;

            if (FilePath.EndsWith("vb"))
            {
                result = BuildVB(sr.ReadToEnd(), OutputFile, References);
            }
            else if (FilePath.EndsWith("cs"))
            {
                result = BuildCSharp(sr.ReadToEnd(), OutputFile, References);
            }
            else
            {

                Debug.Logs.Write("[Plugin.Builder] Can't compilling '" + FilePath.Split('.').Last() + "' files", Debug.LogType.Info);

            }



            sr.Close();
            return result;

        }

        //Compiler du code VB.net
        public static bool BuildVB(string Code, string OutputPath, string References = "none")
        {
            Debug.Logs.Write("[Plugin.Builder] Compilling with Visual Basic compilator.", Debug.LogType.Info);

            Microsoft.VisualBasic.VBCodeProvider Provider = new Microsoft.VisualBasic.VBCodeProvider();

            return BuildCode(Code, OutputPath, Provider, References);
        }

        //Compiler du Code c#
        public static bool BuildCSharp(string Code, string OutputPath, string References = "none")
        {
            Debug.Logs.Write("[Plugin.Builder] Compilling with C# compilator.", Debug.LogType.Info);

            Microsoft.CSharp.CSharpCodeProvider Provider = new Microsoft.CSharp.CSharpCodeProvider();

            return BuildCode(Code, OutputPath, Provider, References);

        }

        public static bool BuildCode(string Code, string OutputPath, CodeDomProvider Provider, string References = "none")
        {
            ICodeCompiler Compiler = Provider.CreateCompiler();
            CompilerParameters Parameters = new CompilerParameters();
            Parameters.GenerateExecutable = false;
            Parameters.OutputAssembly = OutputPath;

            foreach (Assembly Asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Parameters.ReferencedAssemblies.Add(Asm.Location);
            }


            if (!(References == "none")) {

                string[] Files = References.Split(';');

                foreach (string f in Files) {

                    if (System.IO.File.Exists(f)) {
                        Parameters.ReferencedAssemblies.Add(f);
                        Debug.Logs.Write("[Plugin.Builder] Add reférence : '" + f.Split('\\').Last() + "'", Debug.LogType.Info);
                    }
                }

            }

            CompilerResults result = Compiler.CompileAssemblyFromSource(Parameters, Code);

            if (result.Errors.Count > 0)
            {
                Debug.Logs.Write("[Plugin.Builder] Compilation failled! '" + OutputPath + "' with (" + result.Errors.Count + " Error)", Debug.LogType.Errore);

                foreach (CompilerError Error in result.Errors)
                {
                    if (Error.IsWarning)
                    {

                        Debug.Logs.Write("   Ln" + Error.Line + " '" + Error.ErrorText + "'", Debug.LogType.Warning);
                    }
                    else
                    {

                        Debug.Logs.Write("    Ln" + Error.Line + " '" + Error.ErrorText + "'", Debug.LogType.Errore);
                    }


                }


                return false;
            }
            else
            {
                Debug.Logs.Write("[Plugin.Builder] Compilation success! '" + OutputPath + "'", Debug.LogType.Info);
                return true;
            }
        }

    }
}

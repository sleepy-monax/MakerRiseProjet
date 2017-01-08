using Maker.RiseEngine.Core.Storage;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Maker.RiseEngine.Core.Plugin
{

    public class BuildOutput
    {


        public BuildOutput(bool _Sucess, CompilerResults _Result)
        {
            Sucess = _Sucess;
            Result = _Result;
        }

        public bool Sucess;
        public CompilerResults Result;

    }

    public static class Builder
    {

        /// <summary>
        /// Compile an assembly from a source code file.
        /// </summary>
        /// <param name="SourcePath">Path of the source file.</param>
        /// <param name="OutputPath">Path of the output assemblie.</param>
        /// <param name="References">Liste of all references.</param>
        /// <returns>Compilation sucess.</returns>
        public static BuildOutput Build(string SourcePath, string OutputPath, string providerName = "CSharp")
        {

            try
            {
                //Check if the file existe. return false went does't existe.
                if (!(System.IO.File.Exists(SourcePath)))
                {

                    return new BuildOutput(false, null);
                }

                //Reading the source file
                System.IO.StreamReader sr = new System.IO.StreamReader(SourcePath);
                string Code = sr.ReadToEnd().ToDosLineEnd();
                sr.Close();


                CodeDomProvider Provider = CodeDomProvider.CreateProvider(providerName);

                //Setup compiler.
#pragma warning disable CS0618 // Le type ou le membre est obsolète
                ICodeCompiler Compiler = Provider.CreateCompiler();
#pragma warning restore CS0618 // Le type ou le membre est obsolète

                CompilerParameters Parameters = new CompilerParameters();
                Parameters.GenerateExecutable = false;
                Parameters.OutputAssembly = OutputPath;

                //getting references.
                foreach (Assembly Asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    Parameters.ReferencedAssemblies.Add(Asm.Location);
                }

                //compiling assemblie.
                CompilerResults result = Compiler.CompileAssemblyFromSource(Parameters, Code);

                //Error handeling.
                if (result.Errors.Count == 0)
                {
                    //compilation sucess do nothing.
                    EngineDebug.DebugLogs.WriteLog("Compilation success! '" + OutputPath + "'", EngineDebug.LogType.Info, "Plugin.Builder");
                    return new BuildOutput(true, result);
                }
                else
                {
                    //Compilation failled. Catch and trow error to the user.
                    EngineDebug.DebugLogs.WriteLog("Compilation failled! '" + OutputPath + "' with (" + result.Errors.Count + " ERROR!)", EngineDebug.LogType.Error, "Plugin.Builder");

                    foreach (CompilerError Error in result.Errors)
                    {
                        if (Error.IsWarning)
                        {

                            EngineDebug.DebugLogs.WriteLog("   Ln" + Error.Line + " '" + Error.ErrorText + "'", EngineDebug.LogType.Warning, "Plugin.Builder");
                        }
                        else
                        {

                            EngineDebug.DebugLogs.WriteLog("    Ln" + Error.Line + " '" + Error.ErrorText + "'", EngineDebug.LogType.Error, "Plugin.Builder");
                        }


                    }

                    return new BuildOutput(false, result);
                }
            }
            catch (Exception ex)
            {

                //Catch exeption, write in logs.
                EngineDebug.DebugLogs.WriteLog("Compilation failled!", EngineDebug.LogType.Error, "Plugin.Builder");
                EngineDebug.DebugLogs.WriteLog(ex.ToString(), EngineDebug.LogType.Info, "Plugin.Builder");

                return new BuildOutput(false, null);
            }


        }

    }
}

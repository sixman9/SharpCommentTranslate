using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Google.API.Translate;

using FileHelpers;

using ICSharpCode.NRefactory.CSharp;

namespace CommentTranslate
{
	//See Libraries/mono.options/Options.cs for Mono.Options code
	class Program
	{
		static void Main (string[] args)
		{
			string textin = "mitä tietoja vertexissä";
			
			//Get a list of files
			List<string> fileList = FindFiles.GetFilesRecursive ("/Users/sixman9/Projects/mono-dotnet/code/csat-opentk-3d", "cs");
			
			CSharpParser parser = new CSharpParser ();
			
			List<string>.Enumerator e = fileList.GetEnumerator ();
			
			while (e.MoveNext ()) {
				string nextFileName = e.Current;
				//FileStream fs = File.OpenRead(nextFileName);
				
				CompilationUnit cu = null;
				
				StringReader sr = new StringReader(File.ReadAllText(nextFileName));
				
				if (!nextFileName.ToLower ().Contains ("assemblyinfo")) {
					cu = parser.Parse(sr);
				}
				
				if (cu != null) {
					
					List<DomNode> comments = new List<DomNode> (cu.GetChildrenByRole (CompilationUnit.Roles.Comment));
					List<DomNode>.Enumerator ce = comments.GetEnumerator ();
					
					while (ce.MoveNext ()) {
						DomNode nextComment = ce.Current;
						
						if (nextComment is Comment) {
							Console.WriteLine ("File: {0}", new string[] { nextFileName });
							Console.WriteLine (((Comment)nextComment).Content + "\n\n");
						}
					}
				}
			}
			
		}
			/*
			TranslateClient client = new TranslateClient("");
			string translation = client.Translate(textin, Language.Finnish, Language.English);
			Console.WriteLine("Translation: " + translation);
			*/			
			}
}


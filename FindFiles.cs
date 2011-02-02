using System;
using System.IO;
using System.Collections.Generic;

namespace FileHelpers
{
	public static class FindFiles
	{
		public static List<string> GetFilesRecursive (string startDir, string fileExt)
		{
			fileExt = string.IsNullOrEmpty (fileExt) ? "*.*" : "*." + fileExt;
			
			// 1.
			// Store results in the file results list.
			List<string> result = new List<string> ();
			
			// 2.
			// Store a stack of our directories.
			Stack<string> stack = new Stack<string> ();
			
			// 3.
			// Add initial directory.
			stack.Push (startDir);
			
			// 4.
			// Continue while there are directories to process
			while (stack.Count > 0) {
				// A.
				// Get top directory
				string dir = stack.Pop ();
				
				try {
					// B
					// Add all files at this directory to the result List.
					result.AddRange (Directory.GetFiles (dir, fileExt));
					
					// C
					// Add all directories at this directory.
					foreach (string dn in Directory.GetDirectories (dir)) {
						stack.Push (dn);
					}
				} catch {
					// D
					// Could not open the directory
				}
			}
			return result;
		}
	}
}



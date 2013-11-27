﻿/*
  Copyright (C) 2012 Jeroen Frijters

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.

  Jeroen Frijters
  jeroen@frijters.net
  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ildasm
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputFile = null;
            string inputFile = null;
            var compatLevel = CompatLevel.None;
            foreach (var arg in args)
            {
                if (arg.StartsWith("-", StringComparison.Ordinal) || arg.StartsWith("/", StringComparison.Ordinal))
                {
                    string value;
                    if (TryMatchOption(arg, "out", out value))
                    {
                        outputFile = value;
                    }
                    else if (TryMatchOption(arg, "compat", out value))
                    {
                        switch (value)
                        {
                            case "2.0":
                                compatLevel = CompatLevel.V20;
                                break;
                            case "4.0":
                                compatLevel = CompatLevel.V40;
                                break;
                            case "4.5":
                                compatLevel = CompatLevel.V45;
                                break;
                            default:
                                PrintUsage();
                                return;
                        }
                    }
                    else
                    {
                        PrintUsage();
                        return;
                    }
                }
                else
                {
                    if (inputFile != null)
                    {
                        PrintUsage();
                        return;
                    }
                    else
                    {
                        inputFile = arg;
                    }
                }
            }
            if (inputFile == null)
            {
                PrintUsage();
                return;
            }
            var disassembler = new Disassembler(inputFile, outputFile, compatLevel);
            if (outputFile != null)
            {
                using (StreamWriter sw = new StreamWriter(outputFile, false, Encoding.ASCII))
                {
                    disassembler.Save(sw);
                }
            }
            else
            {
                disassembler.Save(Console.Out);
            }
        }

        static bool TryMatchOption(string arg, string key, out string value)
        {
            if (arg.Length > key.Length + 2 && (arg[key.Length + 1] == ':' || arg[key.Length + 1] == '=') && String.Compare(arg, 1, key, 0, key.Length, true) == 0)
            {
                value = arg.Substring(key.Length + 2);
                return true;
            }
            value = null;
            return false;
        }

        static void PrintUsage()
        {
            Console.WriteLine("IKDASM - IL disassembler example for IKVM.Reflection");
            Console.WriteLine("Copyright (C) 2012 Jeroen Frijters");
            Console.WriteLine();
            Console.WriteLine("Usage: ikdasm [options] <file_name> [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  /OUT=<file name>    Direct output to file rather than to stdout.");
            Console.WriteLine("  /COMPAT=<version>   Match ildasm behavior. (<version> = 2.0 | 4.0 | 4.5)");
        }
    }
}
